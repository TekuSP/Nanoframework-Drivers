using UnitsNet;
using UnitsNet.Units;

namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Altitude sensor capabilities interface.
    /// </summary>
    public interface IAltitudeSensor
    {
        /// <summary>
        /// Calculates and converts temperature and pressure to target unit of altitude
        /// </summary>
        /// <param name="readLengthUnit">Target unit to convert to.</param>
        /// <param name="rawTemperature">Temperature to convert from, has to be in raw format</param>
        /// <param name="rawPressure">Pressure to convert from, has to be in raw format</param>
        /// <returns>Returns temperature in target unit.</returns>
        Length CalculateAltitude(LengthUnit readLengthUnit, double rawPressure, double rawTemperature);

        /// <summary>
        /// Read Altitude from sensor (raw).
        /// </summary>
        /// <returns>Returns raw data from sensor.</returns>
        double ReadAltitude();

        /// <summary>
        /// Read Altitude from sensor and convert to target unit.
        /// </summary>
        /// <param name="readAltitudeUnit">Target unit to convert to.</param>
        /// <returns>Returns data from sensor in target unit.</returns>
        Length ReadAltitude(LengthUnit readAltitudeUnit);
    }
}
