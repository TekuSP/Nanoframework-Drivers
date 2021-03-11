using System;
using System.Device.I2c;

using DriverBase;

namespace ADS1015
{
    /// <summary>
    /// 12 bit ADC Texas Instruments ADS1015, <a href="https://www.ti.com/lit/ds/symlink/ads1015.pdf"/>
    /// </summary>
    public class ADS1015 : DriverBaseI2C
    {
        public ADS1015(int I2CBusID, int deviceAddress) : base("ADS1015", I2CBusID, deviceAddress)
        {
        }

        public ADS1015(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress) : base("ADS1015", I2CBusID, connectionSettings, deviceAddress)
        {
        }

        public override long ReadData(byte pointer)
        {
            WriteData(new byte[] { pointer });
            return -1;
        }

        public override long ReadData(params byte[] data)
        {
            SpanByte read = new SpanByte(data);
            I2CDevice.Read(read);
            data = read.ToArray();
            return data.Length;
        }
        /// <summary>
        /// Not Supported
        /// </summary>
        /// <returns>Not Supported</returns>
        public override string ReadDeviceId()
        {
            return "Not supported";
        }
        /// <summary>
        /// Not Supported
        /// </summary>
        /// <returns>Not Supported</returns>
        public override string ReadManufacturerId()
        {
            return "Texas Instruments";
        }
        /// <summary>
        /// Not Supported
        /// </summary>
        /// <returns>Not Supported</returns>
        public override string ReadSerialNumber()
        {
            return "Not supported";
        }

        public override void WriteData(params byte[] data)
        {
            I2CDevice.Write(new SpanByte(data));
        }

        private enum PointerRegister
        {
            ADS_POINTER_CONVERT = 0x00,
            ADS_POINTER_CONFIG = 0x01,
            ADS_POINTER_LOWTHRESH = 0x02,
            ADS_POINTER_HIGHTHRESH = 0x03
        }
    }
}
