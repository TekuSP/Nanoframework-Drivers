using nanoFramework.Hardware.Esp32;
using System.Diagnostics;
using System.Threading;
using Windows.Devices.Gpio;

namespace Meteostanice
{
    public class Program
    {
        #region Public Methods

        public static void Main()
        {
            Debug.WriteLine("Hello world!");
            //Display TEST
            //Configuration.SetPinFunction(23, DeviceFunction.SPI1_MOSI);
            //Configuration.SetPinFunction(19, DeviceFunction.SPI1_CLOCK);

            //SSD1331.SSD1331 display = new SSD1331.SSD1331("SPI1", Gpio.IO32, Gpio.IO33, Gpio.IO27, GpioController.GetDefault());
            //display.Start();
            //display.DrawLine(0, 0, 50, 50, 255, 0, 0);

            //HDC1080 TEST
            //Configuration.SetPinFunction(22, DeviceFunction.I2C1_CLOCK);
            //Configuration.SetPinFunction(21, DeviceFunction.I2C1_DATA);

            //HDC1080.HDC1080 hDC1080 = new HDC1080.HDC1080();
            //hDC1080.Start();
            //var manu = hDC1080.ReadManufacturerId();
            //var serial = hDC1080.ReadSerialNumber();
            //var devID = hDC1080.ReadDeviceId();
            //var temperature = hDC1080.ReadTemperature();
            //hDC1080.HeatUp(10);
            //var humi = hDC1080.ReadHumidity();

            //MHZ19B TEST
            Configuration.SetPinFunction(Gpio.IO16, DeviceFunction.COM3_RX);
            Configuration.SetPinFunction(Gpio.IO17, DeviceFunction.COM3_TX);

            MHZ19B.MHZ19B mhz = new MHZ19B.MHZ19B("COM3");
            mhz.Start();
            int ppm = mhz.ReadCO2Concentration();
            Debug.WriteLine("Current ppm is: " + ppm);
            Thread.Sleep(Timeout.Infinite);

            // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }

        #endregion Public Methods
    }
}