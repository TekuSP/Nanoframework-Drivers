namespace ESP32_DriverBase.Interfaces
{
    public interface IHumiditySensor
    {
        #region Public Properties

        Enums.HumidityUnit ReadHumidityUnit { get; }

        #endregion Public Properties

        #region Public Methods

        float ReadHumidity();

        #endregion Public Methods
    }
}