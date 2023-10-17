using DriverBase;
using DriverBase.Interfaces;

using System;
using System.Device.I2c;
using System.Threading;

namespace CST816D
{
    /// <summary>
    /// CST816D Touch screen, <a href="https://gitee.com/LY3T/cst816d_test/blob/master/components/cst816d/src/cst816d.c"/>
    /// </summary>
    public class CST816D : DriverBaseI2C, ITouchSensor
    {
        #region Private Fields

        private Register currentState = null;

        private Thread currentThread = null;

        private Gesture lastGesture = Gesture.GEST_NONE;

        private int pollingMillis = 0;

        private bool pollingRunning = false;

        #endregion Private Fields

        #region Public Constructors

        public CST816D(int I2CBusID, int deviceAddress = 0x15) : base("CST816D", I2CBusID, deviceAddress)
        {
        }

        public CST816D(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress = 0x15) : base("CST816D", I2CBusID, connectionSettings, deviceAddress)
        {
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<ITouchData> OnStateChanged;

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
            returnData.Gesture = (Gesture)scanRegisterData[1];
            returnData.Reserve2 = scanRegisterData[2];
            returnData.XH = scanRegisterData[3];
            returnData.XL = scanRegisterData[4];
            returnData.YH = scanRegisterData[5];
            returnData.YL = scanRegisterData[6];
            returnData.Reserve7 = scanRegisterData[7];

            returnData.X = (ushort)(((returnData.XH & Registers.MSBMask) << 8) | ((returnData.XL & Registers.LSBMask)));
            returnData.Y = (ushort)(((returnData.YH & Registers.MSBMask) << 8) | ((returnData.YL & Registers.LSBMask)));

            if (returnData.X > Registers.MaxX || returnData.X < Registers.MinX)
                returnData.X = 0xff;
            if (returnData.Y > Registers.MaxY || returnData.Y < Registers.MinY)
                returnData.Y = 0xff;
            return returnData;
        }

        public override long ReadData(byte pointer)
        {
            throw new NotSupportedException();
        }

        public override long ReadData(byte[] data)
        {
            return I2CDevice.Read(data).BytesTransferred;
        }

        public override string ReadDeviceId()
        {
            return "Not supported";
        }

        public override string ReadManufacturerId()
        {
            return "Not supported";
        }

        public override string ReadSerialNumber()
        {
            return "Not supported";
        }

        public void StartPolling(int millis)
        {
            if (pollingRunning) //We are already running
                return;
            currentThread = new Thread(new ThreadStart(WorkTask));
            currentThread.Start();
        }

        public void StopPolling()
        {
            pollingRunning = false;
        }

        public override void WriteData(byte[] data)
        {
            I2CDevice.Write(data);
        }

        #endregion Public Methods

        #region Private Methods

        private void WorkTask()
        {
            Register reg = (Register)Poll();
            currentState = reg;
            while (pollingRunning)
            {
                reg = (Register)Poll();
                if (reg.Gesture != lastGesture)
                {
                    currentState = reg;
                    OnStateChanged?.Invoke(this, currentState);
                }
                Thread.Sleep(pollingMillis);
            }
        }

        #endregion Private Methods
    }
}