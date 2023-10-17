using DriverBase;
using DriverBase.Interfaces;

using System;
using System.Device.Gpio;
using System.Device.I2c;
using System.Threading;

using static DriverBase.Event_Handlers.EventHandlers;

namespace CST816D
{
    /// <summary>
    /// CST816D Touch screen, <a href="https://github.com/lupyuen/hynitron_i2c_cst0xxse/tree/master"/>
    /// </summary>
    public class CST816D : DriverBaseI2C, ITouchSensor, IVersionInfo, IPowerSaving
    {
        #region Private Fields

        private Register currentState = null;

        private GpioPin interruptPin;
        private int interruptPinNumber;

        private GpioPin resetPin;
        private int resetPinNumber;

        #endregion Private Fields

        #region Public Constructors

        public CST816D(int I2CBusID, int interruptPin, int resetPin, int deviceAddress = 0x15) : base("CST816D", I2CBusID, deviceAddress)
        {
            interruptPinNumber = interruptPin;
            resetPinNumber = resetPin;
        }

        public CST816D(int I2CBusID, I2cConnectionSettings connectionSettings, int interruptPin, int resetPin, int deviceAddress = 0x15) : base("CST816D", I2CBusID, connectionSettings, deviceAddress)
        {
            interruptPinNumber = interruptPin;
            resetPinNumber = resetPin;
        }

        #endregion Public Constructors

        #region Public Events

        public event ITouchDataHandler OnStateChanged;

        #endregion Public Events

        #region Public Methods

        public ITouchData GetCurrentState()
        {
            return currentState;
        }

        public ITouchData Poll()
        {
            Register returnData = new Register();
            byte[] scanRegisterData = new byte[Registers.ScanRegisterByte];
            I2CDevice.WriteRead(new byte[] { Registers.ScanRegisterAddress }, scanRegisterData);

            returnData.Reserve0 = scanRegisterData[0];
            returnData.Gesture = scanRegisterData[1];
            returnData.TouchPoints = scanRegisterData[2];
            returnData.XH = scanRegisterData[3];
            returnData.XL = scanRegisterData[4];
            returnData.YH = scanRegisterData[5];
            returnData.YL = scanRegisterData[6];
            returnData.Pressure = scanRegisterData[7];
            returnData.Miscellaneous = scanRegisterData[8];
            //TODO, add more data

            returnData.X = (ushort)(((returnData.XH & Registers.MSBMask) << 8) | ((returnData.XL & Registers.LSBMask)));
            returnData.Y = (ushort)(((returnData.YH & Registers.MSBMask) << 8) | ((returnData.YL & Registers.LSBMask)));

            if (returnData.X > Registers.MaxX || returnData.X < Registers.MinX)
                returnData.X = 0xff;
            if (returnData.Y > Registers.MaxY || returnData.Y < Registers.MinY)
                returnData.Y = 0xff;

            returnData.TouchPressure = returnData.Pressure;
            return returnData;
        }

        public override long ReadData(byte pointer)
        {
            return ReadData(new byte[] { pointer });
        }

        public override long ReadData(byte[] data)
        {
            return I2CDevice.Read(data).BytesTransferred;
        }

        public override string ReadDeviceId()
        {
            return "CST816D";
        }

        public override string ReadManufacturerId()
        {
            return "Hynitron Microelectronics Co., Ltd.";
        }

        public override string ReadSerialNumber()
        {
            return "Not supported";
        }

        public int ReadVersion()
        {
            byte[] returnData = new byte[1];
            I2CDevice.WriteRead(new byte[] { Registers.VersionAddress }, returnData);
            return returnData[0];
        }

        public string ReadVersionInfo()
        {
            byte[] returnData = new byte[3];
            I2CDevice.WriteRead(new byte[] { Registers.VersionInfoAddress }, returnData);
            return $"{returnData[0]}.{returnData[1]}.{returnData[2]}";
        }

        public override void Restart()
        {
            resetPin.Toggle();
            Thread.Sleep(50);
            resetPin.Toggle();
            Thread.Sleep(50);
        }

        public void Sleep()
        {
            Restart();
            I2CDevice.Write(new byte[] { Registers.SleepRegister, Registers.StandbyCommand });
            Stop();
        }

        public override void Start()
        {
            if (I2CDevice != null)
                return; //We are already running
            base.Start();
            interruptPin = new GpioController().OpenPin(interruptPinNumber, PinMode.Input);
            resetPin = new GpioController().OpenPin(resetPinNumber, PinMode.Output);

            resetPin.Write(PinValue.High);
            Thread.Sleep(50);
            Restart();

            currentState = (Register)Poll();

            interruptPin.ValueChanged += InterruptPin_ValueChanged;
        }

        public override void Stop()
        {
            interruptPin.ValueChanged -= InterruptPin_ValueChanged;
            interruptPin.Dispose();
            resetPin.Dispose();
            base.Stop();
        }

        public void Wakeup()
        {
            Start();
        }

        public override void WriteData(byte[] data)
        {
            I2CDevice.Write(data);
        }

        #endregion Public Methods

        #region Private Methods

        private void InterruptPin_ValueChanged(object sender, PinValueChangedEventArgs e)
        {
            var newState = (Register)Poll();
            if ((newState.X == currentState.X && newState.Y == currentState.Y) || newState.Gesture == (byte)Gesture.GEST_NONE)
                return;
            currentState = newState;
            OnStateChanged(this, currentState);
        }

        #endregion Private Methods
    }
}