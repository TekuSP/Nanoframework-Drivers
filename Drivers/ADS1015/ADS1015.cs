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

        public ADS1015(int I2CBusID, int deviceAddress = 0x48) : base("ADS1015", I2CBusID, deviceAddress)
        {
        }

        public ADS1015(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress = 0x48) : base("ADS1015", I2CBusID, connectionSettings, deviceAddress)
        {
        }

        #endregion Public Constructors

        #region Private Enums

        private enum PointerRegister
        {
            ADS_POINTER_CONVERT = 0x00,
            ADS_POINTER_CONFIG = 0x01,
            ADS_POINTER_LOWTHRESH = 0x02,
            ADS_POINTER_HIGHTHRESH = 0x03
        }

        #endregion Private Enums

        #region Public Methods

        public short DifferentialRead(int channelOne, int channelTwo)
        {
            return DifferentialRead(channelOne, channelTwo, modeSetting: ModeSetting.ADS_CONFIG_MODE_NOCONTINUOUS);
        }

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

        public ushort SingleRead(int channelNumber)
        {
            return SingleRead(channelNumber, modeSetting: ModeSetting.ADS_CONFIG_MODE_NOCONTINUOUS);
        }

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

        public override void WriteData(params byte[] data)
        {
            I2CDevice.Write(new SpanByte(data));
        }

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