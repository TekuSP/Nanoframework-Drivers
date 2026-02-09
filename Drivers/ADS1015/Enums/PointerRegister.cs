namespace TekuSP.Drivers.ADS1015.Enums
{
    /// <summary>
    /// ADS1015 pointer register addresses.
    /// </summary>
    public enum PointerRegister
    {
        /// <summary>Conversion register.</summary>
        ADS_POINTER_CONVERT = 0x00,
        /// <summary>Configuration register.</summary>
        ADS_POINTER_CONFIG = 0x01,
        /// <summary>Low threshold register.</summary>
        ADS_POINTER_LOWTHRESH = 0x02,
        /// <summary>High threshold register.</summary>
        ADS_POINTER_HIGHTHRESH = 0x03
    }
}
