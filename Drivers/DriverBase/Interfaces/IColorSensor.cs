namespace DriverBase.Interfaces
{
    /// <summary>
    /// Defines color sensor
    /// </summary>
    public interface IColorSensor
    {
        /// <summary>
        /// Gets raw data from sensor
        /// </summary>
        /// <returns>Raw data</returns>
        public IColorData GetRawData();
        /// <summary>
        /// Gets RGB data from sensor
        /// </summary>
        /// <returns>RGB data</returns>
        public IColorData GetRGB();
        /// <summary>
        /// Gets Kelvin temperature from sensor
        /// </summary>
        /// <returns>Kelvin temperature</returns>
        public int GetColorTemperature();
    }
}
