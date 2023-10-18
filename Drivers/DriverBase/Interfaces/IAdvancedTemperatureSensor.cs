namespace TekuSP.Drivers.DriverBase.Interfaces
{
    public interface IAdvancedTemperatureSensor : ITemperatureSensor
    {
        #region Public Methods

        /// <summary>
        /// Sets sensor resolution
        /// </summary>
        /// <param name="resolution">Resolution to set for temperature sensor</param>
        void SetTemperatureResolution(int resolution);

        #endregion Public Methods
    }
}