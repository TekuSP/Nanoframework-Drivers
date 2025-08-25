using System;
using System.Device; // for DelayHelper
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;

using TekuSP.Drivers.Nano_OpenTherm.Requests;

using TekuSP.Drivers.DriverBase.Enums;
using TekuSP.Drivers.DriverBase.Helpers;
using TekuSP.Drivers.DriverBase.Interfaces;
using TekuSP.Drivers.Nano_OpenTherm.Responses;

namespace TekuSP.Drivers.Nano_OpenTherm
{
    /// <summary>
    /// Driver for OpenTherm boilers and thermostats using OpenTherm adapter<br/>
    /// <a href="http://ihormelnyk.com/opentherm_adapter"/> <br/>
    /// <a href="https://diyless.com/product/master-opentherm-shield"/>
    /// </summary>
    public class OpenTherm : IDriverBase
    {
        #region Private Fields

        // Manchester timing constant
        private const int HalfBitUs = 500;

        private readonly ManualResetEvent dataProcessedEvent = new ManualResetEvent(false);
        private GpioController controller;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// OpenTherm driver constructor
        /// </summary>
        /// <param name="inPin">IN Pin for OpenTherm</param>
        /// <param name="outPin">OUT Pin for OpenTherm</param>
        /// <param name="slave">Is this device slave?
        /// <code>Slave</code>
        /// Can be used as Boiler simulator/or real Boiler
        /// <br/>Can be used as Thermostat input for passthrough
        /// <br/><see href="https://diyless.com/product/slave-opentherm-shield"/>
        /// <code>Master</code>
        /// Can be used as Thermostat simulator/or real Thermostat
        /// <br/>Can be used as Boiler input for passthrough
        /// <br/><see href="https://diyless.com/product/master-opentherm-shield"/>
        /// </param>
        public OpenTherm(int inPin, int outPin, bool slave)
        {
            RawInPin = inPin;
            RawOutPin = outPin;
            Slave = slave;
        }

        #endregion Public Constructors

        #region Public Events

        /// <summary>
        /// Happens when data is received invalid the <see cref="IOpenThermData"/> is either: <code>Request</code> We have received invalid OpenTherm <see cref="Request"/>, <code>Response</code> We have received invalid OpenTherm <see cref="Response"/>
        /// <br/><br/> It is not safe to access data from this event, they can malformed or invalid
        /// </summary>
        public event EventHandler<IOpenThermData> DataInvalid = (_, _) => { };

        /// <summary>
        /// Happens when data is received the <see cref="IOpenThermData"/> is either: <code>Request</code> We have received valid OpenTherm <see cref="Request"/>, <code>Response</code> We have received valid OpenTherm <see cref="Response"/>
        /// </summary>
        public event EventHandler<IOpenThermData> DataReceived = (_, _) => { };

        /// <summary>
        /// Happens when data is sent, and <see cref="Request"/> is attached to it
        /// </summary>
        public event EventHandler<IOpenThermData> DataSent = (_, _) => { };

        /// <summary>
        /// Happens when request timeout is reached <code>Slave</code>20000ms<code>Master</code>100000ms
        /// <br/> <br/> Attached is <see cref="Request"/> which was timeout
        /// </summary>
        public event EventHandler<IOpenThermData> DataTimeout = (_, _) => { };

        #endregion Public Events

        #region Public Properties

        public CommunicationType CommunicationType => CommunicationType.OpenTherm;
        public int DeviceAddress => RawInPin + RawOutPin;
        public bool IsRunning { get; private set; }
        public string Name => "OpenTherm Adapter";
        public uint RawResponse { get; set; }

        #endregion Public Properties

        #region Private Properties

        private int DataReceiveIndex { get; set; }
        private Stopwatch DataReceiveTimer { get; set; }
        private GpioPin InPin { get; set; }
        private Enums.DataStatus InternalReceiveStatus { get; set; }
        private Enums.DataStatus InternalSendStatus { get; set; }
        private IOpenThermData LastReceivedDataFromSend { get; set; }
        private GpioPin OutPin { get; set; }
        private int RawInPin { get; }
        private int RawOutPin { get; }
        private bool Slave { get; set; }
        private Timer TimeoutTimer { get; set; }

        #endregion Private Properties

        #region Public Methods

        /// <summary>
        /// Not supported, use <see cref="SendRequest(Request)"/> to request data and <see cref="DataReceived"/> to Read Data
        /// </summary>
        /// <param name="pointer">Pointer where to read in device</param>
        /// <returns>Data from device or -1 if not supported</returns>
        public long ReadData(byte pointer) => -1;

