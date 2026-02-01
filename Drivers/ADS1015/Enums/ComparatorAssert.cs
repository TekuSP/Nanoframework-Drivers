namespace TekuSP.Drivers.ADS1015.Enums
{
    /// <summary>
    /// Comparator assert settings for ADS1015.
    /// </summary>
    public enum ComparatorAssert
    {
        /// <summary>
        /// Assert after one conversion.
        /// </summary>
        ADS_CONFIG_COMP_QUE_ONE = 0x0000,
        /// <summary>
        /// Assert after two conversions.
        /// </summary>
        ADS_CONFIG_COMP_QUE_TWO = 0x0001,
        /// <summary>
        /// Assert after four conversions.
        /// </summary>
        ADS_CONFIG_COMP_QUE_FOUR = 0x0002,
        /// <summary>
        /// Disable comparator and set ALERT/RDY pin to high-impedance <i>(default)</i>.
        /// </summary>
        ADS_CONFIG_COMP_QUE_NON = 0x0003
    }
}
