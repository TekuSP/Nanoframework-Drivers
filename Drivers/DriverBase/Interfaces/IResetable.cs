namespace DriverBase.Interfaces
{
    /// <summary>
    /// Device supports reset
    /// </summary>
    public interface IResetable
    {
        /// <summary>
        /// Resets the device
        /// </summary>
        public void Reset();
    }
}
