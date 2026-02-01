namespace TekuSP.Drivers.TCS34725.Enums
{
    /// <summary>
    /// Status register address and flags.
    /// </summary>
    public enum Status
    {
        /// <summary>Status register address.</summary>
        TCS34725_STATUS = 0x13,
        /// <summary>ALS interrupt flag.</summary>
        TCS34725_STATUS_AINT = 0x10,
        /// <summary>ALS valid data flag.</summary>
        TCS34725_STATUS_AVALID = 0x01
    }
}
