namespace TekuSP.Drivers.TCS34725.Enums
{
    /// <summary>
    /// Gain settings for the ALS.
    /// </summary>
    public enum Gain
    {
        /// <summary>1x gain.</summary>
        TCS34725_GAIN_1X = 0x00,
        /// <summary>4x gain.</summary>
        TCS34725_GAIN_4X = 0x01,
        /// <summary>16x gain.</summary>
        TCS34725_GAIN_16X = 0x02,
        /// <summary>60x gain.</summary>
        TCS34725_GAIN_60X = 0x03
    }
}
