namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Devices has sleeping capability
    /// </summary>
    public interface ISleepSupport
    {
        #region Public Properties

        /// <summary>
        /// Is device sleeping?
        /// </summary>
        bool IsSleeping { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Sleep device
        /// </summary>
        void Sleep();

        /// <summary>
        /// Wake up device from sleep
        /// </summary>
        void WakeUp();

        #endregion Public Methods
    }
}