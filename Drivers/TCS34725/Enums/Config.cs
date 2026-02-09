namespace TekuSP.Drivers.TCS34725.Enums
{
    /// <summary>
    /// Configuration register and flags.
    /// </summary>
    public enum Config
    {
        /// <summary>Configuration register address.</summary>
        TCS34725_CONFIG = 0x0D,
        /// <summary>Wait long flag.</summary>
        TCS34725_CONFIG_WLONG = 0x02
    }
}
