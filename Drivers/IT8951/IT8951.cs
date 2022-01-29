using DriverBase;

using System;
using System.Device.Gpio;
using System.Device.Spi;
using System.Threading;

namespace IT8951
{
    public class IT8951 : DriverBaseSPI
    {
        protected int reset;
        protected int hostDataBusReady;
        protected int chipSelect;
        protected GpioController gpio;
        protected GpioPin resetPin;
        protected GpioPin hostDataBusReadyPin;
        protected GpioPin chipSelectPin;
        public IT8951(int SPIBusID, SpiConnectionSettings settings, int reset, int hostDataBusReady, int chipSelect) : base("IT8951", SPIBusID, settings)
        {
            settings.DataFlow = DataFlow.MsbFirst;
            settings.Mode = SpiMode.Mode0;
            settings.ClockFrequency = 20000000;
            this.reset = reset;
            this.hostDataBusReady = hostDataBusReady;
            this.chipSelect = chipSelect;
        }
        public override long ReadData(byte pointer)
        {
            throw new NotImplementedException();
        }

        public override long ReadData(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override string ReadDeviceId()
        {
            throw new NotImplementedException();
        }

        public override string ReadManufacturerId()
        {
            throw new NotImplementedException();
        }

        public override string ReadSerialNumber()
        {
            throw new NotImplementedException();
        }

        public override void WriteData(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            base.Start();
            //Init working pins
            resetPin = gpio.OpenPin(reset, PinMode.Output);
            hostDataBusReadyPin = gpio.OpenPin(hostDataBusReady, PinMode.Input); //INPUT
            chipSelectPin = gpio.OpenPin(chipSelect, PinMode.Output);

            chipSelectPin.Write(PinValue.High); //Turn on Chip Select

            //Reset the display
            resetPin.Write(PinValue.Low);
            Thread.Sleep(1000);
            resetPin.Write(PinValue.High);
        }
        public override void Stop()
        {
            resetPin.Write(PinValue.Low);
            hostDataBusReadyPin.Write(PinValue.Low);
            chipSelectPin.Write(PinValue.Low);
            Thread.Sleep(100); //Waiting for power off
            //Cleanup RST/HDBR/CS
            resetPin.Dispose();
            resetPin = null;
            hostDataBusReadyPin.Dispose();
            hostDataBusReadyPin = null;
            chipSelectPin.Dispose();
            chipSelectPin = null;
            //Stop SPI communication
            base.Stop();
        }
        public void WaitForLCDReady()
        {
            while (hostDataBusReadyPin.Read() == PinValue.Low) //Wait for display to signal ready
            {
                Thread.Sleep(10);
            }
        }
        public void WriteLCDCode(ushort code)
        {
            if (!IsRunning)
                return; //Must be running for sending data
            ushort wPreamble = 0x6000;

            WaitForLCDReady(); //wait for ready

            chipSelectPin.Write(PinValue.Low); //Turn off Chip Select

            SpiDevice.Write(BitConverter.GetBytes(wPreamble >> 8)); //Send Preamble
            SpiDevice.Write(BitConverter.GetBytes(wPreamble));

            WaitForLCDReady(); //wait for ready

            SpiDevice.Write(BitConverter.GetBytes(code >> 8)); //Send Code
            SpiDevice.Write(BitConverter.GetBytes(code));

            chipSelectPin.Write(PinValue.High); //Turn on Chip Select
        }
        public void WriteLCDData(ushort data)
        {
            if (!IsRunning)
                return; //Must be running for sending data
            ushort wPreamble = 0x0000;

            WaitForLCDReady(); //wait for ready

            chipSelectPin.Write(PinValue.Low); //Turn off Chip Select

            SpiDevice.Write(BitConverter.GetBytes(wPreamble >> 8)); //Send Preamble
            SpiDevice.Write(BitConverter.GetBytes(wPreamble));

            WaitForLCDReady(); //wait for ready

            SpiDevice.Write(BitConverter.GetBytes(data >> 8)); //Send data
            SpiDevice.Write(BitConverter.GetBytes(data));

            chipSelectPin.Write(PinValue.High); //Turn on Chip Select
        }
    }
}
