namespace TekuSP.Drivers.QMP6988.Enums
{
    /// <summary>
    /// Oversampling settings for Pressure and Temperature.
    /// <para>
    /// Higher oversampling reduces noise (increases resolution) but increases current consumption 
    /// and measurement time.
    /// </para>
    /// </summary>
    public enum Oversampling : byte
    {
        /// <summary>
        /// Measurement is skipped.
        /// <para>Output data is set to 0x80000. No measurement is performed.</para>
        /// </summary>
        Skipped = 0x0,

        /// <summary>
        /// 1x Oversampling (Ultra Low Power).
        /// <para>Fastest measurement, highest noise. 16-bit resolution.</para>
        /// </summary>
        x1 = 0x1,

        /// <summary>
        /// 2x Oversampling (Low Power).
        /// <para>Reduces noise by approx 30%. 17-bit resolution.</para>
        /// </summary>
        x2 = 0x2,

        /// <summary>
        /// 4x Oversampling (Standard Resolution).
        /// <para>Recommended for handheld devices. 18-bit resolution.</para>
        /// </summary>
        x4 = 0x3,

        /// <summary>
        /// 8x Oversampling (High Resolution).
        /// <para>19-bit resolution.</para>
        /// </summary>
        x8 = 0x4,

        /// <summary>
        /// 16x Oversampling (Ultra High Resolution).
        /// <para>20-bit resolution.</para>
        /// </summary>
        x16 = 0x5,

        /// <summary>
        /// 32x Oversampling.
        /// <para>Highest precision, slowest measurement time.</para>
        /// </summary>
        x32 = 0x6,

        /// <summary>
        /// 64x Oversampling.
        /// <para>Maximum possible noise reduction.</para>
        /// </summary>
        x64 = 0x7
    }
}
