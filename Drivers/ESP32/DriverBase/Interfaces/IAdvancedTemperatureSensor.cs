
namespace ESP32_DriverBase.Interfaces
{
   public interface IAdvancedTemperatureSensor : ITemperatureSensor
    {
        /// <summary>
        /// Sets sensor resolution
        /// </summary>
        /// <param name="resolution">Resolution to set for temperature sensor</param>
        void SetTemperatureResolution(int resolution);
    }
}