        /// <summary>
        /// Not supported, use <see cref="SendRequest(Request)"/> to request data and <see cref="DataReceived"/> to Read Data
        /// </summary>
        /// <param name="data">Data which to read from device</param>
        /// <returns>Length of data read or -1 if not supported</returns>
        public long ReadData(byte[] data) => -1;

        /// <summary>
        /// Reads device ID from device
        /// </summary>
        /// <returns>Device ID</returns>
        public string ReadDeviceId()
        {
            if (!IsRunning)
                return "Must be running first!";
            var manu = (Response)SendRequestAndWaitForResponse(new Requests.GetManufacturerRequest());
            var ver = (Response)SendRequestAndWaitForResponse(new Requests.GetManufacturerVersionRequest());
            var ser = (Response)SendRequestAndWaitForResponse(new Requests.GetSerialRequest());
            return ""; // TODO: implement formatting when helpers are available
        }

        /// <summary>
        /// Reads manufacturer from device
        /// </summary>
        /// <returns>Manufacturer ID</returns>
        public string ReadManufacturerId()
        {
            if (!IsRunning)
                return "Must be running first!";
            var response = (Response)SendRequestAndWaitForResponse(new Requests.GetManufacturerRequest());
            return "";
        }

        /// <summary>
        /// Reads serial number from device
        /// </summary>
        /// <returns>Serial Number</returns>
        public string ReadSerialNumber()
        {
            if (!IsRunning)
                return "Must be running first!";
            var response = (Response)SendRequestAndWaitForResponse(new Requests.GetSerialRequest());
            return "";
        }

        /// <summary>
        /// Restarts device driver
        /// </summary>
        public void Restart()
        {
            Stop();
            Thread.Sleep(1000);
            Start();
        }

        /// <summary>
        /// Sends and processes request
        /// </summary>
        /// <param name="data">Request or response to send</param>
        /// <returns>True if sending started successfully</returns>
        public bool SendRequest(IOpenThermData data)
        {
            if (!IsRunning)
                return false;
            if (InternalSendStatus != Enums.DataStatus.READY)
                return false;

            InternalSendStatus = Enums.DataStatus.REQUEST_SENDING;

            WriteData(true);
            for (int i = 31; i >= 0; i--)
            {
                WriteData(BitHelper.GetBit(data.RawData, i));
            }
            WriteData(true);

            DataSent.Invoke(this, data);

            DataReceived += SendDataFinished;
            DataInvalid += SendDataFinished;
            TimeoutTimer = new Timer((_) =>
            {
                DataReceived -= SendDataFinished;
                DataInvalid -= SendDataFinished;
                //Timeout
                DataTimeout.Invoke(this, data);
                dataProcessedEvent.Set();
                InternalSendStatus = Enums.DataStatus.READY;
            }, null, Slave ? 20000 : 100000, Timeout.Infinite);
            OutPin.Write(PinValue.High);
            Thread.Sleep(1000);
            return true;
        }

