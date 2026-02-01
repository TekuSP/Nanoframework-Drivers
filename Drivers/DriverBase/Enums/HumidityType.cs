namespace TekuSP.Drivers.DriverBase.Enums
{
    /// <summary>
    /// Supported humidity unit types.
    /// </summary>
    public enum HumidityType
    {
        /// <summary>Relative humidity percentage (0-100%).</summary>
        Relative,
        /// <summary>Relative humidity in Q16 fixed-point format.</summary>
        RelativeQ16,
        /// <summary>Other or custom unit.</summary>
        Other = 100
    }
}