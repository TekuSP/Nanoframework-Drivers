namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Provides control for sensors with an internal heater.
    /// </summary>
    public interface IHeaterControl
    {
        /// <summary>
        /// Gets a value indicating whether the heater is currently active.
        /// </summary>
        bool IsHeaterActive { get; }

        /// <summary>
        /// Enables the internal heater.
        /// </summary>
        void EnableHeater();

        /// <summary>
        /// Disables the internal heater.
        /// </summary>
        void DisableHeater();
    }
}
