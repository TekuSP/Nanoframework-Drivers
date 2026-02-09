namespace TekuSP.Drivers.SHT3x.Enums
{
    /// <summary>
    /// Measurement delay in milliseconds for each repeatability level.
    /// </summary>
    public enum MeasurementDelayMs
    {
        /// <summary>
        /// High repeatability delay (16 ms).
        /// </summary>
        HighRepeatability = 16,

        /// <summary>
        /// Medium repeatability delay (7 ms).
        /// </summary>
        MediumRepeatability = 7,

        /// <summary>
        /// Low repeatability delay (5 ms).
        /// </summary>
        LowRepeatability = 5
    }
}
