namespace TekuSP.Drivers.TCS34725.Enums
{
    /// <summary>
    /// Enable register address and flags.
    /// </summary>
    public enum Enable
    {
        /// <summary>Enable register address.</summary>
        TCS34725_ENABLE = 0x00,
        /// <summary>ALS interrupt enable.</summary>
        TCS34725_ENABLE_AIEN = 0x10,
        /// <summary>Wait enable.</summary>
        TCS34725_ENABLE_WEN = 0x08,
        /// <summary>ALS enable.</summary>
        TCS34725_ENABLE_AEN = 0x02,
        /// <summary>Power on.</summary>
        TCS34725_ENABLE_PON = 0x01
    }
}
