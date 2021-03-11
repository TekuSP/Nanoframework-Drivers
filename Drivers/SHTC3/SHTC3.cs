using System;
using System.Device.I2c;

using DriverBase;
using DriverBase.Enums;
using DriverBase.Interfaces;
namespace SHTC3
{
    public class SHTC3 : DriverBaseI2C, ISleepSupport, ITemperatureSensor, IHumiditySensor
    {
        public SHTC3(int I2CBusID, int deviceAddress = 0x70) : base("SHTC3", I2CBusID, deviceAddress)
        {
        }

        public SHTC3(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress = 0x70) : base("SHTC3", I2CBusID, connectionSettings, deviceAddress)
        {
        }

        public bool IsSleeping => throw new NotImplementedException();

        public float CalculateHumidity(HumidityType readHumidityType, float rawHumidity)
        {
            throw new NotImplementedException();
        }

        public float CalculateTemperature(TemperatureUnit readTemperatureUnit, float rawTemperature)
        {
            throw new NotImplementedException();
        }

        public override long ReadData(byte pointer)
        {
            WriteData(new byte[] { pointer });
            return -1;
        }

        public override long ReadData(byte[] data)
        {
            return I2CDevice.Read(data).BytesTransferred;
        }

        public override string ReadDeviceId()
        {
            throw new NotImplementedException();
        }

        public float ReadHumidity()
        {
            throw new NotImplementedException();
        }

        public float ReadHumidity(HumidityType readHumidityType)
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

        public float ReadTemperature()
        {
            throw new NotImplementedException();
        }

        public float ReadTemperature(TemperatureUnit readTemperatureUnit)
        {
            throw new NotImplementedException();
        }

        public void Sleep()
        {
            throw new NotImplementedException();
        }

        public void WakeUp()
        {
            throw new NotImplementedException();
        }

        public override void WriteData(params byte[] data)
        {
            I2CDevice.Write(data);
        }
    }
}
