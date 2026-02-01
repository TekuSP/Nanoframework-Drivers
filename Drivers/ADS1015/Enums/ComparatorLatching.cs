namespace TekuSP.Drivers.ADS1015.Enums
{
    /// <summary>
    /// Comparator latching settings for ADS1015.
    /// </summary>
    public enum ComparatorLatching
    {
        /// <summary>
        /// Latching comparator.
        /// </summary>
        ADS_CONFIG_COMP_LAT = 0x0004,
        /// <summary>
        /// Nonlatching comparator <i>(default)</i>.
        /// </summary>
        ADS_CONFIG_COMP_NONLAT = 0x0000
    }
}
