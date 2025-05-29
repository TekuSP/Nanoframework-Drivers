namespace TekuSP.Drivers.DriverBase.Interfaces
{
    public interface IHumiditySensor
    {
        #region Public Methods

        /// <summary>
        /// Calculates and converts humidity to target unit
        /// </summary>
        /// <param name="readHumidityType">Target unit to convert to</param>
        /// <param name="rawHumidity">Humidity to convert from, has to be in raw format</param>
        /// <returns>Returns humidity in target unit</returns>
        double CalculateHumidity(Enums.HumidityType readHumidityType, double rawHumidity);

        /// <summary>
        /// Read humidity from sensor, if supports <see cref="IAdvancedHumiditySensor"/> then use <see cref="IAdvancedHumiditySensor.HeatUp(int)"/> before reading
        /// </summary>
        /// <returns>Returns raw data from sensor</returns>
        double ReadHumidity();

        /// <summary>
        /// Read humidity from sensor and gets them with target type, if supports <see cref="IAdvancedHumiditySensor"/> then use <see cref="IAdvancedHumiditySensor.HeatUp(int)"/> before reading
        /// </summary>
        /// <param name="readHumidityType">Target type to process</param>
        /// <returns>Returns data from sensor processed to target type</returns>
        double ReadHumidity(Enums.HumidityType readHumidityType);

        #endregion Public Methods
    }
}