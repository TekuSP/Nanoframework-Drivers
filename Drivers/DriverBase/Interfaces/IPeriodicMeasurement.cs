using TekuSP.Drivers.DriverBase.Enums;

namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Provides a common surface for devices that support periodic measurements.
    /// </summary>
    public interface IPeriodicMeasurement
    {
        /// <summary>
        /// Gets a value indicating whether periodic measurement is running.
        /// </summary>
        bool IsPeriodicMeasurementRunning { get; }

        /// <summary>
        /// Gets the configured measurement rate.
        /// </summary>
        byte PeriodicMeasurementRate { get; }

        /// <summary>
        /// Starts periodic measurement at the specified rate.
        /// </summary>
        /// <param name="measurementRate">Measurement rate.</param>
        void StartPeriodicMeasurement(byte measurementRate);

        /// <summary>
        /// Stops periodic measurement.
        /// </summary>
        void StopPeriodicMeasurement();
    }
}
