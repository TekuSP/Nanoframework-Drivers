namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Defines if device knows its version info
    /// </summary>
    public interface IVersionInfo : IVersion
    {
        /// <summary>
        /// Returns version info
        /// </summary>
        /// <returns>Version info</returns>
        public string ReadVersionInfo();
    }
}
