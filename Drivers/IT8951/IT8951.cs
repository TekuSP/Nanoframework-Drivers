using DriverBase;

using System;
using System.Device.Gpio;
using System.Device.Spi;
using System.Threading;

//https://github.com/clashman/it8951/blob/master/it8951.ino
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
        public void WriteLCDCode(ushort code, ushort[] arguments)
        {
            WriteLCDCode(code);
            foreach (var item in arguments)
            {
                WriteLCDData(item);
            }
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
        public void WriteLCDData(ushort[] data)
        {
            if (!IsRunning)
                return; //Must be running for sending data
            ushort wPreamble = 0x0000;

            WaitForLCDReady(); //wait for ready

            chipSelectPin.Write(PinValue.Low); //Turn off Chip Select

            SpiDevice.Write(BitConverter.GetBytes(wPreamble >> 8)); //Send Preamble
            SpiDevice.Write(BitConverter.GetBytes(wPreamble));

            WaitForLCDReady(); //wait for ready

            foreach (var item in data)
            {
                SpiDevice.Write(BitConverter.GetBytes(item >> 8)); //Send data
                SpiDevice.Write(BitConverter.GetBytes(item));
            }

            chipSelectPin.Write(PinValue.High); //Turn on Chip Select
        }
        public ushort ReadLCDData()
        {
            if (!IsRunning)
                return 0; //Must be running for sending data
            ushort wPreamble = 0x1000;
            WaitForLCDReady(); //wait for ready

            chipSelectPin.Write(PinValue.Low); //Turn off Chip Select

            SpiDevice.Write(BitConverter.GetBytes(wPreamble >> 8)); //Send Preamble
            SpiDevice.Write(BitConverter.GetBytes(wPreamble));

            WaitForLCDReady(); //wait for ready

            byte[] readBuffer = new byte[2];

            SpiDevice.TransferFullDuplex(BitConverter.GetBytes(0x00), readBuffer); //dummy
            ushort data = BitConverter.ToUInt16(readBuffer, 0);
            SpiDevice.TransferFullDuplex(BitConverter.GetBytes(0x00), readBuffer); //dummy
            data = BitConverter.ToUInt16(readBuffer, 0);

            WaitForLCDReady();

            SpiDevice.TransferFullDuplex(BitConverter.GetBytes(0x00), readBuffer); //read 
            data = (ushort)(BitConverter.ToUInt16(readBuffer, 0) << 8);

            SpiDevice.TransferFullDuplex(BitConverter.GetBytes(0x00), readBuffer); //read
            data |= BitConverter.ToUInt16(readBuffer, 0);

            chipSelectPin.Write(PinValue.High); //Turn on Chip Select
            return data;
        }
        public ushort[] ReadLCDData(int size)
        {
            if (!IsRunning)
                return null; //Must be running for sending data
            ushort wPreamble = 0x1000;

            WaitForLCDReady(); //wait for ready

            chipSelectPin.Write(PinValue.Low); //Turn off Chip Select

            SpiDevice.Write(BitConverter.GetBytes(wPreamble >> 8)); //Send Preamble
            SpiDevice.Write(BitConverter.GetBytes(wPreamble));

            WaitForLCDReady(); //wait for ready

            byte[] readBuffer = new byte[2];
            ushort[] data = new ushort[size];

            SpiDevice.TransferFullDuplex(BitConverter.GetBytes(0x00), readBuffer); //dummy
            data[0] = BitConverter.ToUInt16(readBuffer, 0);
            SpiDevice.TransferFullDuplex(BitConverter.GetBytes(0x00), readBuffer); //dummy
            data[0] = BitConverter.ToUInt16(readBuffer, 0);

            WaitForLCDReady();

            for (int i = 0; i < data.Length; i++)
            {
                SpiDevice.TransferFullDuplex(BitConverter.GetBytes(0x00), readBuffer); //read 
                data[i] = (ushort)(BitConverter.ToUInt16(readBuffer, 0) << 8);

                SpiDevice.TransferFullDuplex(BitConverter.GetBytes(0x00), readBuffer); //read
                data[i] |= BitConverter.ToUInt16(readBuffer, 0);
            }

            chipSelectPin.Write(PinValue.High); //Turn on Chip Select
            return data;
        }
    }
}
