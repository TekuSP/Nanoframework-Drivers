namespace TekuSP.Drivers.SHT3x.Constants
{
    /// <summary>
    /// SHT3x constant values.
    /// </summary>
    public static class SHT3xConstants
    {
        /// <summary>
        /// Maximum read retries for single-shot measurements.
        /// </summary>
        public const int MaxReadRetries = 10;

        /// <summary>
        /// Status register: alert pending flag.
        /// </summary>
        public const ushort StatusAlertPendingMask = 1 << 15;

        /// <summary>
        /// Status register: heater on flag.
        /// </summary>
        public const ushort StatusHeaterOnMask = 1 << 13;

        /// <summary>
        /// Status register: humidity tracking alert flag.
        /// </summary>
        public const ushort StatusHumidityTrackingAlertMask = 1 << 11;

        /// <summary>
        /// Status register: temperature tracking alert flag.
        /// </summary>
        public const ushort StatusTemperatureTrackingAlertMask = 1 << 10;

        /// <summary>
        /// Status register: system reset detected flag.
        /// </summary>
        public const ushort StatusSystemResetMask = 1 << 4;

        /// <summary>
        /// Status register: command status flag.
        /// </summary>
        public const ushort StatusCommandStatusMask = 1 << 1;

        /// <summary>
        /// Status register: write CRC status flag.
        /// </summary>
        public const ushort StatusWriteCrcStatusMask = 1 << 0;
    }
}
