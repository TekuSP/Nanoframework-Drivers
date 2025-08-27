using System;

using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm
{
    public static class Utilities
    {
        #region Public Methods

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

        /// <summary>Extract day of month (1..31) from a date payload.</summary>
        public static byte GetDateDay(uint rawData) => (byte)(GetLowByte(rawData) & 0x1F);

        /// <summary>Extract month (1..12) from a date payload.</summary>
        public static Enums.Month GetDateMonth(uint rawData) => (Enums.Month)(GetHighByte(rawData) & 0x1F);

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

        /// <summary>Extract <see cref="System.DayOfWeek"/> from a day/time payload.</summary>
        public static DayOfWeek GetDayOfWeekFromDayTime(uint rawData) => (DayOfWeek)((GetLowByte(rawData) >> 5) & 0x07);

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

        /// <summary>Extract hour (0..23) from a day/time payload.</summary>
        public static byte GetHourFromDayTime(uint rawData) => (byte)(GetLowByte(rawData) & 0x1F);

        /// <summary>
        /// Gets Int from raw data (low 32-bits)
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>Int Response</returns>
        public static int GetInt(uint rawData) => (int)(rawData & 0xFFFFFFFFu);

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
        /// Gets low part of UShort from raw data (bits 15..0)
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>Low part of uint</returns>
        public static ushort GetLowUShort(uint rawData) => (ushort)(rawData & 0xFFFF);

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
        /// Extracts MessageID (bits 16..23) from a 32-bit OpenTherm frame.
        /// </summary>
        public static MessageID GetMessageID(uint rawData) => (MessageID)((rawData >> 16) & 0xFF);

        // ---------------------------
        // Header helpers (type/id)
        // ---------------------------
        /// <summary>
        /// Extracts MessageType (bits 28..30) from a 32-bit OpenTherm frame.
        /// </summary>
        public static MessageType GetMessageType(uint rawData) => (MessageType)((rawData >> 28) & 0x7);

        /// <summary>Extract minute (0..59) from a day/time payload.</summary>
        public static byte GetMinuteFromDayTime(uint rawData) => (byte)(GetHighByte(rawData) & 0x3F);

        /// <summary>Get OperatingMode for Domestic Hot Water (DHW) from raw data.</summary>
        public static Enums.OperatingMode GetOperatingModeDHW(uint rawData)
        {
            var b = GetLowByte(rawData);
            return (Enums.OperatingMode)((b >> 6) & 0x03);
        }

        /// <summary>Get OperatingMode for Heating Circuit 1 (HC1) from raw data.</summary>
        public static Enums.OperatingMode GetOperatingModeHC1(uint rawData)
        {
            var b = GetLowByte(rawData);
            return (Enums.OperatingMode)(b & 0x03);
        }

        /// <summary>Get OperatingMode for Heating Circuit 2 (HC2) from raw data.</summary>
        public static Enums.OperatingMode GetOperatingModeHC2(uint rawData)
        {
            var b = GetLowByte(rawData);
            return (Enums.OperatingMode)((b >> 3) & 0x03);
        }

        /// <summary>
        /// Unpack operating modes from the low data byte: HC1 bits 0..1, HC2 bits 3..4, DHW bits 6..7.
        /// </summary>
        /// <param name="rawData">Raw 32-bit frame value (only low data byte is used).</param>
        /// <param name="hc1">Out: Operating mode for Heating Circuit 1.</param>
        /// <param name="hc2">Out: Operating mode for Heating Circuit 2.</param>
        /// <param name="dhw">Out: Operating mode for Domestic Hot Water.</param>
        public static void GetOperatingModes(uint rawData, out Enums.OperatingMode hc1, out Enums.OperatingMode hc2, out Enums.OperatingMode dhw)
        {
            var b = GetLowByte(rawData);
            hc1 = (Enums.OperatingMode)(b & 0x03);
            hc2 = (Enums.OperatingMode)((b >> 3) & 0x03);
            dhw = (Enums.OperatingMode)((b >> 6) & 0x03);
        }

        /// <summary>
        /// Alias for percentage decoding from 8.8 fixed-point.
        /// </summary>
        public static float GetPercentage(uint rawData) => GetFloat(rawData);

        /// <summary>
        /// Alias for percentage encoding (8.8 fixed-point, clamped 0..100).
        /// </summary>
        public static uint GetRawPercentage(float percent) => GetRawTemperature(percent);

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
        /// Gets Remote Override Function from raw data
        /// </summary>
        /// <param name="rawData">Raw data</param>
        /// <returns>RemoteOverrideFunction</returns>
        public static Enums.RemoteOverrideFunction GetRemoteOverrideFunction(uint rawData)
        {
            var data = GetLowByte(rawData);
            return (Enums.RemoteOverrideFunction)data;
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
        /// Gets Slave Status from raw data
        /// </summary>
        /// <param name="rawData">Raw Data</param>
        /// <returns>SlaveStatus</returns>
        public static Enums.SlaveStatus GetSlaveStatus(uint rawData)
        {
            var data = GetHighByte(rawData);
            return (Enums.SlaveStatus)data;
        }

        //TODO: Move to base solution
        /// <summary>
        /// Get Uint from raw data (low 16-bits data field)
        /// </summary>
        /// <returns>Uint Response</returns>
        public static uint GetUInt(uint rawData) => rawData & 0xffffu;

        // ---------------------------
        // Flag helpers (set/get)
        // ---------------------------
        /// <summary>Checks if a MasterStatus flag is set.</summary>
        public static bool IsSet(this Enums.MasterStatus value, Enums.MasterStatus flag) => (value & flag) == flag;

        /// <summary>Checks if a SlaveStatus flag is set.</summary>
        public static bool IsSet(this Enums.SlaveStatus value, Enums.SlaveStatus flag) => (value & flag) == flag;

        /// <summary>Checks if a MasterConfiguration flag is set.</summary>
        public static bool IsSet(this Enums.MasterConfiguration value, Enums.MasterConfiguration flag) => (value & flag) == flag;

        /// <summary>Checks if a SlaveConfiguration flag is set.</summary>
        public static bool IsSet(this Enums.SlaveConfiguration value, Enums.SlaveConfiguration flag) => (value & flag) == flag;

        /// <summary>Checks if an ApplicationSpecificFaultFlags flag is set.</summary>
        public static bool IsSet(this Enums.ApplicationSpecificFaultFlags value, Enums.ApplicationSpecificFaultFlags flag) => (value & flag) == flag;

        /// <summary>Checks if a RemoteParameterTransferEnable flag is set.</summary>
        public static bool IsSet(this Enums.RemoteParameterTransferEnable value, Enums.RemoteParameterTransferEnable flag) => (value & flag) == flag;

        /// <summary>Checks if a RemoteParameterTransferReadWrite flag is set.</summary>
        public static bool IsSet(this Enums.RemoteParameterTransferReadWrite value, Enums.RemoteParameterTransferReadWrite flag) => (value & flag) == flag;

        /// <summary>Checks if a RemoteOverrideFunction flag is set.</summary>
        public static bool IsSet(this Enums.RemoteOverrideFunction value, Enums.RemoteOverrideFunction flag) => (value & flag) == flag;

        /// <summary>
        /// Compose a 16-bit payload from high and low bytes.
        /// </summary>
        public static ushort MakeUShort(byte high, byte low) => (ushort)((high << 8) | low);

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

        /// <summary>Convert <see cref="Enums.ApplicationSpecificFaultFlags"/> flags to byte.</summary>
        public static byte SetApplicationSpecificFaultFlags(Enums.ApplicationSpecificFaultFlags value) => (byte)value;

        /// <summary>
        /// Compose a date payload with day (low 5 bits of low byte) and month (low 5 bits of high byte).
        /// </summary>
        public static ushort SetDate(byte day, Enums.Month month)
        {
            byte low = (byte)(day & 0x1F);
            byte high = (byte)(((byte)month) & 0x1F);
            return MakeUShort(high, low);
        }

        /// <summary>
        /// Compose a day/time payload: low byte = (dayOfWeek<<5)|(hour&0x1F), high byte = (minute&0x3F).
        /// </summary>
        public static ushort SetDayTime(DayOfWeek dayOfWeek, byte hour, byte minute)
        {
            byte low = (byte)(((int)dayOfWeek & 0x07) << 5 | (hour & 0x1F));
            byte high = (byte)(minute & 0x3F);
            return MakeUShort(high, low);
        }

        /// <summary>Sets or clears a MasterStatus flag by reference.</summary>
        public static Enums.MasterStatus SetFlag(this Enums.MasterStatus value, Enums.MasterStatus flag, bool set)
        { if (set) value |= flag; else value &= ~flag; return value; }

        /// <summary>Sets or clears a SlaveStatus flag by reference.</summary>
        public static Enums.SlaveStatus SetFlag(this Enums.SlaveStatus value, Enums.SlaveStatus flag, bool set)
        { if (set) value |= flag; else value &= ~flag; return value; }

        /// <summary>Sets or clears a MasterConfiguration flag by reference.</summary>
        public static Enums.MasterConfiguration SetFlag(this Enums.MasterConfiguration value, Enums.MasterConfiguration flag, bool set)
        { if (set) value |= flag; else value &= ~flag; return value; }

        /// <summary>Sets or clears a SlaveConfiguration flag by reference.</summary>
        public static Enums.SlaveConfiguration SetFlag(this Enums.SlaveConfiguration value, Enums.SlaveConfiguration flag, bool set)
        { if (set) value |= flag; else value &= ~flag; return value; }

        /// <summary>Sets or clears an ApplicationSpecificFaultFlags flag by reference.</summary>
        public static Enums.ApplicationSpecificFaultFlags SetFlag(this Enums.ApplicationSpecificFaultFlags value, Enums.ApplicationSpecificFaultFlags flag, bool set)
        { if (set) value |= flag; else value &= ~flag; return value; }

        /// <summary>Sets or clears a RemoteParameterTransferEnable flag by reference.</summary>
        public static Enums.RemoteParameterTransferEnable SetFlag(this Enums.RemoteParameterTransferEnable value, Enums.RemoteParameterTransferEnable flag, bool set)
        { if (set) value |= flag; else value &= ~flag; return value; }

        /// <summary>Sets or clears a RemoteParameterTransferReadWrite flag by reference.</summary>
        public static Enums.RemoteParameterTransferReadWrite SetFlag(this Enums.RemoteParameterTransferReadWrite value, Enums.RemoteParameterTransferReadWrite flag, bool set)
        { if (set) value |= flag; else value &= ~flag; return value; }

        /// <summary>Sets or clears a RemoteOverrideFunction flag by reference.</summary>
        public static Enums.RemoteOverrideFunction SetFlag(this Enums.RemoteOverrideFunction value, Enums.RemoteOverrideFunction flag, bool set)
        { if (set) value |= flag; else value &= ~flag; return value; }

        /// <summary>
        /// Replace the high byte of a 16-bit value.
        /// </summary>
        public static ushort SetHighByte(ushort data, byte high) => (ushort)((data & 0x00FF) | (high << 8));

        /// <summary>
        /// Replace the low byte of a 16-bit value.
        /// </summary>
        public static ushort SetLowByte(ushort data, byte low) => (ushort)((data & 0xFF00) | low);

        /// <summary>Convert <see cref="Enums.MasterConfiguration"/> flags to byte.</summary>
        public static byte SetMasterConfiguration(Enums.MasterConfiguration value) => (byte)value;

        /// <summary>Convert <see cref="Enums.MasterStatus"/> flags to byte.</summary>
        public static byte SetMasterStatus(Enums.MasterStatus value) => (byte)value;

        /// <summary>
        /// Pack operating modes into the low data byte: HC1 bits 0..1, HC2 bits 3..4, DHW bits 6..7.
        /// </summary>
        /// <param name="hc1">Operating mode for Heating Circuit 1.</param>
        /// <param name="hc2">Operating mode for Heating Circuit 2.</param>
        /// <param name="dhw">Operating mode for Domestic Hot Water.</param>
        /// <returns>Composed low byte containing the three 2-bit operating modes.</returns>
        public static byte SetOperatingModes(Enums.OperatingMode hc1, Enums.OperatingMode hc2, Enums.OperatingMode dhw)
            => (byte)(((byte)hc1 & 0x03)
                | (((byte)hc2 & 0x03) << 3)
                | (((byte)dhw & 0x03) << 6));

        /// <summary>Convert <see cref="Enums.RemoteOverrideFunction"/> flags to byte.</summary>
        public static byte SetRemoteOverrideFunction(Enums.RemoteOverrideFunction value) => (byte)value;

        /// <summary>Convert <see cref="Enums.RemoteParameterTransferEnable"/> flags to byte.</summary>
        public static byte SetRemoteParameterTransferEnable(Enums.RemoteParameterTransferEnable value) => (byte)value;

        /// <summary>Convert <see cref="Enums.RemoteParameterTransferReadWrite"/> flags to byte.</summary>
        public static byte SetRemoteParameterTransferReadWrite(Enums.RemoteParameterTransferReadWrite value) => (byte)value;

        /// <summary>Convert <see cref="Enums.SlaveConfiguration"/> flags to byte.</summary>
        public static byte SetSlaveConfiguration(Enums.SlaveConfiguration value) => (byte)value;

        /// <summary>Convert <see cref="Enums.SlaveStatus"/> flags to byte.</summary>
        public static byte SetSlaveStatus(Enums.SlaveStatus value) => (byte)value;

        #endregion Public Methods
    }
}