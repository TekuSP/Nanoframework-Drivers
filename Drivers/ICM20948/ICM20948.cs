using System;
using System.Device.I2c;

using DriverBase;

namespace ICM20948
{
    public class ICM20948 : DriverBaseI2C
    {
        /// <summary>
        /// AK09916 I2C address on the bus
        /// </summary>
        public int SecondaryDeviceAddress { get; }
        /// <summary>
        /// Read I2C address on the bus
        /// </summary>
        public int ReadDeviceAddress { get; }
        /// <summary>
        /// Write I2C address on the bus
        /// </summary>
        public int WriteDeviceAddress { get; }
        protected I2cDevice SecondaryI2CDevice;
        protected I2cDevice ReadI2CDevice;
        protected I2cDevice WriteI2CDevice;
        public ICM20948(int I2CBusID, int deviceAddress, int secondaryAddress, int deviceReadAddress, int deviceWriteAddress) : base("ICM20948", I2CBusID, deviceAddress)
        {
            SecondaryDeviceAddress = secondaryAddress;
            ReadDeviceAddress = deviceReadAddress;
            WriteDeviceAddress = deviceWriteAddress;
        }

        public ICM20948(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress, int secondaryAddress, int deviceReadAddress, int deviceWriteAddress) : base("ICM20948", I2CBusID, connectionSettings, deviceAddress)
        {
            SecondaryDeviceAddress = secondaryAddress;
            ReadDeviceAddress = deviceReadAddress;
            WriteDeviceAddress = deviceWriteAddress;
        }

        public override long ReadData(byte pointer)
        {
            throw new NotImplementedException();
        }

        public override long ReadData(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override string ReadDeviceId()
        {
            throw new NotImplementedException();
        }

        public override string ReadManufacturerId()
        {
            throw new NotImplementedException();
        }

        public override string ReadSerialNumber()
        {
            throw new NotImplementedException();
        }

        public override void WriteData(byte[] data)
        {
            throw new NotImplementedException();
        }
        public override bool IsRunning => I2CDevice != null && SecondaryI2CDevice != null && ReadI2CDevice != null && WriteI2CDevice != null;
        public override void Start()
        {
            base.Start();
            SecondaryI2CDevice = new I2cDevice(new I2cConnectionSettings(I2CConnectionSettings.BusId, SecondaryDeviceAddress, I2CConnectionSettings.BusSpeed));
            ReadI2CDevice = new I2cDevice(new I2cConnectionSettings(I2CConnectionSettings.BusId, ReadDeviceAddress, I2CConnectionSettings.BusSpeed));
            WriteI2CDevice = new I2cDevice(new I2cConnectionSettings(I2CConnectionSettings.BusId, WriteDeviceAddress, I2CConnectionSettings.BusSpeed));
        }
        public override void Stop()
        {
            base.Stop();
            SecondaryI2CDevice?.Dispose();
            SecondaryI2CDevice = null;
            ReadI2CDevice?.Dispose();
            ReadI2CDevice = null;
            WriteI2CDevice?.Dispose();
            WriteI2CDevice = null;
        }
    }
}
