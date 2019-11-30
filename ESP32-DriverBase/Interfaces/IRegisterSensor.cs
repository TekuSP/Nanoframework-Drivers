namespace ESP32_DriverBase.Interfaces
{
    public interface IRegisterSensor
    {
        #region Public Methods

        IRegister ReadRegister();

        void WriteRegister(IRegister register);

        #endregion Public Methods
    }
}