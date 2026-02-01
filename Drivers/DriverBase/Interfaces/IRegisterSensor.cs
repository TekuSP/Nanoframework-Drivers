namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Interface for sensors exposing device registers.
    /// </summary>
    public interface IRegisterSensor
    {
        #region Public Methods

        /// <summary>
        /// Reads register from device
        /// </summary>
        /// <returns>IRegister type read from device</returns>
        IRegister ReadRegister();

        /// <summary>
        /// Sets register to device
        /// </summary>
        /// <param name="register">IRegister type to set</param>
        void WriteRegister(IRegister register);

        #endregion Public Methods
    }
}