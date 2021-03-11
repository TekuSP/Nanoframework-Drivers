namespace ADS1015.Enums
{
    /// <summary>
    /// Gain setting for ADS1015
    /// </summary>
    public enum GainSetting
    {
        /// <summary>
        /// Gain= +/- 6.144V
        /// </summary>
        ADS_CONFIG_PGA_6144 = 0x0000,

        /// <summary>
        /// Gain= +/- 4.096V
        /// </summary>
        ADS_CONFIG_PGA_4096 = 0x0200,

        /// <summary>
        /// Gain= +/- 2.048V <i>(default)</i>
        /// </summary>
        ADS_CONFIG_PGA_2048 = 0x0400,

        /// <summary>
        /// Gain= +/- 1.024V
        /// </summary>
        ADS_CONFIG_PGA_1024 = 0x0600,

        /// <summary>
        /// Gain= +/- 0.512V
        /// </summary>
        ADS_CONFIG_PGA_512 = 0x0800,

        /// <summary>
        ///  Gain= +/- 0.256V
        /// </summary>
        ADS_CONFIG_PGA_256 = 0x0A00
    }
}