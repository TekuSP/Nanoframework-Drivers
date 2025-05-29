namespace TekuSP.Drivers.ADS1015.Enums
{
    /// <summary>
    /// Setting multiplex for ADS1015
    /// </summary>
    public enum MultiplexerSetting
    {
        /// <summary>
        ///  Input multiplexer, AINP = AIN0 and AINN = AIN1 <i>(default)</i>
        /// </summary>
        ADS_CONFIG_MUX_MUL_0_1 = 0x0000,

        /// <summary>
        /// Input multiplexer, AINP = AIN0 and AINN = AIN3
        /// </summary>
        ADS_CONFIG_MUX_MUL_0_3 = 0x1000,

        /// <summary>
        ///  Input multiplexer, AINP = AIN1 and AINN = AIN3
        /// </summary>
        ADS_CONFIG_MUX_MUL_1_3 = 0x2000,

        /// <summary>
        ///  Input multiplexer, AINP = AIN2 and AINN = AIN3
        /// </summary>
        ADS_CONFIG_MUX_MUL_2_3 = 0x3000,

        /// <summary>
        /// SINGLE, AIN0
        /// </summary>
        ADS_CONFIG_MUX_SINGLE_0 = 0x4000,

        /// <summary>
        /// SINGLE, AIN1
        /// </summary>
        ADS_CONFIG_MUX_SINGLE_1 = 0x5000,

        /// <summary>
        ///  SINGLE, AIN2
        /// </summary>
        ADS_CONFIG_MUX_SINGLE_2 = 0x6000,

        /// <summary>
        ///  SINGLE, AIN3
        /// </summary>
        ADS_CONFIG_MUX_SINGLE_3 = 0x7000
    }
}