using UnitsNet;

namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Sensor which supports IR
    /// </summary>
    public interface IIRSensor
    {
        /// <summary>
        /// Gets IR data from sensor.
        /// </summary>
        /// <returns>IR level as a dimensionless ratio.</returns>
        Ratio GetIR();
    }
}
