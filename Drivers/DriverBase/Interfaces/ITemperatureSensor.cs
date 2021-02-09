namespace DriverBase.Interfaces
{
    public interface ITemperatureSensor
    {
        #region Public Methods
        /// <summary>
        /// Read temperature from sensor
        /// </summary>
        /// <returns>Returns raw data from sensor</returns>

        float ReadTemperature();
        /// <summary>
        /// Read temperature from sensor and convert to target unit
        /// </summary>
        /// <param name="readTemperatureUnit">Target unit to convert to</param>
        /// <returns>Returns data from sensor in target unit</returns>

        float ReadTemperature(Enums.TemperatureUnit readTemperatureUnit);
        /// <summary>
        /// Calculates and converts temperature to target unit
        /// </summary>
        /// <param name="readTemperatureUnit">Target unit to convert to</param>
        /// <param name="rawTemperature">Temperature to convert from, has to be in raw format</param>
        /// <returns>Returns temperature in target unit</returns>
        float CalculateTemperature(Enums.TemperatureUnit readTemperatureUnit, float rawTemperature);

        #endregion Public Methods
    }
}