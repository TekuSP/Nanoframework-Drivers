namespace DriverBase.Interfaces
{
    public interface IPressureSensor
    {
        /// <summary>
        /// Read pressure from a sensor, raw value
        /// </summary>
        /// <returns>Pressure in RAW value</returns>
        float ReadPressure();
        /// <summary>
        /// Calculates pressure from raw to target type
        /// </summary>
        /// <param name="type">Pressure type</param>
        /// <param name="rawPressure">Raw pressure value</param>
        /// <returns>Target unit pressure from sensor</returns>
        float CalculatePressure(Enums.PressureType type, float rawPressure);
        /// <summary>
        /// Reads pressure from sensor and calculates pressure to your unit
        /// </summary>
        /// <param name="type">Pressure unit type</param>
        /// <returns>Target unit pressure from sensor</returns>
        float ReadPressure(Enums.PressureType type);
    }
}
