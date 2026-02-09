namespace TekuSP.Drivers.SHT3x
{
    /// <summary>
    /// Parsed SHT3x status register information.
    /// </summary>
    public struct SHT3xStatus
    {
        /// <summary>
        /// Gets or sets the raw status register value.
        /// </summary>
        public ushort RawValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if an alert is pending.
        /// </summary>
        public bool AlertPending { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the heater is active.
        /// </summary>
        public bool HeaterActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if a humidity tracking alert is active.
        /// </summary>
        public bool HumidityTrackingAlert { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if a temperature tracking alert is active.
        /// </summary>
        public bool TemperatureTrackingAlert { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if a system reset was detected.
        /// </summary>
        public bool SystemResetDetected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the last command failed.
        /// </summary>
        public bool CommandStatusError { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the last write CRC check failed.
        /// </summary>
        public bool WriteDataCrcError { get; set; }
    }
}
