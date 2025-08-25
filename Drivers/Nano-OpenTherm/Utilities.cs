using System;
using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm
{
    public static class Utilities
    {
        /// <summary>
        /// Gets raw temperature from float
        /// </summary>
        /// <param name="temperature">Temperature</param>
        /// <returns>Uint Raw Temperature</returns>
        public static uint GetRawTemperature(float temperature)
        {
            if (temperature < 0)
                temperature = 0;
            if (temperature > 100)
                temperature = 100;
            return (uint)(temperature * 256);
        }
        /// <summary>
        /// Parity check for a frame
        /// </summary>
        /// <param name="frame">Frame to check on</param>
        /// <returns>Parity (true when number of 1 bits is odd)</returns>
        public static bool Parity(uint frame)
        {
            byte p = 0;
            while (frame > 0)
            {
                if ((frame & 1) == 1)
                    p++;
                frame >>= 1;
            }
            return (p & 1) == 1;
        }
        //TODO: Move to base solution
        /// <summary>
        /// Get Uint from raw data (low 16-bits data field)
        /// </summary>
        /// <returns>Uint Response</returns>
        public static uint GetUInt(uint rawData) => rawData & 0xffffu;
        /// <summary>
        /// Gets Int from raw data (low 32-bits)
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>Int Response</returns>
        public static int GetInt(uint rawData) => (int)(rawData & 0xFFFFFFFFu);
        /// <summary>
        /// Gets High part of Int from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>High of int</returns>
        public static short GetHighShort(uint rawData)
        {
            var temp = GetInt(rawData);
            return (short)(temp >> 16);
        }
        /// <summary>
        /// Gets Low part of Int from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>Low of int</returns>
        public static short GetLowShort(uint rawData)
        {
            var temp = GetInt(rawData);
            return (short)(temp & 0xFFFF);
        }
        /// <summary>
        /// Gets high part of UShort from raw data (bits 31..16)
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>High of uint</returns>
        public static ushort GetHighUShort(uint rawData) => (ushort)((rawData >> 16) & 0xFFFF);
        /// <summary>
        /// Gets high part of UShort from raw data where low part is byte (bits 23..8)
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>High part of int, minus byte</returns>
        public static ushort GetHighUShortWithLowByte(uint rawData) => (ushort)((rawData >> 8) & 0xFFFF);
        /// <summary>
        /// Gets low part of UShort from raw data (bits 15..0)
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>Low part of uint</returns>
        public static ushort GetLowUShort(uint rawData) => (ushort)(rawData & 0xFFFF);
        /// <summary>
        /// Gets High part of Byte from raw data (bits 15..8 of 16-bit data field)
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>High part of uint</returns>
        public static byte GetHighByte(uint rawData)
        {
            uint temp = GetUInt(rawData);
            return (byte)(temp >> 8);
        }
        /// <summary>
        /// Gets Low part of Byte from raw data (bits 7..0 of 16-bit data field)
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>Low part of uint</returns>
        public static byte GetLowByte(uint rawData)
        {
            var temp = GetUInt(rawData);
            return (byte)(temp & 0xFF);
        }
        /// <summary>
        /// Get Float from raw data (signed 16-bit fixed point 8.8)
        /// </summary>
        /// <returns>Float Response</returns>
        public static float GetFloat(uint rawData)
        {
            var temp = GetUInt(rawData);
            if ((temp & 0x8000) != 0)
            {
                // negative
                return -((0x10000 - (temp & 0xFFFF)) / 256.0f);
            }
            else
            {
                return (temp & 0xFFFF) / 256.0f;
            }
        }
        /// <summary>
        /// If under 0 returns 0, if over 100 returns 100, else returns input
        /// </summary>
        /// <param name="input">Float to normalize</param>
        /// <returns>Normalized float</returns>
        public static float Normalize(this float input)
        {
            return input < 0 ? 0 : (input > 100 ? 100 : input);
        }
        /// <summary>
        /// Gets Special DateTime from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <param name="time">Time</param>
        /// <param name="dayOfWeek">Day of week</param>
        public static void GetDateTime(uint rawData, out DateTime time, out DayOfWeek dayOfWeek)
        {
            var date = GetLowByte(rawData);
            var minutes = GetHighByte(rawData);
            var dayofweek = (byte)((date >> 5) & 0x07);
            var hour = (byte)(date & 0x1F);
            time = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, hour, minutes, DateTime.UtcNow.Second);
            dayOfWeek = (DayOfWeek)dayofweek;
        }
        /// <summary>
        /// Gets Master Status from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>MasterStatus</returns>
        public static Enums.MasterStatus GetMasterStatus(uint rawData)
        {
            var data = GetLowByte(rawData);
            return (Enums.MasterStatus)data;
        }
        /// <summary>
        /// Gets Slave Status from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>SlaveStatus</returns>
        public static Enums.SlaveStatus GetSlaveStatus(uint rawData)
        {
            var data = GetHighByte(rawData);
            return (Enums.SlaveStatus)data;
        }
        /// <summary>
        /// Gets Master Configuration from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>MasterConfiguration</returns>
        public static Enums.MasterConfiguration GetMasterConfiguration(uint rawData)
        {
            var data = GetLowByte(rawData);
            return (Enums.MasterConfiguration)data;
        }
        /// <summary>
        /// Gets Slave Configuration from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>SlaveConfiguration</returns>
        public static Enums.SlaveConfiguration GetSlaveConfiguration(uint rawData)
        {
            var data = GetLowByte(rawData);
            return (Enums.SlaveConfiguration)data;
        }
        /// <summary>
        /// Gets Application Specific Fault Flags from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>ApplicationSpecificFaultFlags</returns>
        public static Enums.ApplicationSpecificFaultFlags GetApplicationSpecificFaultFlags(uint rawData)
        {
            var data = GetLowByte(rawData);
            return (Enums.ApplicationSpecificFaultFlags)data;
        }
        /// <summary>
        /// Gets Remote Parameter Transfer Enable from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>RemoteParameterTransferEnable</returns>
        public static Enums.RemoteParameterTransferEnable GetRemoteParameterTransferEnable(uint rawData)
        {
            var data = GetLowByte(rawData);
            return (Enums.RemoteParameterTransferEnable)data;
        }
        /// <summary>
        /// Gets Remote Parameter Transfer Read Write from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>RemoteParameterTransferReadWrite</returns>
        public static Enums.RemoteParameterTransferReadWrite GetRemoteParameterTransferReadWrite(uint rawData)
        {
            var data = GetHighByte(rawData);
            return (Enums.RemoteParameterTransferReadWrite)data;
        }
        /// <summary>
        /// Gets Remote Override Function from raw data
        /// </summary>
        /// <param name="rawData">Raw data</param>
        /// <returns>RemoteOverrideFunction</returns>
        public static Enums.RemoteOverrideFunction GetRemoteOverrideFunction(uint rawData)
        {
            var data = GetLowByte(rawData);
            return (Enums.RemoteOverrideFunction)data;
        }
    }
}
