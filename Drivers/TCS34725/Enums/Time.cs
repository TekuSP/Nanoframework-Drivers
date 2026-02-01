namespace TekuSP.Drivers.TCS34725.Enums
{
    /// <summary>
    /// Timing register addresses and wait time values.
    /// </summary>
    public enum Time
    {
        /// <summary>ATIME register address.</summary>
        TCS34725_ATIME = 0x01,
        /// <summary>WTIME register address.</summary>
        TCS34725_WTIME = 0x03,
        /// <summary>2.4 ms wait time.</summary>
        TCS34725_WTIME_2_4MS = 0xFF,
        /// <summary>204 ms wait time.</summary>
        TCS34725_WTIME_204MS = 0xAB,
        /// <summary>614 ms wait time.</summary>
        TCS34725_WTIME_614MS = 0x00
    }
}
