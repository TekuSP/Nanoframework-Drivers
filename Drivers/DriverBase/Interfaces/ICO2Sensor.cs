namespace DriverBase.Interfaces
{
    public interface ICO2Sensor
    {
        /// <summary>
        /// Reads CO2 data from sensor
        /// </summary>
        /// <returns>CO2 concentration</returns>
        int ReadCO2Concentration();
        /// <summary>
        /// Calibrates Zero Point on sensor
        /// </summary>
        void CalibrateZeroPoint();
        /// <summary>
        /// Calibrates Span Point on sensor
        /// </summary>
        void CalibrateSpanPoint(int ppm);
    }
}
