using System;
using System.Device.I2c;
using System.Threading;

using TekuSP.Drivers.ADS1015.Enums;

using TekuSP.Drivers.DriverBase;
using TekuSP.Drivers.DriverBase.Interfaces;

namespace TekuSP.Drivers.ADS1015
{
    /// <summary>
    /// 12 bit ADC Texas Instruments ADS1015, <a href="https://www.ti.com/lit/ds/symlink/ads1015.pdf"/>
    /// </summary>
    public class ADS1015 : DriverBaseI2C, IADCModule
    {
        #region Public Constructors

        /// <summary>
        /// Initializes ADS1015 with default I2C connection settings.
        /// </summary>
        /// <param name="I2CBusID">I2C bus ID.</param>
        /// <param name="deviceAddress">I2C device address.</param>
        public ADS1015(int I2CBusID, int deviceAddress = 0x48) : base("ADS1015", I2CBusID, deviceAddress)
        {
        }

        /// <summary>
        /// Initializes ADS1015 with custom I2C connection settings.
        /// </summary>
        /// <param name="I2CBusID">I2C bus ID.</param>
        /// <param name="connectionSettings">Custom I2C connection settings.</param>
        /// <param name="deviceAddress">I2C device address.</param>
        public ADS1015(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress = 0x48) : base("ADS1015", I2CBusID, connectionSettings, deviceAddress)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Performs a differential conversion using default configuration.
        /// </summary>
        /// <param name="channelOne">Positive input channel.</param>
        /// <param name="channelTwo">Negative input channel.</param>
        /// <returns>Signed 12-bit conversion result.</returns>
        public short DifferentialRead(int channelOne, int channelTwo)
        {
            return DifferentialRead(channelOne, channelTwo, modeSetting: ModeSetting.ADS_CONFIG_MODE_NOCONTINUOUS);
        }

