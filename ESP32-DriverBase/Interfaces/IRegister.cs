namespace ESP32_DriverBase.Interfaces
{
    public interface IRegister
    {
        #region Public Methods

        byte GetData();

        void SetData(byte input);

        #endregion Public Methods
    }
}