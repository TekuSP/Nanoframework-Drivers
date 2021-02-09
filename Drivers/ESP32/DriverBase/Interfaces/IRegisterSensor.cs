namespace ESP32_DriverBase.Interfaces
{
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