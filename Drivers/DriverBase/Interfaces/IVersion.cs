namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Defines if device knows its version
    /// </summary>
    public interface IVersion
    {
        /// <summary>
        /// Returns version of device
        /// </summary>
        /// <returns>Version</returns>
        public int ReadVersion();
    }
}
