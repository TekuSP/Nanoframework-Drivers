using System;
using System.Device.I2c;

using TekuSP.Drivers.DriverBase;
using TekuSP.Drivers.DriverBase.Enums;
using TekuSP.Drivers.DriverBase.Interfaces;
using TekuSP.Drivers.LPS22HB.Constants;
using TekuSP.Drivers.LPS22HB.Enums;

namespace TekuSP.Drivers.LPS22HB
{
    /// <summary>
    /// Driver for STMicroelectronics LPS22HB pressure sensor.
    /// </summary>
    public class LPS22HB : DriverBaseI2C, ITemperatureSensor, IPressureSensor
    {
        #region Public Constructors

        /// <summary>
        /// Initializes LPS22HB with default I2C settings.
        /// </summary>
        /// <param name="I2CBusID">I2C bus ID.</param>
        /// <param name="deviceAddress">I2C device address.</param>
        public LPS22HB(int I2CBusID, int deviceAddress = 0x5D) : base("LPS22HB", I2CBusID, deviceAddress)
        {
        }

        /// <summary>
        /// Initializes LPS22HB with custom I2C settings.
        /// </summary>
        /// <param name="I2CBusID">I2C bus ID.</param>
        /// <param name="connectionSettings">Custom I2C connection settings.</param>
        /// <param name="deviceAddress">I2C device address.</param>
        public LPS22HB(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress = 0x5D) : base("LPS22HB", I2CBusID, connectionSettings, deviceAddress)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc/>
        public double CalculatePressure(PressureType type, double rawPressure)
        {
            switch (type)
            {
                case PressureType.mBar:
                    return rawPressure / 4096.0f;

                case PressureType.Bar:
                    return (rawPressure / 4096.0f) / 1000;

                case PressureType.Torr:
                    return ((rawPressure / 4096.0f) / 1000) / 750.06167382f;

                default:
                    throw new System.NotImplementedException();
            }
        }

        /// <inheritdoc/>
        public double CalculateTemperature(TemperatureUnit readTemperatureUnit, double rawTemperature)
        {
            switch (readTemperatureUnit)
            {
                case TemperatureUnit.Celsius:
                    return rawTemperature; //Already in Celsius
                case TemperatureUnit.Fahrenheit:
                    return (rawTemperature * 9 / 5) + (32 * LPS22HBConstants.UShortMaxValuePlusOne);

                default:
                    throw new System.NotImplementedException();
            }
        }

        /// <inheritdoc/>
        public override long ReadData(byte pointer)
        {
            WriteData(new byte[] { pointer });
            return -1;
        }

        /// <inheritdoc/>
        public override long ReadData(params byte[] data)
        {
            data[0] = I2CDevice.ReadByte();
            return 1;
        }

        /// <inheritdoc/>
        public override string ReadDeviceId()
        {
            return ReadWrite(LPS22HBCommands.LPS22HB_WHO_AM_I).ToString();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <returns>Returns manufacturer</returns>
        /// <inheritdoc/>
        public override string ReadManufacturerId()
        {
            return "STMicroelectronics";
        }

        /// <inheritdoc/>
        public double ReadPressure()
        {
            WriteData(LPS22HBCommands.LPS22HB_CTRL_REG2, 0x1);
            if (!Status(0x1))
                return -1;
            byte pressOutH = ReadWrite(LPS22HBCommands.LPS22HB_PRES_OUT_H);
            byte pressOutL = ReadWrite(LPS22HBCommands.LPS22HB_PRES_OUT_L);
            byte pressOutXL = ReadWrite(LPS22HBCommands.LPS22HB_PRES_OUT_XL);
            return ((((long)pressOutH << 24) | ((long)pressOutL << 16) | ((long)pressOutXL << 8)) >> 8);
        }

        /// <inheritdoc/>
        public double ReadPressure(PressureType type)
        {
            double pr = ReadPressure();
            if (pr == -1)
                return pr;
            return CalculatePressure(type, pr);
        }

        /// <summary>
        /// Not Supported
        /// </summary>
        /// <returns>Not Supported</returns>
        /// <inheritdoc/>
        public override string ReadSerialNumber()
        {
            return "Not Supported";
        }

        /// <inheritdoc/>
        public double ReadTemperature()
        {
            WriteData(LPS22HBCommands.LPS22HB_CTRL_REG2, 0x1);
            if (!Status(0x2))
                return -1;
            byte tempOutH = ReadWrite(LPS22HBCommands.LPS22HB_TEMP_OUT_H);
            byte tempOutL = ReadWrite(LPS22HBCommands.LPS22HB_TEMP_OUT_L);
            return ((tempOutH << 8) | (tempOutL & 0xff)) / 100.0f;
        }

        /// <inheritdoc/>
        public double ReadTemperature(TemperatureUnit readTemperatureUnit)
        {
            return CalculateTemperature(readTemperatureUnit, ReadTemperature());
        }

        /// <inheritdoc/>
        public override void Start()
        {
            base.Start();
            WriteData(LPS22HBCommands.LPS22HB_RES_CONF, 0x0); // resolution: temp=32, pressure=128
            WriteData(LPS22HBCommands.LPS22HB_CTRL_REG1, 0x00); // one-shot mode
        }

        /// <summary>
        /// Checks status flags for data availability.
        /// </summary>
        /// <param name="status">Status bit mask.</param>
        /// <returns>True if data is available.</returns>
        public bool Status(byte status)
        {
            int count = 1000;
            byte data;
            do
            {
                data = ReadWrite(LPS22HBCommands.LPS22HB_STATUS_REG);
                --count;
                if (count < 0)
                    break;
            } while ((data & status) == 0);

            if (count < 0)
                return false;
            else
                return true;
        }

        /// <inheritdoc/>
        public override void WriteData(params byte[] data)
        {
            I2CDevice.Write(new SpanByte(data));
        }

        #endregion Public Methods

        #region Private Methods

        private byte ReadWrite(LPS22HBCommands command, byte data)
        {
            byte[] buff = new byte[1];
            WriteData(command, data);
            ReadData(buff);
            return buff[0];
        }

        private byte ReadWrite(LPS22HBCommands command)
        {
            byte[] buff = new byte[1];
            WriteData(command);
            ReadData(buff);
            return buff[0];
        }

        private void WriteData(LPS22HBCommands command, byte[] data)
        {
            WriteData(new byte[] { (byte)command });
            if (data != null) //Data is also present to the command
                WriteData(data);
        }

        private void WriteData(LPS22HBCommands command)
        {
            WriteData(new byte[] { (byte)command });
        }

        private void WriteData(LPS22HBCommands command, byte data)
        {
            WriteData(new byte[] { (byte)command, data });
        }

        #endregion Private Methods
    }
}