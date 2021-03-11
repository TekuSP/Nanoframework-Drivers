namespace ADS1015.Enums
{
    /// <summary>
    /// Data rate setting for ADS1015
    /// </summary>
    public enum DataRateSetting
    {
        /// <summary>
        /// Data rate = 128SPS
        /// </summary>
        ADS_CONFIG_DR_RATE_128 = 0x0000,

        /// <summary>
        /// Data rate = 250SPS
        /// </summary>
        ADS_CONFIG_DR_RATE_250 = 0x0020,

        /// <summary>
        /// Data rate = 490SPS
        /// </summary>
        ADS_CONFIG_DR_RATE_490 = 0x0040,

        /// <summary>
        /// Data rate = 920SPS
        /// </summary>
        ADS_CONFIG_DR_RATE_920 = 0x0060,

        /// <summary>
        /// Data rate = 1600SPS <i>(default)</i>
        /// </summary>
        ADS_CONFIG_DR_RATE_1600 = 0x0080,

        /// <summary>
        /// Data rate = 2400SPS
        /// </summary>
        ADS_CONFIG_DR_RATE_2400 = 0x00A0,

        /// <summary>
        /// Data rate = 3300SPS
        /// </summary>
        ADS_CONFIG_DR_RATE_3300 = 0x00C0
    }
}