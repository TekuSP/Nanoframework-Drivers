using nanoFramework.Hardware.Esp32;
using System.Diagnostics;
using System.Threading;

namespace Meteostanice
{
    public class Program
    {
        #region Public Methods

        public static void Main()
        {
            Debug.WriteLine("Hello world!");
            Configuration.SetPinFunction(22, DeviceFunction.I2C1_CLOCK);
            Configuration.SetPinFunction(21, DeviceFunction.I2C1_DATA);

            HDC1080.HDC1080 hDC1080 = new HDC1080.HDC1080();
            hDC1080.Start();
            var manu = hDC1080.ReadManufacturerId();
            var serial = hDC1080.ReadSerialNumber();
            var devID = hDC1080.ReadDeviceId();
            var temperature = hDC1080.ReadTemperature();
            hDC1080.HeatUp(10);
            var humi = hDC1080.ReadHumidity();

            Thread.Sleep(Timeout.Infinite);

            // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }

        #endregion Public Methods
    }
}