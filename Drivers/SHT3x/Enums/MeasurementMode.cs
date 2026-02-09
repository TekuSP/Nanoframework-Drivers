namespace TekuSP.Drivers.SHT3x.Enums
{
    /// <summary>
    /// Measurement mode for single shot measurements.
    /// </summary>
    public enum MeasurementMode
    {
        /// <summary>
        /// Single shot measurement without clock stretching.
        /// </summary>
        SingleShot = 0,

        /// <summary>
        /// Single shot measurement with clock stretching.
        /// </summary>
        SingleShotClockStretching = 1
    }
}
