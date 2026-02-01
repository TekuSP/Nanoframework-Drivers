namespace TekuSP.Drivers.ADS1015.Enums
{
    /// <summary>
    /// Comparator mode settings for ADS1015.
    /// </summary>
    public enum ComparatorMode
    {
        /// <summary>
        /// Comparator mode: window comparator.
        /// </summary>
        ADS_CONFIG_COMP_MODE_WINDOW = 0x0010,
        /// <summary>
        /// Comparator mode: traditional comparator <i>(default)</i>.
        /// </summary>
        ADS_CONFIG_COMP_MODE_TRADITIONAL = 0x0000
    }
}
