namespace DriverBase.Interfaces
{
    /// <summary>
    /// This interfaces requires sensors having both ITemperatureSensor and IHumiditySensor
    /// </summary>
    public interface IDewPointSensor : ITemperatureSensor, IHumiditySensor
    {
        #region Public Methods

        /// <summary>
        /// Calculates dewpoint based on input
        /// </summary>
        /// <param name="dewPointType">Type of dewpoint</param>
        /// <param name="rawTemperature">Raw data temperature</param>
        /// <param name="rawHumidity">Raw data humidity</param>
        /// <returns>Dewpoint</returns>
        double CalculateDewPoint(Enums.TemperatureUnit dewPointType, double rawTemperature, double rawHumidity);

        /// <summary>
        /// Gets data from Temperature and Humidity sensors, calculates data
        /// </summary>
        /// <param name="dewPointType">Type of dewpoint</param>
        /// <returns>Dewpoint</returns>
        double GetAndCalculteDewPoint(Enums.TemperatureUnit dewPointType);

        #endregion Public Methods
    }
}