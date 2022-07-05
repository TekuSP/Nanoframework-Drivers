using System;
using System.Device.I2c;
using System.Threading;

using DriverBase;
using DriverBase.Enums;
using DriverBase.Interfaces;

namespace TCS34725
{
    public class TCS34725 : DriverBaseI2C
    {
        public TCS34725(int I2CBusID, int deviceAddress = 0x29) : base("TCS34725", I2CBusID, deviceAddress)
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
