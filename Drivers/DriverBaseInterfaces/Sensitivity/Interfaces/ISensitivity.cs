using UnitsNet;

namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Defines support of setting sensitivity to light or color
    /// </summary>
    public interface ISensitivity
    {
        /// <summary>
        /// Sets integration time.
        /// </summary>
        /// <param name="integrationTime">Integration time.</param>
        void SetIntegrationTime(Duration integrationTime);
        /// <summary>
        /// Set gain.
        /// </summary>
        /// <param name="gain">Gain.</param>
        void SetGain(byte gain);
    }
}
