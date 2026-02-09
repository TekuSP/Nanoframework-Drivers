namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Provides a generic status interface for devices exposing a raw status value.
    /// </summary>
    public interface IStatusProvider
    {
        /// <summary>
        /// Gets a value indicating whether the device reports an error condition.
        /// </summary>
        bool HasErrors { get; }

        /// <summary>
        /// Gets a status code representing the last device status evaluation.
        /// </summary>
        int StatusCode { get; }

        /// <summary>
        /// Gets a human-readable summary of the current status.
        /// </summary>
        string StatusSummary { get; }

        /// <summary>
        /// Gets the raw status value as reported by the device.
        /// </summary>
        uint RawStatus { get; }

        /// <summary>
        /// Refreshes the cached status from the device.
        /// </summary>
        void RefreshStatus();
    }
}
