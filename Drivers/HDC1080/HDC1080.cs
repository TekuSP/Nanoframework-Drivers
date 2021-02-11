using DriverBase;
using DriverBase.Enums;
using DriverBase.Interfaces;
using System;
using System.Threading;
using Windows.Devices.I2c;

namespace HDC1080
{
    /// <summary>
    /// Driver for Texas Instruments HDC1080, <a href="https://www.ti.com/lit/ds/symlink/hdc1080.pdf"/>
    /// </summary>
    public class HDC1080 : DriverBaseI2C, IAdvancedTemperatureSensor, IAdvancedHumiditySensor, IRegisterSensor, IDewPointSensor
    {
        #region Private Fields

        private const int ushortMaxValuePlusOne = 65536;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructs HDC1080 Device Driver, but does not start it, see <see cref="DriverBaseI2C.Start"/> to start it
        /// </summary>
        /// <remarks>Recommended to set resolution <see cref="SetHumidityResolution(int)"/> and <see cref="SetTemperatureResolution(int)"/></remarks>
        /// <param name="I2CBusID">I2C Bus ID</param>
        /// <param name="deviceAddress">I2C Device Address, default 0x40</param>
        public HDC1080(string I2CBusID, int deviceAddress = 0x40) : base("HDC 1080", I2CBusID, deviceAddress)
        {
        }

        /// <summary>
        /// Constructs HDC1080 Device Driver, but does not start it, see <see cref="DriverBaseI2C.Start"/> to start it
        /// </summary>
        /// <remarks>Recommended to set resolution <see cref="SetHumidityResolution(int)"/> and <see cref="SetTemperatureResolution(int)"/></remarks>
        /// <param name="I2CBusID">I2C Bus ID</param>
        /// <param name="connectionSettings">I2C Custom connection settings</param>
        /// <param name="deviceAddress">I2C Device Address, default 0x40</param>
        public HDC1080(string I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress = 0x40) : base("HDC 1080", I2CBusID, connectionSettings, deviceAddress)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public float CalculateDewPoint(TemperatureUnit dewPointType, float rawTemperature, float rawHumidity)
        {
            if (rawHumidity == 0)
                throw new ArgumentException();
            rawHumidity = Math.Log10(rawHumidity);
            rawTemperature = CalculateTemperature(dewPointType, rawTemperature);
            return 15927869 * (rawHumidity + ((1155072 * rawTemperature) / (15927869 + rawTemperature))) / (1155072 - rawHumidity - ((1155072 * rawTemperature) / (15927869 + rawTemperature)));
        }

        public float CalculateHumidity(HumidityType readHumidityType, float rawHumidity)
        {
            switch (readHumidityType)
            {
                case HumidityType.Relative:
                    return (rawHumidity * 100) / ushortMaxValuePlusOne;

                case HumidityType.RelativeQ16:
                    return rawHumidity * 100;

                default:
                    throw new System.NotImplementedException();
            }
        }

        public float CalculateTemperature(TemperatureUnit readTemperatureUnit, float rawTemperature)
        {
            switch (readTemperatureUnit)
            {
                case TemperatureUnit.Celsius:
                    return (rawTemperature * 165) - 40;

                case TemperatureUnit.CelsiusQ16:
                    return (rawTemperature * 165) - (40 * ushortMaxValuePlusOne);

                case TemperatureUnit.Fahrenheit:
                    return ((((rawTemperature * 165) - 40) * 9 / 5)) + (32 * ushortMaxValuePlusOne);

                case TemperatureUnit.FahrenheitQ16:
                    return ((((rawTemperature * 165) - (40 * ushortMaxValuePlusOne)) * 9 / 5)) + (32 * ushortMaxValuePlusOne);

                default:
                    throw new System.NotImplementedException();
            }
        }

        public float GetAndCalculteDewPoint(TemperatureUnit dewPointType) => CalculateDewPoint(dewPointType, ReadTemperature(), ReadHumidity());

        public void HeatUp(int seconds)
        {
            SetHeater(true);
            for (int i = 1; i < (seconds * 66); i++)
            {
                WriteData(new byte[] { 0x00 });
                Thread.Sleep(20);
                ReadData(new byte[4]);
            }
            SetHeater(false);
        }

        public override long ReadData(byte pointer)
        {
            byte[] resultData = new byte[2];
            I2CDevice.Write(new byte[] { pointer });
            Thread.Sleep(9);
            I2CDevice.Read(resultData);
            return resultData[0] << 8 | resultData[1];
        }

        public override long ReadData(params byte[] data)
        {
            I2CDevice.Read(data);
            return data.Length;
        }

        public override string ReadDeviceId() => ReadData(0xFF).ToString();

        public float ReadHumidity() => ReadData(0x01);

        public float ReadHumidity(HumidityType readHumidityType) => CalculateHumidity(readHumidityType, ReadHumidity());

        public override string ReadManufacturerId() => ReadData(0xFE).ToString();

        public IRegister ReadRegister()
        {
            HDC1080_Register register = new HDC1080_Register();
            register.SetData((byte)(ReadData(0x02) >> 8));
            return register;
        }

        public override string ReadSerialNumber() => $"{ReadData(0xFB)}{ReadData(0xFC)}{ReadData(0xFD)}";

        public float ReadTemperature() => ((ReadData(0x00) / 65536f) * 165) - 40;

        public float ReadTemperature(TemperatureUnit readTemperatureUnit) => CalculateTemperature(readTemperatureUnit, ReadTemperature());

        public void SetHeater(bool heaterTargetStatus)
        {
            if (heaterTargetStatus)
            {
                HDC1080_Register register = (HDC1080_Register)ReadRegister();
                register.Heater = true;
                register.ModeOfAcquisition = true;
                WriteRegister(register);
            }
            else
            {
                HDC1080_Register register = (HDC1080_Register)ReadRegister();
                register.Heater = false;
                register.ModeOfAcquisition = false;
                WriteRegister(register);
            }
        }

        public void SetHumidityResolution(int resolution)
        {
            HDC1080_Register register = (HDC1080_Register)ReadRegister();
            switch (resolution)
            {
                case 14:
                    register.TemperatureMeasurementResolution = false;
                    break;

                case 11:
                    register.TemperatureMeasurementResolution = true;
                    break;

                default:
                    throw new ArgumentException("Supported resolutions are 11 bit or 14 bit, this resolution is unsupported");
            }
            WriteRegister(register);
        }

        public void SetTemperatureResolution(int resolution)
        {
            HDC1080_Register register = (HDC1080_Register)ReadRegister();
            switch (resolution)
            {
                case 14:
                    register.HumidityMeasurementResolution = 0x00;
                    break;

                case 11:
                    register.HumidityMeasurementResolution = 0x01;
                    break;

                case 8:
                    register.HumidityMeasurementResolution = 0x02;
                    break;

                default:
                    throw new ArgumentException("Supported resolutions are 8 bit or 11 bit or 14 bit, this resolution is unsupported");
            }
            WriteRegister(register);
        }

        public override void WriteData(params byte[] data)
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