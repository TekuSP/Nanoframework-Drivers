using DriverBase;

using System;
using System.Device.I2c;

namespace CST816D
{
    public class CST816D : DriverBaseI2C
    {
        public CST816D(string name, int I2CBusID, int deviceAddress) : base(name, I2CBusID, deviceAddress)
        {
        }

        public CST816D(string name, int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress) : base(name, I2CBusID, connectionSettings, deviceAddress)
        {
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
    }
}