        /// <summary>
        /// Sends request and waits for result
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Returns null if unable to send, otherwise returns OpenThermData</returns>
        public IOpenThermData SendRequestAndWaitForResponse(Request request)
        {
            if (SendRequest(request))
            {
                dataProcessedEvent.WaitOne();
                dataProcessedEvent.Reset();
                return LastReceivedDataFromSend;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Starts device driver, this is required to call other methods. To stop call <see cref="Stop"/>
        /// </summary>
        public void Start()
        {
            if (IsRunning)
                return;
            IsRunning = true;

            controller = new GpioController();
            if (controller.IsPinOpen(RawInPin))
                throw new ArgumentException($"Pin Input {RawInPin} is already opened elsewhere. Please close it first.");
            InPin = controller.OpenPin(RawInPin, PinMode.Input);
            if (controller.IsPinOpen(RawOutPin))
                throw new ArgumentException($"Pin Output {RawOutPin} is already opened elsewhere. Please close it first.");
            OutPin = controller.OpenPin(RawOutPin, PinMode.Output);

            DataReceiveTimer = new Stopwatch();
            OutPin.Write(PinValue.High); //Confirm that we are ready to communicate
            Thread.Sleep(1000);
            InternalReceiveStatus = Enums.DataStatus.READY;
            InternalSendStatus = Enums.DataStatus.READY;

            InPin.ValueChanged += InPin_DataRecieved;
        }

        /// <summary>
        /// Stops device driver and clears memory. To start it again use <see cref="Start"/>
        /// </summary>
        public void Stop()
        {
            if (!IsRunning)
                return;
            IsRunning = false;

            InPin.ValueChanged -= InPin_DataRecieved;

            OutPin.Write(PinValue.Low); //Stop communication

            if (controller != null)
            {
                if (controller.IsPinOpen(RawInPin))
                    controller.ClosePin(RawInPin);
                if (controller.IsPinOpen(RawOutPin))
                    controller.ClosePin(RawOutPin);
            }

            InPin.Dispose();
            OutPin.Dispose();

            InternalReceiveStatus = Enums.DataStatus.NOT_INITIALIZED;
            InternalSendStatus = Enums.DataStatus.NOT_INITIALIZED;

            LastReceivedDataFromSend = null;
            dataProcessedEvent.Set();
            dataProcessedEvent.Reset();
            if (TimeoutTimer != null)
            {
                TimeoutTimer.Dispose();
                TimeoutTimer = null;
            }
            DataReceiveIndex = 0;

            controller?.Dispose();
            controller = null;

            Thread.Sleep(1000);
        }

        /// <summary>
        /// Not supported, see <see cref="SendRequest(Request)"/> to send data
        /// </summary>
        public void WriteData(byte[] data)
        {
            return;
        }
        /// <summary>
        /// Writes single bit to OUT pin using Manchester encoding
        /// </summary>
        /// <param name="bit">Bit to write</param>
        public void WriteData(bool bit)
        {
            // Manchester encoding: 500us per half-bit, mid-bit transition
            var first = bit ? PinValue.Low : PinValue.High;
            var second = bit ? PinValue.High : PinValue.Low;

            OutPin.Write(first);
            DelayHelper.DelayMicroseconds(HalfBitUs, true);
            OutPin.Write(second);
            DelayHelper.DelayMicroseconds(HalfBitUs, true);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Processes IN pin data with interrupt trigger and microsecond sampling
        /// </summary>
        private void InPin_DataRecieved(object sender, PinValueChangedEventArgs e)
        {
            if (!IsRunning)
                return;
            if (InternalReceiveStatus != Enums.DataStatus.READY)
                return;

            // Trigger a decode attempt on any edge when ready
            InternalReceiveStatus = Enums.DataStatus.RESPONSE_RECEIVING;
            if (TryDecodeManchesterFrame(out var frame))
            {
                if (Slave)
                {
                    var req = new ReceivedRequest(frame);
                    if (req.IsValidRequest())
                        DataReceived.Invoke(this, req.SelectRequest());
                    else
                        DataInvalid.Invoke(this, req);
                }
                else
                {
                    var res = new ReceivedResponse(frame);
                    if (res.IsValidResponse())
                        DataReceived.Invoke(this, res.SelectResponse());
                    else
                        DataInvalid.Invoke(this, res);
                }
            }
            else
            {
                // could not decode a valid frame
                DataInvalid.Invoke(this, Slave ? new ReceivedRequest(RawResponse) : new ReceivedResponse(RawResponse));
            }
            InternalReceiveStatus = Enums.DataStatus.READY;
        }

        /// <summary>
        /// Decode one Manchester-encoded OpenTherm frame starting at the detected edge
        /// </summary>
        /// <param name="frame">Decoded 32-bit frame</param>
        /// <returns>True if a valid frame shape was captured, regardless of parity</returns>
        private bool TryDecodeManchesterFrame(out uint frame)
        {
            frame = 0U;

            // Expect start bit: Low then High with 500us half periods
            DelayHelper.DelayMicroseconds(HalfBitUs, true);
            if (InPin.Read() != PinValue.Low)
            {
                return false;
            }
            DelayHelper.DelayMicroseconds(HalfBitUs, true);
            if (InPin.Read() != PinValue.High)
            {
                return false;
            }

            // Read 32 bits
            for (int i = 0; i < 32; i++)
            {
                DelayHelper.DelayMicroseconds(HalfBitUs, true);
                var a = InPin.Read();
                DelayHelper.DelayMicroseconds(HalfBitUs, true);
                var b = InPin.Read();
                if (a == b)
                {
                    return false; // no transition -> invalid Manchester bit
                }
                bool bit = (a == PinValue.Low && b == PinValue.High); // Low->High encodes 1
                frame = (frame << 1) | (bit ? 1U : 0U);
            }
            RawResponse = frame;
            return true;
        }

        /// <summary>
        /// Callback for invalid or valid data from send operation, completes waiting
        /// </summary>
        private void SendDataFinished(object sender, IOpenThermData e)
        {
            DataReceived -= SendDataFinished;
            DataInvalid -= SendDataFinished;
            if (TimeoutTimer != null)
            {
                TimeoutTimer.Dispose();
                TimeoutTimer = null;
            }
            LastReceivedDataFromSend = e;
            dataProcessedEvent.Set();
            InternalSendStatus = Enums.DataStatus.READY;
        }

        #endregion Private Methods
    }
}