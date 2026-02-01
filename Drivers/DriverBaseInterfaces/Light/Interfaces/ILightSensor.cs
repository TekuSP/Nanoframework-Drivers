using UnitsNet;

namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Defines light sensor
    /// </summary>
    public interface ILightSensor
    {
        /// <summary>
        /// Gets current lux levels of light.
        /// </summary>
        /// <returns>Illuminance in lux.</returns>
        Illuminance GetLux();
    }
}
