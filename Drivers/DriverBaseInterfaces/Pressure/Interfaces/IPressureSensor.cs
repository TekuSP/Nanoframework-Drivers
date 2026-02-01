using UnitsNet;
using UnitsNet.Units;

namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Basic pressure sensor interface.
    /// </summary>
    public interface IPressureSensor
    {
        /// <summary>
        /// Read pressure from a sensor, raw value
        /// </summary>
        /// <returns>Pressure in RAW value.</returns>
        double ReadPressure();
        /// <summary>
        /// Calculates pressure from raw to target type
        /// </summary>
        /// <param name="type">Pressure type.</param>
        /// <param name="rawPressure">Raw pressure value.</param>
        /// <returns>Target unit pressure from sensor.</returns>
        Pressure CalculatePressure(PressureUnit type, double rawPressure);
        /// <summary>
        /// Reads pressure from sensor and calculates pressure to your unit
        /// </summary>
        /// <param name="type">Pressure unit type.</param>
        /// <returns>Target unit pressure from sensor.</returns>
        Pressure ReadPressure(PressureUnit type);
    }
}
