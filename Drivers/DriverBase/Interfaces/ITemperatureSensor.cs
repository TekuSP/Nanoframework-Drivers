namespace TekuSP.Drivers.DriverBase.Interfaces
{
    public interface ITemperatureSensor
    {
        /// <summary>
        /// Read temperature from sensor
        /// </summary>
        /// <returns>Returns raw data from sensor</returns>

        #region Public Methods

        /// <summary>
        /// Calculates and converts temperature to target unit
        /// </summary>
        /// <param name="readTemperatureUnit">Target unit to convert to</param>
        /// <param name="rawTemperature">Temperature to convert from, has to be in raw format</param>
        /// <returns>Returns temperature in target unit</returns>
        double CalculateTemperature(Enums.TemperatureUnit readTemperatureUnit, double rawTemperature);

        double ReadTemperature();

        /// <summary>
        /// Read temperature from sensor and convert to target unit
        /// </summary>
        /// <param name="readTemperatureUnit">Target unit to convert to</param>
        /// <returns>Returns data from sensor in target unit</returns>

        double ReadTemperature(Enums.TemperatureUnit readTemperatureUnit);

        #endregion Public Methods
    }
}