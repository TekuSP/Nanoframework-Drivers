namespace TekuSP.Drivers.ADS1015.Enums
{
    /// <summary>
    /// Comparator polarity settings for ADS1015.
    /// </summary>
    public enum ComparatorPolarity
    {
        /// <summary>
        /// Comparator polarity: active low <i>(default)</i>.
        /// </summary>
        ADS_CONFIG_COMP_POL_LOW = 0x0000,
        /// <summary>
        /// Comparator polarity: active high.
        /// </summary>
        ADS_CONFIG_COMP_POL_HIGH = 0x0008
    }
}
