namespace ESP32_DriverBase.Interfaces
{
    public interface ITemperatureSensor
    {
        #region Public Properties

        Enums.TemperatureUnit ReadTemperatureUnit { get; }

        #endregion Public Properties

        #region Public Methods

        float ReadTemperature();

        #endregion Public Methods
    }
}