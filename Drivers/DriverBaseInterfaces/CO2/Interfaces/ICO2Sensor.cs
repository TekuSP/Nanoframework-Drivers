using UnitsNet;

namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Basic CO2 sensor interface.
    /// </summary>
    public interface ICO2Sensor
    {
        /// <summary>
        /// Reads CO2 data from sensor.
        /// </summary>
        /// <returns>CO2 concentration.</returns>
        VolumeConcentration ReadCO2Concentration();
        /// <summary>
        /// Calibrates Zero Point on sensor.
        /// </summary>
        void CalibrateZeroPoint();
        /// <summary>
        /// Calibrates Span Point on sensor.
        /// </summary>
        /// <param name="ppm">Span point in parts per million.</param>
        void CalibrateSpanPoint(Ratio ppm);
    }
}
