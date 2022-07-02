using DriverBase;
using DriverBase.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Windows.Storage.Streams;

namespace MHZ19B
{
    /// <summary>
    /// Driver for Zhengzhou Winsen Electronics Technology Co., Ltd, Intelligent Infrared CO2 Module MH-Z19B <br/>
    /// <a href="https://www.winsen-sensor.com/d/files/infrared-gas-sensor/mh-z19b-co2-ver1_0.pdf"/>
    /// </summary>
    public class MHZ19B : DriverBaseUART, IAdvancedCO2Sensor, ICO2Sensor
    {
        #region Public Constructors
        /// <summary>
        /// Constructs MH-Z19B Device Driver, but does not start it, see <see cref="Start"/> to start it
        /// </summary>
        /// <param name="serialBusID">Serial Bus ID (COM1 for example)</param>
        public MHZ19B(string serialBusID) : base("MH-Z19B", serialBusID)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public void AutoCalibration(bool turnOn)
        {
            byte[] dataToSend = new byte[8] { 0xFF, 0x01, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00 }; //Default off
            if (turnOn)
                dataToSend[4] = 0xA0;
            dataToSend[7] = CalculateCheckSum(dataToSend);
            WriteData(dataToSend); //No response expected
        }

        /// <summary>
        /// Calibrates Span Point, Run <see cref="CalibrateZeroPoint"/> before running this, make sure the sensor worked under a certain level co2 for over 20 minutes.
        /// </summary>
        /// <param name="ppm">1000 ppm or more suggested, 2000 ppm recommended</param>
        public void CalibrateSpanPoint(int ppm)
        {
            byte[] dataToSend = new byte[8] { 0xFF, 0x01, 0x88, (byte)(ppm / 256), (byte)(ppm % 256), 0x00, 0x00, 0x00 };
            dataToSend[7] = CalculateCheckSum(dataToSend);
            WriteData(dataToSend); //No response expected
        }

        /// <summary>
        /// Calibrates Zero Point, Zero point is 400 ppm, make sure sensor worked under 400 ppm for over 20 minutes.
        /// </summary>
        public void CalibrateZeroPoint()
        {
            byte[] dataToSend = new byte[8] { 0xFF, 0x01, 0x87, 0x00, 0x00, 0x00, 0x00, 0x00 };
            dataToSend[7] = CalculateCheckSum(dataToSend);
            WriteData(dataToSend); //No response expected
        }

        /// <summary>
        /// Reads and calculates CO2 Limited Concentration
        /// </summary>
        /// <returns>CO2 concentration in ppm</returns>
        public int ReadCO2Concentration()
        {
            var response = SendAndRead(MHZCommands.CO2LimitedTemp);
            return GetFromHighLowByte(response[2],response[3]);
        }
        /// <summary>
        /// Reads and calculates CO2 Unlimited Concentration
        /// </summary>
        /// <returns>CO2 concentration in ppm</returns>
        public int ReadCO2ConcentrationUnlimited()
        {
            var response = SendAndRead(MHZCommands.CO2UnlimitedTemp);
            return GetFromHighLowByte(response[4], response[5]);
        }

        /// <summary>
        /// Reads temperature
        /// </summary>
        /// <returns>CO2 concentration in ppm</returns>
        public int ReadTemperature()
        {
            var response = SendAndRead(MHZCommands.CO2LimitedTemp);
            return Convert.ToInt32(response[4].ToString(), 16) - 38; //random constant???
        }

        /// <summary>
        /// Not supported on MHZ-19B
        /// </summary>
        /// <param name="pointer"></param>
        /// <returns></returns>
        public override long ReadData(byte pointer)
        {
            return -1;
        }

        public override long ReadData(params byte[] data)
        {
            var read = serialDevice.BytesToRead;
            if (read != 9)
                return 0;
            serialDevice.Read(data, 0, read);
            return data.Length;
        }

