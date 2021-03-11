﻿using nanoFramework.Hardware.Esp32;
using System.Threading;
using System.Device.Gpio;
using System;
using System.Diagnostics.Uart;
using System.Diagnostics;

namespace Meteostanice
{
    public class Program
    {
        #region Public Methods

        public static void Main()
        {
            //Display TEST
            //Configuration.SetPinFunction(23, DeviceFunction.SPI1_MOSI);
            //Configuration.SetPinFunction(19, DeviceFunction.SPI1_CLOCK);

            //SSD1331.SSD1331 display = new SSD1331.SSD1331(1, Gpio.IO32, Gpio.IO33, Gpio.IO27, GpioController.GetDefault());
            //display.Start();
            //display.DrawLine(0, 0, 50, 50, 255, 0, 0);

            //HDC1080 TEST
            //Configuration.SetPinFunction(22, DeviceFunction.I2C1_CLOCK);
            //Configuration.SetPinFunction(21, DeviceFunction.I2C1_DATA);

            //HDC1080.HDC1080 hDC1080 = new HDC1080.HDC1080(1);
            //hDC1080.Start();
            //var manu = hDC1080.ReadManufacturerId();
            //var serial = hDC1080.ReadSerialNumber();
            //var devID = hDC1080.ReadDeviceId();
            //var temperature = hDC1080.ReadTemperature();
            //hDC1080.HeatUp(10);
            //var humi = hDC1080.ReadHumidity();
            //MHZ19B TEST
            //Configuration.SetPinFunction(Gpio.IO16, DeviceFunction.COM3_RX);
            //Configuration.SetPinFunction(Gpio.IO17, DeviceFunction.COM3_TX);
            //MHZ19B.MHZ19B mhz = new MHZ19B.MHZ19B("COM3");
            //mhz.Start();
            //Debug.WriteLine("MHZ Started.");
            //while (true)
            //{
            //    int ppm = mhz.ReadCO2Concentration();
            //    Debug.WriteLine("Current limited ppm is: " + ppm);
            //    ppm = mhz.ReadCO2ConcentrationUnlimited();
            //    Debug.WriteLine("Current unlimited ppm is: " + ppm);
            //    int temp = mhz.ReadTemperature();
            //    Debug.WriteLine("Current temperature is: " + temp);
            //    Thread.Sleep(10000);
            //}


            Configuration.SetPinFunction(Gpio.IO23, DeviceFunction.I2C1_CLOCK);
            Configuration.SetPinFunction(Gpio.IO18, DeviceFunction.I2C1_DATA);
            LPS22HB.LPS22HB lPS22HB = new LPS22HB.LPS22HB(1);
            lPS22HB.Start();
            var manu = lPS22HB.ReadManufacturerId();
            var serial = lPS22HB.ReadSerialNumber();
            var devID = lPS22HB.ReadDeviceId();
            float temperature;
            float pressure;
            Debug.WriteLine($"Initialized device {manu} {devID} - {serial}");
            while (true)
            {
                temperature = lPS22HB.ReadTemperature(DriverBase.Enums.TemperatureUnit.Celsius);
                pressure = lPS22HB.ReadPressure(DriverBase.Enums.PressureType.mBar);
                Debug.WriteLine($"Temperature is: {temperature} C");
                Debug.WriteLine($"Pressure is: {pressure} mBar");
                Thread.Sleep(5000);
            }
        }

        #endregion Public Methods
    }
}