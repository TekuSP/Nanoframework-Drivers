namespace TekuSP.Drivers.ADS1015.Enums
{
    /// <summary>
    /// Comparator mode settings for ADS1015 
    /// </summary>
    public enum ComparatorMode
    {
        /// <summary>
        /// Comparator mode： Window comparator
        /// </summary>
        ADS_CONFIG_COMP_MODE_WINDOW = 0x0010,
        /// <summary>
        /// Comparator mode：Traditional comparator <i>(default)</i>
        /// </summary>
        ADS_CONFIG_COMP_MODE_TRADITIONAL = 0x0000
    }
    /// <summary>
    /// Comparator polarity settings for ADS1015 
    /// </summary>
    public enum ComparatorPolarity
    {
        /// <summary>
        /// Comparator polarity： Active low <i>(default)</i>
        /// </summary>
        ADS_CONFIG_COMP_POL_LOW = 0x0000,
        /// <summary>
        /// Comparator polarity： Active high
        /// </summary>
        ADS_CONFIG_COMP_POL_HIGH = 0x0008
    }
    /// <summary>
    /// Comparator latching settings for ADS1015 
    /// </summary>
    public enum ComparatorLatching
    {
        /// <summary>
        /// Latching comparator
        /// </summary>
        ADS_CONFIG_COMP_LAT = 0x0004,
        /// <summary>
        /// Nonlatching comparator <i>(default)</i>
        /// </summary>
        ADS_CONFIG_COMP_NONLAT = 0x0000
    }
    /// <summary>
    /// Comparator assert settings for ADS1015 
    /// </summary>
    public enum ComparatorAssert
    {
        /// <summary>
        /// Assert after one conversion
        /// </summary>
        ADS_CONFIG_COMP_QUE_ONE = 0x0000,
        /// <summary>
        /// Assert after two conversions
        /// </summary>
        ADS_CONFIG_COMP_QUE_TWO = 0x0001,
        /// <summary>
        /// Assert after four conversions
        /// </summary>
        ADS_CONFIG_COMP_QUE_FOUR = 0x0002,
        /// <summary>
        /// Disable comparator and set ALERT/RDY pin to high-impedance <i>(default)</i>
        /// </summary>
        ADS_CONFIG_COMP_QUE_NON = 0x0003
    }
}