        /// <summary>
        /// Not Supported
        /// </summary>
        /// <returns>Not Supported</returns>
        public override string ReadDeviceId()
        {
            return "Not Supported";
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <returns>Returns manufacturer</returns>
        public override string ReadManufacturerId()
        {
            return "Zhengzhou Winsen Electronics Technology Co., Ltd ";
        }

        /// <summary>
        /// Not Supported
        /// </summary>
        /// <returns>Not Supported</returns>
        public override string ReadSerialNumber()
        {
            return "Not Supported";
        }
        /// <summary>
        /// Sets detection range for CO2 Sensor
        /// </summary>
        /// <param name="ppm">Only 2000 ppm or 5000 ppm allowed!</param>
        public void SetDetectionRange(int ppm)
        {
            if (ppm != 2000 || ppm != 5000)
                return; //Allowed is only 2000 ppm or 5000 ppm
            byte[] dataToSend = new byte[9] { 0xFF, 0x01, 0x99, (byte)(ppm / 256), (byte)(ppm % 256), 0x00, 0x00, 0x00,0x00 };
            dataToSend[8] = CalculateCheckSum(dataToSend);
            WriteData(dataToSend); //No response expected
        }

        public override void Start()
        {
            base.Start();
            serialDevice.BaudRate = 9600;
            serialDevice.DataBits = 8;
            serialDevice.StopBits = System.IO.Ports.StopBits.One;
            serialDevice.Parity = System.IO.Ports.Parity.None;
            serialDevice.Handshake = System.IO.Ports.Handshake.None;
            serialDevice.ReadTimeout = 1000;
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void WriteData(byte[] data)
        {
            serialDevice.Write(data, 0, data.Length);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Calculates checksum
        /// </summary>
        /// <param name="packet">Input byte array</param>
        /// <returns>Checksum byte</returns>
        private byte CalculateCheckSum(params byte[] packet)
        {
            byte checksum = 0;
            for (byte i = 1; i < 8; i++)
                checksum += packet[i];
            return (byte)((byte)(0xff - checksum) + 1);
        }

        private byte[] SendAndRead(MHZCommands command)
        {
            byte[] dataToSend = new byte[9] { 0xFF, 0x01, (byte)command, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            dataToSend[8] = CalculateCheckSum(dataToSend);
            byte[] dataToRead = new byte[9];
            try
            {
                WriteData(dataToSend); //Response EXPECTED
                ReadData(dataToRead); //Read Response
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception happened when reading/writing data: " + ex);
            }
            return dataToRead;
        }
        private byte[] SendAndRead(MHZCommands command, byte[] additionalData)
        {
            byte[] dataToSend = new byte[9] { 0xFF, 0x01, (byte)command, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            for (int i = 0; i < 5; i++)
                dataToSend[i + 3] = additionalData[i];
            dataToSend[8] = CalculateCheckSum(dataToSend);
            byte[] dataToRead = new byte[9];
            try
            {
                WriteData(dataToSend); //Response EXPECTED
                ReadData(dataToRead); //Read Response
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception happened when reading/writing data: " + ex);
            }
            return dataToRead;
        }
        private int GetFromHighLowByte(byte high, byte low)
        {
            return (high * 256) + low;
        }
        enum MHZCommands
        {
          RecoveryReset = 0x78,	// 0 Recovery Reset        Changes operation mode and performs MCU reset
          ABC = 0x79,	// 1 ABC Mode ON/OFF       Turns ABC logic on or off (b[3] == 0xA0 - on, 0x00 - off)
          GetABC = 0x7D,	// 2 Get ABC logic status  (1 - enabled, 0 - disabled)	
          RawCO2 = 0x84,	// 3 Raw CO2
          CO2UnlimitedTemp = 0x85,	// 4 Temp double, CO2 Unlimited
          CO2LimitedTemp = 0x86,	// 5 Temp integer, CO2 limited
          ZeroCalibration = 0x87,	// 6 Zero Calibration
          SpanCalibration = 0x88,	// 7 Span Calibration
          Range = 0x99,	// 8 Range
          GetRange = 0x9B,	// 9 Get Range
          GetBackgroundCO2 = 0x9C,	// 10 Get Background CO2
          GetFirmwaveVersion = 0xA0,	// 11 Get Firmware Version
          ResendMessage = 0xA2,	// 12 Get Last Response
          GetTemperatureCalibration = 0xA3		// 13 Get Temp Calibration
        };

        #endregion Private Methods
    }
}