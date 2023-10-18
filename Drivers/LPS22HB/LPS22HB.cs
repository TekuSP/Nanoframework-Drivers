using System;
using System.Device.I2c;

using TekuSP.Drivers.DriverBase;
using TekuSP.Drivers.DriverBase.Enums;
using TekuSP.Drivers.DriverBase.Interfaces;

namespace TekuSP.Drivers.LPS22HB
{
    public class LPS22HB : DriverBaseI2C, ITemperatureSensor, IPressureSensor
    {
        #region Private Fields

        private const int ushortMaxValuePlusOne = 65536;

        #endregion Private Fields

        #region Public Constructors

        public LPS22HB(int I2CBusID, int deviceAddress = 0x5D) : base("LPS22HB", I2CBusID, deviceAddress)
        {
        }

        public LPS22HB(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress = 0x5D) : base("LPS22HB", I2CBusID, connectionSettings, deviceAddress)
        {
        }

        #endregion Public Constructors

        #region Private Enums

        private enum LPS22HBCommands
        {
            LPS22HB_WHO_AM_I = 0x0F, //Who am I
            LPS22HB_RES_CONF = 0x1A, //Normal (0) or Low current mode (1)
            LPS22HB_CTRL_REG1 = 0x10, //Output rate and filter settings
            LPS22HB_CTRL_REG2 = 0x11, //BOOT FIFO_EN STOP_ON_FTH IF_ADD_INC I2C_DIS SWRESET One_Shot
            LPS22HB_STATUS_REG = 0x27, //Temp or Press data available bits
            LPS22HB_PRES_OUT_XL = 0x28, //XLSB
            LPS22HB_PRES_OUT_L = 0x29, //LSB
            LPS22HB_PRES_OUT_H = 0x2A, //MSB
            LPS22HB_TEMP_OUT_L = 0x2B, //LSB
            LPS22HB_TEMP_OUT_H = 0x2C //MSB
        }

        #endregion Private Enums

        #region Public Methods

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

        public double CalculateTemperature(TemperatureUnit readTemperatureUnit, double rawTemperature)
        {
            switch (readTemperatureUnit)
            {
                case TemperatureUnit.Celsius:
                    return rawTemperature; //Already in Celsius
                case TemperatureUnit.Fahrenheit:
                    return (rawTemperature * 9 / 5) + (32 * ushortMaxValuePlusOne);

                default:
                    throw new System.NotImplementedException();
            }
        }

        public override long ReadData(byte pointer)
        {
            WriteData(new byte[] { pointer });
            return -1;
        }

        public override long ReadData(params byte[] data)
        {
            data[0] = I2CDevice.ReadByte();
            return 1;
        }

        public override string ReadDeviceId()
        {
            return ReadWrite(LPS22HBCommands.LPS22HB_WHO_AM_I).ToString();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <returns>Returns manufacturer</returns>
        public override string ReadManufacturerId()
        {
            return "STMicroelectronics";
        }

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
        public override string ReadSerialNumber()
        {
            return "Not Supported";
        }

        public double ReadTemperature()
        {
            WriteData(LPS22HBCommands.LPS22HB_CTRL_REG2, 0x1);
            if (!Status(0x2))
                return -1;
            byte tempOutH = ReadWrite(LPS22HBCommands.LPS22HB_TEMP_OUT_H);
            byte tempOutL = ReadWrite(LPS22HBCommands.LPS22HB_TEMP_OUT_L);
            return ((tempOutH << 8) | (tempOutL & 0xff)) / 100.0f;
        }

        public double ReadTemperature(TemperatureUnit readTemperatureUnit)
        {
            return CalculateTemperature(readTemperatureUnit, ReadTemperature());
        }

        public override void Start()
        {
            base.Start();
            WriteData(LPS22HBCommands.LPS22HB_RES_CONF, 0x0); // resolution: temp=32, pressure=128
            WriteData(LPS22HBCommands.LPS22HB_CTRL_REG1, 0x00); // one-shot mode
        }

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