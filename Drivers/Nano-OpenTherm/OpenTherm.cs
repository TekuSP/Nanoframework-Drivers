using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;

using TekuSP.Drivers.Nano_OpenTherm.Requests;

using TekuSP.Drivers.DriverBase.Enums;
using TekuSP.Drivers.DriverBase.Helpers;
using TekuSP.Drivers.DriverBase.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm
{
    /// <summary>
    /// Driver for OpenTherm boilers and thermostats using OpenTherm adapter<br/>
    /// <a href="http://ihormelnyk.com/opentherm_adapter"/> <br/>
    /// <a href="https://diyless.com/product/master-opentherm-shield"/>
    /// </summary>
    public class OpenTherm : IDriverBase
    {
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
        public ulong Response
        {
            get; set;
        }
        private event EventHandler<IOpenThermData> DataReceived = (_, _) => { };
        private event EventHandler<IOpenThermData> DataInvalid = (_, _) => { };
        private event EventHandler<IOpenThermData> DataTimeout = (_, _) => { };

        private ManualResetEvent dataProcessedEvent = new ManualResetEvent(false);
        private IOpenThermData LastReceivedDataFromSend { get; set; }

        private Timer TimeoutTimer
        {
            get; set;
        }
        public OpenTherm(int inPin, int outPin, bool slave)
        {
            using GpioController controller = new GpioController();
            InPin = controller.OpenPin(inPin, PinMode.Input);
            OutPin = controller.OpenPin(outPin, PinMode.Output);
            Slave = slave;
        }
        public CommunicationType CommunicationType => CommunicationType.Other;

        public int DeviceAddress => 0;

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
            var manu = (TekuSP.Drivers.Nano_OpenTherm.Response)SendRequestAndWaitForResponse(new Requests.GetManufacturerRequest());
            var ver = (TekuSP.Drivers.Nano_OpenTherm.Response)SendRequestAndWaitForResponse(new Requests.GetManufacturerVersionRequest());
            var ser = (TekuSP.Drivers.Nano_OpenTherm.Response)SendRequestAndWaitForResponse(new Requests.GetSerialRequest());

            return $"{manu.GetUInt()}-{ver.GetUInt()}-{ser.GetUInt()}";
        }
        public string ReadManufacturerId()
        {
            var response = (TekuSP.Drivers.Nano_OpenTherm.Response)SendRequestAndWaitForResponse(new Requests.GetManufacturerRequest());
            return response.GetUInt().ToString();
        }
        public string ReadSerialNumber()
        {
            var response = (TekuSP.Drivers.Nano_OpenTherm.Response)SendRequestAndWaitForResponse(new Requests.GetSerialRequest());
            return response.GetUInt().ToString();
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
            Start();
        }
        public void Start()
        {
            InPin.ValueChanged += InPin_DataRecieved;
            OutPin.Write(PinValue.High); //Confirm that we are ready to communicate
            Thread.Sleep(1000);
            IsRunning = true;
            InternalReceiveStatus = Enums.DataStatus.READY;
            InternalSendStatus = Enums.DataStatus.READY;
        }

        public void Stop()
        {
            InPin.ValueChanged -= InPin_DataRecieved;
            OutPin.Write(PinValue.Low); //Stop communication
            Thread.Sleep(1000);
            IsRunning = false;
            InternalReceiveStatus = Enums.DataStatus.NOT_INITIALIZED;
            InternalSendStatus = Enums.DataStatus.NOT_INITIALIZED;
        }
        /// <summary>
        /// Not supported
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
                    DataInvalid.Invoke(this, Slave ? new ReceivedRequest(Response) : new Response(Response)); //Invalid initial bit
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
                    DataInvalid.Invoke(this, Slave ? new ReceivedRequest(Response) : new Response(Response)); //Invalid end of initial bit
                    DataReceiveTimer.Reset();
                }
            }
            else if (InternalReceiveStatus == Enums.DataStatus.RESPONSE_RECEIVING)
            {
                if (DataReceiveTimer.ElapsedTicks > 750) //Is this needed?
                {
                    if (DataReceiveIndex < 32) //Data have 32 indexes
                    {
                        Response = (Response << 1) | (e.ChangeType == PinEventTypes.Rising ? (uint)0 : (uint)1); //Read bit and add it to data
                        DataReceiveIndex++; //Move index
                        DataReceiveTimer.Restart();
                    }
                    else
                    {
                        if (Slave)
                        {
                            var req = new ReceivedRequest(Response);
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
                            var res = new Response(Response);
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