        /// <summary>
        /// Performs a differential conversion with full configuration options.
        /// </summary>
        /// <param name="channelOne">Positive input channel.</param>
        /// <param name="channelTwo">Negative input channel.</param>
        /// <param name="modeSetting">Conversion mode setting.</param>
        /// <param name="gain">PGA gain setting.</param>
        /// <param name="comparatorLatching">Comparator latching mode.</param>
        /// <param name="comparatorPolarity">Comparator polarity.</param>
        /// <param name="comparatorAssert">Comparator assert behavior.</param>
        /// <param name="comparatorMode">Comparator mode.</param>
        /// <param name="dataRate">Data rate setting.</param>
        /// <returns>Signed 12-bit conversion result.</returns>
        public short DifferentialRead(int channelOne, int channelTwo, ModeSetting modeSetting = ModeSetting.ADS_CONFIG_MODE_NOCONTINUOUS, GainSetting gain = GainSetting.ADS_CONFIG_PGA_2048, ComparatorLatching comparatorLatching = ComparatorLatching.ADS_CONFIG_COMP_NONLAT, ComparatorPolarity comparatorPolarity = ComparatorPolarity.ADS_CONFIG_COMP_POL_LOW, ComparatorAssert comparatorAssert = ComparatorAssert.ADS_CONFIG_COMP_QUE_NON, ComparatorMode comparatorMode = ComparatorMode.ADS_CONFIG_COMP_MODE_TRADITIONAL, DataRateSetting dataRate = DataRateSetting.ADS_CONFIG_DR_RATE_1600)
        {
            ushort configuration = (ushort)((ushort)modeSetting | (ushort)gain | (ushort)comparatorAssert | (ushort)comparatorLatching | (ushort)comparatorPolarity | (ushort)comparatorMode | (ushort)dataRate);
            if (channelOne == 2 && channelTwo == 3)
                configuration |= (ushort)MultiplexerSetting.ADS_CONFIG_MUX_MUL_2_3;
            else if (channelOne == 1 && channelTwo == 3)
                configuration |= (ushort)MultiplexerSetting.ADS_CONFIG_MUX_MUL_1_3;
            else if (channelOne == 0 && channelTwo == 3)
                configuration |= (ushort)MultiplexerSetting.ADS_CONFIG_MUX_MUL_0_3;
            else if (channelOne == 0 && channelTwo == 1)
                configuration |= (ushort)MultiplexerSetting.ADS_CONFIG_MUX_MUL_0_1;
            else
                throw new ArgumentException("Supports only, AIN0 = P, AIN1 = N or AIN0 = P, AIN3 = N or AIN2 = P, AIN3 = N or AIN1 = P, AIN3 = N");
            configuration |= (ushort)OSCommands.ADS_CONFIG_OS_SINGLE_CONVERT;
            WriteRegister((byte)PointerRegister.ADS_POINTER_CONFIG, configuration);
            Thread.Sleep(1);
            var res = (ushort)(ReadRegister((byte)PointerRegister.ADS_POINTER_CONVERT) >> 4);
            if (res > 0x07FF)
            {
                // negative number - extend the sign to 16th bit
                res |= 0xF000;
            }
            return (short)res;
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

        /// <summary>
        /// Performs a single-ended conversion using default configuration.
        /// </summary>
        /// <param name="channelNumber">Input channel number (0-3).</param>
        /// <returns>Unsigned 12-bit conversion result.</returns>
        public ushort SingleRead(int channelNumber)
        {
            return SingleRead(channelNumber, modeSetting: ModeSetting.ADS_CONFIG_MODE_NOCONTINUOUS);
        }

        /// <summary>
        /// Performs a single-ended conversion with full configuration options.
        /// </summary>
        /// <param name="channelNumber">Input channel number (0-3).</param>
        /// <param name="modeSetting">Conversion mode setting.</param>
        /// <param name="gain">PGA gain setting.</param>
        /// <param name="comparatorLatching">Comparator latching mode.</param>
        /// <param name="comparatorPolarity">Comparator polarity.</param>
        /// <param name="comparatorAssert">Comparator assert behavior.</param>
        /// <param name="comparatorMode">Comparator mode.</param>
        /// <param name="dataRate">Data rate setting.</param>
        /// <returns>Unsigned 12-bit conversion result.</returns>
        public ushort SingleRead(int channelNumber, ModeSetting modeSetting = ModeSetting.ADS_CONFIG_MODE_NOCONTINUOUS, GainSetting gain = GainSetting.ADS_CONFIG_PGA_2048, ComparatorLatching comparatorLatching = ComparatorLatching.ADS_CONFIG_COMP_NONLAT, ComparatorPolarity comparatorPolarity = ComparatorPolarity.ADS_CONFIG_COMP_POL_LOW, ComparatorAssert comparatorAssert = ComparatorAssert.ADS_CONFIG_COMP_QUE_NON, ComparatorMode comparatorMode = ComparatorMode.ADS_CONFIG_COMP_MODE_TRADITIONAL, DataRateSetting dataRate = DataRateSetting.ADS_CONFIG_DR_RATE_1600)
        {
            ushort configuration = (ushort)((ushort)modeSetting | (ushort)gain | (ushort)comparatorAssert | (ushort)comparatorLatching | (ushort)comparatorPolarity | (ushort)comparatorMode | (ushort)dataRate);
            switch (channelNumber)
            {
                case 0:
                    configuration |= (ushort)MultiplexerSetting.ADS_CONFIG_MUX_SINGLE_0;
                    break;

                case 1:
                    configuration |= (ushort)MultiplexerSetting.ADS_CONFIG_MUX_SINGLE_1;
                    break;

                case 2:
                    configuration |= (ushort)MultiplexerSetting.ADS_CONFIG_MUX_SINGLE_2;
                    break;

                case 3:
                    configuration |= (ushort)MultiplexerSetting.ADS_CONFIG_MUX_SINGLE_3;
                    break;

                default:
                    throw new ArgumentException("Only channels 0, 1, 2, 3 are supported");
            }
            configuration |= (ushort)OSCommands.ADS_CONFIG_OS_SINGLE_CONVERT;
            WriteRegister((byte)PointerRegister.ADS_POINTER_CONFIG, configuration);
            Thread.Sleep(1);
            return (ushort)(ReadRegister((byte)PointerRegister.ADS_POINTER_CONVERT) >> 4);
        }

        /// <inheritdoc/>
        public override void WriteData(params byte[] data)
        {
            I2CDevice.Write(new SpanByte(data));
        }

        /// <summary>
        /// Writes a 16-bit value to a register.
        /// </summary>
        /// <param name="reg">Register address.</param>
        /// <param name="value">16-bit value to write.</param>
        public void WriteRegister(byte reg, ushort value)
        {
            I2CDevice.Write(new SpanByte(new byte[] { reg, (byte)(value >> 8), (byte)(value & 0xFF) }));
        }

        #endregion Public Methods

        #region Private Methods

        private ushort ReadRegister(byte reg)
        {
            byte[] readBuffer = new byte[2];
            I2CDevice.WriteRead(new byte[] { reg }, readBuffer);

            return (ushort)((readBuffer[0] << 8) | readBuffer[1]);
        }

        #endregion Private Methods
    }
}