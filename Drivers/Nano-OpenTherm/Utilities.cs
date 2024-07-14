using System;

using TekuSP.Drivers.Nano_OpenTherm.Enums;

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
        /// <returns>Parity</returns>
        public static bool Parity(ulong frame)
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
        /// Get Uint from raw data
        /// </summary>
        /// <returns>Uint Response</returns>
        public static uint GetUInt(ulong rawData) => (uint)(rawData & 0xffff);
        /// <summary>
        /// Gets Int from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>Int Response</returns>
        public static int GetInt(ulong rawData) => (int)(rawData & 0xFFFFFFFF);
        /// <summary>
        /// Gets High part of Int from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>High of int</returns>
        public static short GetHighShort(ulong rawData)
        {
            var temp = GetInt(rawData);
            return (short)(temp >> 16);
        }
        /// <summary>
        /// Gets Low part of Int from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>Low of int</returns>
        public static short GetLowShort(ulong rawData)
        {
            var temp = GetInt(rawData);
            return (short)(temp & 0xFFFF);
        }
        /// <summary>
        /// Gets high part of UShort from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>High of uint</returns>
        public static ushort GetHighUShort(ulong rawData)
        {
            var temp = GetUInt(rawData);
            return (ushort)(temp >> 16);
        }
        /// <summary>
        /// Gets high part of UShort from raw data where low part is byte
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>High part of int, minus byte</returns>
        public static ushort GetHighUShortWithLowByte(ulong rawData)
        {
            var temp = GetUInt(rawData);
            return (ushort)((temp >> 8) & 0xFFFF);
        }
        /// <summary>
        /// Gets low part of UShort from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>Low part of uint</returns>
        public static ushort GetLowUShort(ulong rawData)
        {
            var temp = GetUInt(rawData);
            return (ushort)(temp & 0xFFFF);
        }
        /// <summary>
        /// Gets High part of Byte from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>High part of uint</returns>
        public static byte GetHighByte(ulong rawData)
        {
            ulong temp = GetUInt(rawData);
            return (byte)(temp >> 8);
        }
        /// <summary>
        /// Gets Low part of Byte from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>Low part of uint</returns>
        public static byte GetLowByte(ulong rawData)
        {
            var temp = GetUInt(rawData);
            return (byte)(temp & 0xFF);
        }
        /// <summary>
        /// Get Float from raw data
        /// </summary>
        /// <returns>Float Response</returns>
        public static float GetFloat(ulong rawData)
        {
            var temp = GetUInt(rawData);
            return (temp & 0x8000) == 1 ? -(0x10000L - temp) / 256.0f : temp / 256.0f;
        }
        /// <summary>
        /// Gets Special DateTime from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <param name="time">Time</param>
        /// <param name="dayOfWeek">Day of week</param>
        public static void GetDateTime(ulong rawData, out DateTime time, out DayOfWeek dayOfWeek)
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
        public static MasterStatus GetMasterStatus(ulong rawData)
        {
            var data = GetLowByte(rawData);
            return (MasterStatus)data;
        }
        /// <summary>
        /// Gets Slave Status from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>SlaveStatus</returns>
        public static SlaveStatus GetSlaveStatus(ulong rawData)
        {
            var data = GetHighByte(rawData);
            return (SlaveStatus)data;
        }
        /// <summary>
        /// Gets Master Configuration from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>MasterConfiguration</returns>
        public static MasterConfiguration GetMasterConfiguration(ulong rawData)
        {
            var data = GetLowByte(rawData);
            return (MasterConfiguration)data;
        }
        /// <summary>
        /// Gets Slave Configuration from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>SlaveConfiguration</returns>
        public static SlaveConfiguration GetSlaveConfiguration(ulong rawData)
        {
            var data = GetLowByte(rawData);
            return (SlaveConfiguration)data;
        }
        /// <summary>
        /// Gets Application Specific Fault Flags from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>ApplicationSpecificFaultFlags</returns>
        public static ApplicationSpecificFaultFlags GetApplicationSpecificFaultFlags(ulong rawData)
        {
            var data = GetLowByte(rawData);
            return (ApplicationSpecificFaultFlags)data;
        }
        /// <summary>
        /// Gets Remote Parameter Transfer Enable from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>RemoteParameterTransferEnable</returns>
        public static RemoteParameterTransferEnable GetRemoteParameterTransferEnable(ulong rawData)
        {
            var data = GetLowByte(rawData);
            return (RemoteParameterTransferEnable)data;
        }
        /// <summary>
        /// Gets Remote Parameter Transfer Read Write from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>RemoteParameterTransferReadWrite</returns>
        public static RemoteParameterTransferReadWrite GetRemoteParameterTransferReadWrite(ulong rawData)
        {
            var data = GetHighByte(rawData);
            return (RemoteParameterTransferReadWrite)data;
        }
        /// <summary>
        /// Gets Remote Override Function from raw data
        /// </summary>
        /// <param name="rawData">Raw data</param>
        /// <returns>RemoteOverrideFunction</returns>
        public static RemoteOverrideFunction GetRemoteOverrideFunction(ulong rawData)
        {
            var data = GetLowByte(rawData);
            return (RemoteOverrideFunction)data;
        }
    }
}
