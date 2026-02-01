namespace TekuSP.Drivers.TCS34725
{
    /// <summary>
    /// TCS34725 color temperature and lux calculation coefficients.
    /// </summary>
    public static class Offsets
    {
        /// <summary>Red channel coefficient.</summary>
        public static readonly float TCS34725_R_Coef = 0.136f;
        /// <summary>Green channel coefficient.</summary>
        public static readonly float TTCS34725_G_Coef = 1.000f;
        /// <summary>Blue channel coefficient.</summary>
        public static readonly float TCS34725_B_Coef = -0.444f;
        /// <summary>Glass attenuation factor.</summary>
        public static readonly float TCS34725_GA = 1.0f;
        /// <summary>Device factor.</summary>
        public static readonly float TCS34725_DF = 310.0f;
        /// <summary>Color temperature coefficient.</summary>
        public static readonly float TCS34725_CT_Coef = 3810.0f;
        /// <summary>Color temperature offset.</summary>
        public static readonly float TCS34725_CT_Offset = 1391.0f;
    }
}
