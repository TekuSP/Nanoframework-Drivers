namespace DriverBase.Interfaces
{
    public interface IHumiditySensor
    {
        #region Public Methods
        /// <summary>
        /// Read humidity from sensor
        /// </summary>
        /// <returns>Returns raw data from sensor</returns>
        float ReadHumidity();
        /// <summary>
        /// Read humidity from sensor and gets them with target type
        /// </summary>
        /// <param name="readHumidityType">Target type to process</param>
        /// <returns>Returns data from sensor processed to target type</returns>
        float ReadHumidity(Enums.HumidityType readHumidityType);
        /// <summary>
        /// Calculates and converts humidity to target unit
        /// </summary>
        /// <param name="readHumidityType">Target unit to convert to</param>
        /// <param name="rawHumidity">Humidity to convert from, has to be in raw format</param>
        /// <returns>Returns humidity in target unit</returns>
        float CalculateHumidity(Enums.HumidityType readHumidityType, float rawHumidity);

        #endregion Public Methods
    }
}