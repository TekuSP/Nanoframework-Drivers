namespace ESP32_DriverBase.Interfaces
{
    public interface IAdvancedHumiditySensor : IHumiditySensor
    {
        #region Public Methods

        void HeatUp(int seconds);

        #endregion Public Methods
    }
}