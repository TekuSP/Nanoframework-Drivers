using UnitsNet;

namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Advanced CO2 sensor capabilities beyond basic concentration reading.
    /// </summary>
    public interface IAdvancedCO2Sensor : ICO2Sensor
    {
        /// <summary>
        /// Autocalibrate CO2 sensor.
        /// </summary>
        /// <param name="turnOn">Turn on = true, or off = false.</param>
        void AutoCalibration(bool turnOn);
        /// <summary>
        /// Set detection range for CO2 sensor.
        /// </summary>
        /// <param name="ppm">Detection range in parts per million.</param>
        void SetDetectionRange(Ratio ppm);
    }
}
