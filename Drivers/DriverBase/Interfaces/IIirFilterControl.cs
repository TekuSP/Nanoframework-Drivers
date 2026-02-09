using System;

namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Generic interface to control an IIR filter setting on a sensor.
    /// Uses raw byte values so it can be implemented by multiple drivers.
    /// </summary>
    public interface IIirFilterControl
    {
        /// <summary>
        /// Sets the IIR filter coefficient register value (driver-specific encoding).
        /// </summary>
        /// <param name="filter">Raw filter value (device encoding).</param>
        void SetIirFilter(byte filter);

        /// <summary>
        /// Gets the currently configured raw IIR filter value.
        /// </summary>
        byte GetIirFilter();
    }
}