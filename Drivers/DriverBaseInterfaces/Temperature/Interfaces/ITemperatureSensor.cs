using UnitsNet;
using UnitsNet.Units;

namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Basic temperature sensor interface.
    /// </summary>
    public interface ITemperatureSensor
    {
        #region Public Methods

        /// <summary>
        /// Calculates and converts temperature to target unit
        /// </summary>
        /// <param name="readTemperatureUnit">Target unit to convert to.</param>
        /// <param name="rawTemperature">Temperature to convert from, has to be in raw format</param>
        /// <returns>Returns temperature in target unit.</returns>
        Temperature CalculateTemperature(TemperatureUnit readTemperatureUnit, double rawTemperature);

        /// <summary>
        /// Read temperature from sensor (raw).
        /// </summary>
        /// <returns>Returns raw data from sensor.</returns>
        double ReadTemperature();

        /// <summary>
        /// Read temperature from sensor and convert to target unit.
        /// </summary>
        /// <param name="readTemperatureUnit">Target unit to convert to.</param>
        /// <returns>Returns data from sensor in target unit.</returns>
        Temperature ReadTemperature(TemperatureUnit readTemperatureUnit);

        #endregion Public Methods
    }
}
