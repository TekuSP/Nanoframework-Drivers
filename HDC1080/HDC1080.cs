using ESP32_DriverBase;
using ESP32_DriverBase.Enums;
using ESP32_DriverBase.Interfaces;
using System.Threading;

namespace HDC1080
{
    public class HDC1080 : DriverBase, ITemperatureSensor, IAdvancedHumiditySensor, IRegisterSensor
    {
        #region Public Constructors

        public HDC1080(int deviceAddress) : base("HDC 1080", CommunicationType.I2C, deviceAddress)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public HumidityUnit ReadHumidityUnit => HumidityUnit.Percentage;
        public TemperatureUnit ReadTemperatureUnit => TemperatureUnit.Celsius;

        #endregion Public Properties

        #region Public Methods

        public void HeatUp(int seconds)
        {
            HDC1080_Register register = (HDC1080_Register)ReadRegister();
            register.Heater = true;
            register.ModeOfAcquisition = true;
            WriteRegister(register);
            for (int i = 1; i < (seconds * 66); i++)
            {
                WriteData(new byte[] { 0x00 });
                Thread.Sleep(20);
                ReadData(new byte[4]);
            }
            register.Heater = false;
            register.ModeOfAcquisition = false;
            WriteRegister(register);
        }

        public override long ReadData(byte pointer)
        {
            byte[] resultData = new byte[2];
            I2CDevice.Write(new byte[] { pointer });
            Thread.Sleep(9);
            I2CDevice.Read(resultData);
            return resultData[0] << 8 | resultData[1];
        }

        public override long ReadData(byte[] data)
        {
            I2CDevice.Read(data);
            return 0;
        }

        public override string ReadDeviceId() => ReadData(0xFF).ToString();

        public float ReadHumidity() => (ReadData(0x01) / 65536f) * 100;

        public override string ReadManufacturerId() => ReadData(0xFE).ToString();

        public IRegister ReadRegister()
        {
            HDC1080_Register register = new HDC1080_Register();
            register.SetData((byte)(ReadData(0x02) >> 8));
            return register;
        }

        public override string ReadSerialNumber() => $"{ReadData(0xFB)} {ReadData(0xFC)} {ReadData(0xFD)}";

        public float ReadTemperature() => ((ReadData(0x00) / 65536f) * 165) - 40;

        public override void WriteData(byte[] data)
        {
            I2CDevice.Write(data);
        }

        public void WriteRegister(IRegister register)
        {
            WriteData(new byte[] { 0x02, register.GetData(), 0x00 });
            Thread.Sleep(10);
        }

        #endregion Public Methods
    }
}