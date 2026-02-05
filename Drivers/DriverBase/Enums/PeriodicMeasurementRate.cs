namespace TekuSP.Drivers.DriverBase.Enums
{
    /// <summary>
    /// Common periodic measurement rates.
    /// </summary>
    public enum PeriodicMeasurementRate
    {
        /// <summary>
        /// One measurement every two seconds (0.5 Hz).
        /// </summary>
        EveryTwoSeconds = 0,

        /// <summary>
        /// One measurement per second (1 Hz).
        /// </summary>
        OnePerSecond = 1,

        /// <summary>
        /// Two measurements per second (2 Hz).
        /// </summary>
        TwoPerSecond = 2,

        /// <summary>
        /// Four measurements per second (4 Hz).
        /// </summary>
        FourPerSecond = 4,

        /// <summary>
        /// Ten measurements per second (10 Hz).
        /// </summary>
        TenPerSecond = 10
    }
}
