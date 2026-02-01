namespace TekuSP.Drivers.TCS34725.Enums
{
    /// <summary>
    /// Integration time settings.
    /// </summary>
    public enum IntegrationTime
    {
        /// <summary>2.4 ms integration time.</summary>
        TCS34725_INTEGRATIONTIME_2_4MS = 0xFF,
        /// <summary>24 ms integration time.</summary>
        TCS34725_INTEGRATIONTIME_24MS = 0xF6,
        /// <summary>50 ms integration time.</summary>
        TCS34725_INTEGRATIONTIME_50MS = 0xEB,
        /// <summary>101 ms integration time.</summary>
        TCS34725_INTEGRATIONTIME_101MS = 0xD5,
        /// <summary>154 ms integration time.</summary>
        TCS34725_INTEGRATIONTIME_154MS = 0xC0,
        /// <summary>700 ms integration time.</summary>
        TCS34725_INTEGRATIONTIME_700MS = 0x00
    }
}
