using System;

namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Generic interface to control oversampling settings on a sensor.
    /// Uses raw byte values to avoid introducing device-specific enums into DriverBase.
    /// </summary>
    public interface IOversamplingControl
    {
        /// <summary>
        /// Sets the oversampling values for temperature and pressure sensors.
        /// Values are device-specific encodings (typically 0..7).
        /// </summary>
        /// <param name="temp">Temperature oversampling encoding.</param>
        /// <param name="press">Pressure oversampling encoding.</param>
        void SetOversampling(byte temp, byte press);

        /// <summary>
        /// Gets the currently configured oversampling values (raw encodings).
        /// </summary>
        void GetOversampling(out byte temp, out byte press);
    }
}