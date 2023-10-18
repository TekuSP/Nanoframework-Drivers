namespace DriverBase.Interfaces
{
    /// <summary>
    /// Defines light sensor
    /// </summary>
    public interface ILightSensor
    {
        /// <summary>
        /// Gets current Lux levels of light
        /// </summary>
        /// <returns>Lux levels</returns>
        public float GetLux();
    }
}
