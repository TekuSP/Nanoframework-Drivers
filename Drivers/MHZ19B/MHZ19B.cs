using DriverBase;
using DriverBase.Interfaces;
using System.Threading;
using Windows.Storage.Streams;

namespace MHZ19B
{
    public class MHZ19B : DriverBaseUART, IAdvancedCO2Sensor, ICO2Sensor
    {
        #region Public Constructors

        public MHZ19B(string serialBusID) : base("MH-Z19B", serialBusID)
        {
        }

        #endregion Public Constructors

        #region Protected Properties

        protected DataReader DataReader { get; set; }
        protected DataWriter DataWriter { get; set; }
        protected IInputStream InputStream { get; set; }
        protected IOutputStream OutputStream { get; set; }

        #endregion Protected Properties

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
        /// Reads and calculates CO2 Concentration
        /// </summary>
        /// <returns>CO2 concentration in ppm</returns>
        public int ReadCO2Concentration()
        {
            byte[] dataToSend = new byte[8] { 0xFF, 0x01, 0x86, 0x00, 0x00, 0x00, 0x00, 0x00 };
            dataToSend[7] = CalculateCheckSum(dataToSend);
            WriteData(dataToSend); //Response EXPECTED
            byte[] dataToRead = new byte[8];
            ReadData(dataToRead); //Read Response
            return (dataToRead[2] * 256) + dataToRead[3];
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
            while (serialDevice.BytesToRead < data.Length)
                Thread.Sleep(1); //Wait for response

            DataReader.ReadBytes(data);
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

        public void SetDetectionRange(int ppm)
        {
            if (ppm != 2000 || ppm != 5000)
                return; //Allowed is only 2000 ppm or 5000 ppm
            byte[] dataToSend = new byte[8] { 0xFF, 0x01, 0x99, (byte)(ppm / 256), (byte)(ppm % 256), 0x00, 0x00, 0x00 };
            dataToSend[7] = CalculateCheckSum(dataToSend);
            WriteData(dataToSend); //No response expected
        }

        public override void Start()
        {
            base.Start();
            serialDevice.BaudRate = 9600;
            serialDevice.DataBits = 8;
            serialDevice.StopBits = Windows.Devices.SerialCommunication.SerialStopBitCount.One;
            serialDevice.Parity = Windows.Devices.SerialCommunication.SerialParity.None;
            InputStream = serialDevice.InputStream;
            OutputStream = serialDevice.OutputStream;
            DataReader = new DataReader(InputStream);
            DataWriter = new DataWriter(OutputStream);
        }

        public override void Stop()
        {
            base.Stop();
            DataReader.Dispose();
            DataReader = null;
            DataWriter.Dispose();
            DataWriter = null;
            InputStream = null;
            OutputStream = null;
        }

        public override void WriteData(byte[] data)
        {
            DataWriter.WriteBytes(data);
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
            byte i, checksum = 0;
            for (i = 1; i < 8; i++)
                checksum += packet[i];
            return (byte)((byte)(0xff - checksum) + 1);
        }

        #endregion Private Methods
    }
}