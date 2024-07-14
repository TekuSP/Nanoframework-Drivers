using System;
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
        private int RawInPin
        {
            get; 
        }
        private int RawOutPin
        {
            get;
        }
        private GpioPin InPin
        {
            get; set;
        }
        private GpioPin OutPin
        {
            get; set;
        }
        private bool Slave
        {
            get; set;
        }
        private Enums.DataStatus InternalReceiveStatus
        {
            get; set;
        }
        private Enums.DataStatus InternalSendStatus
        {
            get; set;
        }
        private Stopwatch DataReceiveTimer
        {
            get; set;
        }
        private int DataReceiveIndex
        {
            get; set;
        }
        public ulong RawResponse
        {
            get; set;
        }
        /// <summary>
        /// Happens when data is sent, and <see cref="Request"/> is attached to it
        /// </summary>
        public event EventHandler<IOpenThermData> DataSent = (_, _) => { };
        /// <summary>
        /// Happens when data is received the <see cref="IOpenThermData"/> is either: <code>Request</code> We have received valid OpenTherm <see cref="Request"/>, <code>Response</code> We have received valid OpenTherm <see cref="Response"/>
        /// </summary>
        public event EventHandler<IOpenThermData> DataReceived = (_, _) => { };
        /// <summary>
        /// Happens when data is received invalid the <see cref="IOpenThermData"/> is either: <code>Request</code> We have received invalid OpenTherm <see cref="Request"/>, <code>Response</code> We have received invalid OpenTherm <see cref="Response"/>
        /// <br/><br/> It is not safe to access data from this event, they can malformed or invalid
        /// </summary>
        public event EventHandler<IOpenThermData> DataInvalid = (_, _) => { };
        /// <summary>
        /// Happens when request timeout is reached <code>Slave</code>20000ms<code>Master</code>100000ms 
        /// <br/> <br/> Attached is <see cref="Request"/> which was timeout
        /// </summary>
        public event EventHandler<IOpenThermData> DataTimeout = (_, _) => { };

        private readonly ManualResetEvent dataProcessedEvent = new ManualResetEvent(false);
        private IOpenThermData LastReceivedDataFromSend { get; set; }

        private Timer TimeoutTimer
        {
            get; set;
        }
        /// <summary>
        /// OpenTherm driver constructor
        /// </summary>
        /// <param name="inPin">IN Pin for OpenTherm</param>
        /// <param name="outPin">OUT Pin for OpenTherm</param>
        /// <param name="slave">Is this device slave?
        /// <code>Slave</code>
        /// Can be used as Boiler simulator/or real Boiler
        /// <br/>Can be used as Thermostat input for passthrough
        /// <br/><see cref="https://diyless.com/product/slave-opentherm-shield"/>
        /// <code>Master</code>
        /// Can be used as Thermostat simulator/or real Thermostat
        /// <br/>Can be used as Boiler input for passthrough
        /// <br/><see cref="https://diyless.com/product/master-opentherm-shield"/>
        /// </param>

        public OpenTherm(int inPin, int outPin, bool slave)
        {
            RawInPin = inPin;
            RawOutPin = outPin;
            Slave = slave;
        }
        public CommunicationType CommunicationType => CommunicationType.OpenTherm;

        public int DeviceAddress => RawInPin + RawOutPin;

        public bool IsRunning
        {
            get;
            private set;
        }

        public string Name => "OpenTherm Adapter";

        /// <summary>
        /// Not supported, use <see cref="SendRequest(Request)"/> to request data and <see cref="DataReceived"/> to Read Data
        /// </summary>
        /// <param name="pointer"></param>
        /// <returns></returns>
        public long ReadData(byte pointer)
        {
            return -1;
        }
        /// <summary>
        /// Not supported, use <see cref="SendRequest(Request)"/> to request data and <see cref="DataReceived"/> to Read Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long ReadData(byte[] data)
        {
            return -1;
        }
        public string ReadDeviceId()
        {
            if (!IsRunning)
                return "Must be running first!";
            var manu = (Response)SendRequestAndWaitForResponse(new Requests.GetManufacturerRequest());
            var ver = (Response)SendRequestAndWaitForResponse(new Requests.GetManufacturerVersionRequest());
            var ser = (Response)SendRequestAndWaitForResponse(new Requests.GetSerialRequest());

            return "";//$"{manu.GetUInt()}-{ver.GetUInt()}-{ser.GetUInt()}";
        }
        public string ReadManufacturerId()
        {
            if (!IsRunning)
                return "Must be running first!";
            var response = (Response)SendRequestAndWaitForResponse(new Requests.GetManufacturerRequest());
            return "";// response.GetUInt().ToString();
        }
        public string ReadSerialNumber()
        {
            if (!IsRunning)
                return "Must be running first!";
            var response = (Response)SendRequestAndWaitForResponse(new Requests.GetSerialRequest());
            return "";// response.GetUInt().ToString();
        }
        /// <summary>
        /// Sends and processes request
        /// </summary>
        /// <param name="request">Request to send</param>
        /// <returns>Success sending</returns>
        public bool SendRequest(Requests.Request request)
        {
            if (!IsRunning)
                return false;
            if (InternalSendStatus != Enums.DataStatus.READY)
                return false;

            InternalSendStatus = Enums.DataStatus.REQUEST_SENDING;

            WriteData(true);
            for (int i = 31; i >= 0; i--)
            {
                WriteData(BitHelper.GetBit(request.RawData, i));
            }
            WriteData(true);

            DataSent.Invoke(this, request);

            DataReceived += SendDataFinished;
            DataInvalid += SendDataFinished;
            TimeoutTimer = new Timer((_) =>
            {
                DataReceived -= SendDataFinished;
                DataInvalid -= SendDataFinished;
                //Timeout
                DataTimeout.Invoke(this, request);
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
        /// Callback for invalid data from send operation
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Response</param>

        private void SendDataFinished(object sender, IOpenThermData e)
        {
            DataReceived -= SendDataFinished;
            DataInvalid -= SendDataFinished;
            TimeoutTimer.Dispose();
            LastReceivedDataFromSend = e;
            dataProcessedEvent.Set();
            InternalSendStatus = Enums.DataStatus.READY;
        }

        public void Restart()
        {
            Stop();
            Thread.Sleep(1000);
            Start();
        }
        public void Start()
        {
            if (IsRunning)
                return;
            IsRunning = true;
            using GpioController controller = new GpioController();
            if (controller.IsPinOpen(RawInPin))
                throw new ArgumentException($"Pin Input {RawInPin} is already opened elsewhere. Please close it first.");
            InPin = controller.OpenPin(RawInPin, PinMode.Input);
            if (controller.IsPinOpen(RawOutPin))
                throw new ArgumentException($"Pin Output {RawOutPin} is already opened elsewhere. Please close it first.");
            OutPin = controller.OpenPin(RawOutPin, PinMode.Output);

            InPin.ValueChanged += InPin_DataRecieved;
            OutPin.Write(PinValue.High); //Confirm that we are ready to communicate
            Thread.Sleep(1000);
            InternalReceiveStatus = Enums.DataStatus.READY;
            InternalSendStatus = Enums.DataStatus.READY;
        }

        public void Stop()
        {
            if (!IsRunning)
                return;
            IsRunning = false;

            using GpioController controller = new GpioController();
            InPin.ValueChanged -= InPin_DataRecieved;
            OutPin.Write(PinValue.Low); //Stop communication

            if (controller.IsPinOpen(RawInPin))
                controller.ClosePin(RawInPin);
            if (controller.IsPinOpen(RawOutPin))
                controller.ClosePin(RawOutPin);

            InPin.Dispose();
            OutPin.Dispose();

            InternalReceiveStatus = Enums.DataStatus.NOT_INITIALIZED;
            InternalSendStatus = Enums.DataStatus.NOT_INITIALIZED;

            //Reset all data and currently running stuff
            LastReceivedDataFromSend = null;
            if (dataProcessedEvent != null)
            {
                dataProcessedEvent.Set(); //Stop soft locking threads and reset event
                dataProcessedEvent.Reset();
            }
            TimeoutTimer.Dispose();
            DataReceiveIndex = 0;

            Thread.Sleep(1000);
        }
        /// <summary>
        /// Not supported,see <see cref="SendRequest(Request)"/> to send data"/>
        /// </summary>
        /// <param name="data"></param>
        public void WriteData(byte[] data)
        {
            return;
        }
        /// <summary>
        /// Writes single bit to OUT pin
        /// </summary>
        /// <param name="bit">Bit to write</param>
        public void WriteData(bool bit)
        {
            if (bit)
                OutPin.Write(PinValue.Low);
            else
                OutPin.Write(PinValue.High);

            Thread.Sleep(500);

            if (bit)
                OutPin.Write(PinValue.High);
            else
                OutPin.Write(PinValue.Low);

            Thread.Sleep(500);
        }
        /// <summary>
        /// Processes IN pin data
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Pin state</param>
        private void InPin_DataRecieved(object sender, PinValueChangedEventArgs e)
        {
            if (!IsRunning)
                return;

            if (InternalReceiveStatus == Enums.DataStatus.READY) //We are ready to receive data
            {
                if (Slave && e.ChangeType == PinEventTypes.Rising) //We are slave and we have received initial bit
                {
                    InternalReceiveStatus = Enums.DataStatus.RESPONSE_WAITING;
                }
                else
                {
                    return; //We are Master and we are not expecting any data
                }
            }

            if (InternalReceiveStatus == Enums.DataStatus.RESPONSE_WAITING) //We have received initial bit
            {
                if (e.ChangeType == PinEventTypes.Rising)
                {
                    InternalReceiveStatus = Enums.DataStatus.RESPONSE_START_BIT; //Initial bit received and valid
                    DataReceiveTimer.Start(); //Start timer
                }
                else
                {
                    DataInvalid.Invoke(this, Slave ? new ReceivedRequest(RawResponse) : new ReceivedResponse(RawResponse)); //Invalid initial bit
                    DataReceiveTimer.Reset();
                }
            }
            else if (InternalReceiveStatus == Enums.DataStatus.RESPONSE_START_BIT)
            {
                if (DataReceiveTimer.ElapsedTicks < 750 && e.ChangeType == PinEventTypes.Falling) //750 ticks to change to falling
                {
                    InternalReceiveStatus = Enums.DataStatus.RESPONSE_RECEIVING; //We are receiving data now
                    DataReceiveIndex = 0; //Index 0s
                    DataReceiveTimer.Restart(); //Restart timer for next cycle
                }
                else
                {
                    DataInvalid.Invoke(this, Slave ? new ReceivedRequest(RawResponse) : new ReceivedResponse(RawResponse)); //Invalid end of initial bit
                    DataReceiveTimer.Reset();
                }
            }
            else if (InternalReceiveStatus == Enums.DataStatus.RESPONSE_RECEIVING)
            {
                if (DataReceiveTimer.ElapsedTicks > 750) //Is this needed?
                {
                    if (DataReceiveIndex < 32) //Data have 32 indexes
                    {
                        RawResponse = (RawResponse << 1) | (e.ChangeType == PinEventTypes.Rising ? (uint)0 : (uint)1); //Read bit and add it to data
                        DataReceiveIndex++; //Move index
                        DataReceiveTimer.Restart();
                    }
                    else
                    {
                        if (Slave)
                        {
                            var req = new ReceivedRequest(RawResponse);
                            if (req.IsValidRequest())
                            {
                                DataReceived.Invoke(this, req);
                            }
                            else
                            {
                                DataInvalid.Invoke(this, req);
                            }
                        }
                        else
                        {
                            var res = new ReceivedResponse(RawResponse);
                            if (res.IsValidResponse())
                            {
                                DataReceived.Invoke(this, res);
                            }
                            else
                            {
                                DataInvalid.Invoke(this, res);
                            }
                        }
                        DataReceiveTimer.Reset();
                        InternalReceiveStatus = Enums.DataStatus.READY;
                    }
                }
            }
        }
    }
}
