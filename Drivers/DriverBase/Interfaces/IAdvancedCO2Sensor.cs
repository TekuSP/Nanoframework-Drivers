namespace TekuSP.Drivers.DriverBase.Interfaces
{
    public interface IAdvancedCO2Sensor : ICO2Sensor
    {
        /// <summary>
        /// Autocalibrate CO2 sensor
        /// </summary>
        /// <param name="turnOn">Turn on = true, or off = false</param>
        void AutoCalibration(bool turnOn);
        /// <summary>
        /// Set detection range for CO2 sensor
        /// </summary>
        /// <param name="ppm">PPM range to set</param>
        void SetDetectionRange(int ppm);
    }
}
