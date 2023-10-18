namespace TekuSP.Drivers.ADS1015.Enums
{
    /// <summary>
    /// Mode setting for ADS1015
    /// </summary>
    public enum ModeSetting
    {
        /// <summary>
        /// Device operating mode: Continuous-conversion mode
        /// </summary>
        ADS_CONFIG_MODE_CONTINUOUS = 0x0000,

        /// <summary>
        ///  Device operating mode：Single-shot mode or power-down state <i>(default)</i>
        /// </summary>
        ADS_CONFIG_MODE_NOCONTINUOUS = 0x0100
    }
}