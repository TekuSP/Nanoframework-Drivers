namespace TekuSP.Drivers.TCS34725.Enums
{
    /// <summary>
    /// Data register addresses for color channels.
    /// </summary>
    public enum Data
    {
        /// <summary>Clear data low byte.</summary>
        TCS34725_CDATAL = 0x14,
        /// <summary>Clear data high byte.</summary>
        TCS34725_CDATAH = 0x15,
        /// <summary>Red data low byte.</summary>
        TCS34725_RDATAL = 0x16,
        /// <summary>Red data high byte.</summary>
        TCS34725_RDATAH = 0x17,
        /// <summary>Green data low byte.</summary>
        TCS34725_GDATAL = 0x18,
        /// <summary>Green data high byte.</summary>
        TCS34725_GDATAH = 0x19,
        /// <summary>Blue data low byte.</summary>
        TCS34725_BDATAL = 0x1A,
        /// <summary>Blue data high byte.</summary>
        TCS34725_BDATAH = 0x1B
    }
}
