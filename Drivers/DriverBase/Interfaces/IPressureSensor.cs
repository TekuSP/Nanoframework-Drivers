namespace TekuSP.Drivers.DriverBase.Interfaces
{
    public interface IPressureSensor
    {
        /// <summary>
        /// Read pressure from a sensor, raw value
        /// </summary>
        /// <returns>Pressure in RAW value</returns>
        double ReadPressure();
        /// <summary>
        /// Calculates pressure from raw to target type
        /// </summary>
        /// <param name="type">Pressure type</param>
        /// <param name="rawPressure">Raw pressure value</param>
        /// <returns>Target unit pressure from sensor</returns>
        double CalculatePressure(Enums.PressureType type, double rawPressure);
        /// <summary>
        /// Reads pressure from sensor and calculates pressure to your unit
        /// </summary>
        /// <param name="type">Pressure unit type</param>
        /// <returns>Target unit pressure from sensor</returns>
        double ReadPressure(Enums.PressureType type);
    }
}
