namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Sensor which supports IR
    /// </summary>
    public interface IIRSensor
    {
        /// <summary>
        /// Gets IR data from sensor
        /// </summary>
        /// <returns>Returns IR data</returns>
        public float GetIR();
    }
}
