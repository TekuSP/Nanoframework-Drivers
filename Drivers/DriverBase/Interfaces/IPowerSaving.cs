namespace DriverBase.Interfaces
{
    /// <summary>
    /// Defines if device supports sleep and wake up
    /// </summary>
    public interface IPowerSaving
    {
        /// <summary>
        /// Puts device to power saving mode
        /// </summary>
        public void Sleep();
        /// <summary>
        /// Wakes device from power saving mode
        /// </summary>
        public void Wakeup();
    }
}
