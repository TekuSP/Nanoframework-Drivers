namespace TekuSP.Drivers.ADS1015.Enums
{
    /// <summary>
    /// Commands for OS with ADS1015
    /// </summary>
    public enum OSCommands
    {
        /// <summary>
        /// Device is currently performing a conversion
        /// </summary>
        ADS_CONFIG_OS_BUSY = 0x0000,

        /// <summary>
        /// Device is not currently performing a conversion
        /// </summary>
        ADS_CONFIG_OS_NOBUSY = 0x8000,

        /// <summary>
        /// Start a single conversion (when in power-down state)
        /// </summary>
        ADS_CONFIG_OS_SINGLE_CONVERT = 0x8000,

        /// <summary>
        /// No effect
        /// </summary>
        ADS_CONFIG_OS_NO_EFFECT = 0x0000
    }
}