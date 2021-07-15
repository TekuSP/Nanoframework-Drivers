using System;
using System.Device.I2c;
using System.Threading;

using DriverBase;
using DriverBase.Enums;
using DriverBase.Interfaces;

using SHTC3.Enums;

namespace SHTC3
{
    /// <summary>
    /// Sensirion SHTC3 humidity and temperature sensor, https://www.mouser.com/datasheet/2/682/Sensirion_04202018_HT_DS_SHTC3_Preliminiary_D2-1323493.pdf
    /// </summary>
    public class SHTC3 : DriverBaseI2C, ISleepSupport, ITemperatureSensor, IHumiditySensor
    {
        #region Public Constructors

        public SHTC3(int I2CBusID, int deviceAddress = 0x70) : base("SHTC3", I2CBusID, deviceAddress)
        {
        }

        public SHTC3(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress = 0x70) : base("SHTC3", I2CBusID, connectionSettings, deviceAddress)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public bool IsSleeping { get; private set; }

        public MeasurementMode MeasurementMode { get; private set; } = MeasurementMode.SHTC3_CMD_CSD_NPM;

        #endregion Public Properties

        #region Public Methods

        public double CalculateHumidity(HumidityType readHumidityType, double rawHumidity)
        {
            switch (readHumidityType)
            {
                case HumidityType.Relative:
                    return 100f * ((double)rawHumidity / 65535f);

                default:
                    throw new ArgumentException("Only Relative humidity is supported in this sensor!");
            }
        }

        public double CalculateTemperature(TemperatureUnit readTemperatureUnit, double rawTemperature)
        {
            switch (readTemperatureUnit)
            {
                case TemperatureUnit.Celsius:
                    return -45f + (175f * ((double)rawTemperature / 65535f));

                case TemperatureUnit.Fahrenheit:
                    return (-45f + (175f * ((double)rawTemperature / 65535f))) * (9.0f / 5f) + 32.0f;

                default:
                    throw new ArgumentException("Only Celsius and Fahrenheit is supported in this sensor!");
            }
        }

        public bool CheckCRC(ushort packet, byte cs)
        {
            byte upper = (byte)(packet >> 8);
            byte lower = (byte)(packet & 0x00FF);
            byte[] data = new byte[] { upper, lower };
            byte crc = 0xFF;
            byte poly = 0x31;

            for (byte indi = 0; indi < 2; indi++)
            {
                crc ^= data[indi];

                for (byte indj = 0; indj < 8; indj++)
                {
                    if ((crc & 0x80) == 1)
                        crc = (byte)((crc << 1) ^ poly);
                    else
                        crc <<= 1;
                }
            }

            if ((cs ^ crc) == 1)
                return false;
            return true;
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="pointer">Data to write</param>
        /// <returns>-1</returns>
        public override long ReadData(byte pointer)
        {
            WriteData(new byte[] { pointer });
            return -1;
        }

        public override long ReadData(byte[] data)
        {
            return I2CDevice.Read(data).BytesTransferred;
        }

        public override string ReadDeviceId() //TODO: This has some wrong results
        {
            WakeUp();
            WriteCommand(Commands.SHTC3_CMD_READ_ID);
            byte IDhb = I2CDevice.ReadByte();
            byte IDlb = I2CDevice.ReadByte();
            byte IDcs = I2CDevice.ReadByte();
            ushort ID = (ushort)((IDhb << 8) | IDlb);
            if (!CheckCRC(ID, IDcs))
                return "BAD CRC";
            if ((ID & 0b0000100000111111) != 0b0000100000000111)
            {
                return $"Unknown device {ID}";
            }
            return ID.ToString();
        }

        public double ReadHumidity()
        {
            WakeUp();
            byte RHhb;
            byte RHlb;
            byte RHcs;
            SetMeasurmentMode(false); //Start measurment

            Thread.Sleep(20);
            bool gotData = false;
            SpanByte buffer = new SpanByte(new byte[6]);
            while (!gotData) //When we get NACK or ClockStretch, sensor is still measuring, try later
            {
                Thread.Sleep(20);
                var readStatus = I2CDevice.Read(buffer);
                if (readStatus.BytesTransferred == 6 && readStatus.Status == I2cTransferStatus.FullTransfer)
                    gotData = true;
                else
                    System.Diagnostics.Debug.WriteLine($"Status returned is {readStatus.Status.ToString()} with bytes {readStatus.BytesTransferred}");
            }
            Sleep();
            RHhb = buffer[0];
            RHlb = buffer[1];
            RHcs = buffer[2];
            ushort RH = (ushort)((RHhb << 8) | RHlb);
            if (!CheckCRC(RH, RHcs))
                return -1; //BAD CRC
            Thread.Sleep(100);
            return RH;
        }

        public double ReadHumidity(HumidityType readHumidityType)
        {
            return CalculateHumidity(readHumidityType, ReadHumidity());
        }

        public override string ReadManufacturerId()
        {
            return "Sensirion";
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <returns>Not supported</returns>
        public override string ReadSerialNumber()
        {
            return "Not supported";
        }

        public double ReadTemperature() //TODO: This requires some major refactoring, wtf is going on
        {
            WakeUp();
            byte Thb;
            byte Tlb;
            byte Tcs;
            SetMeasurmentMode(true); //Start measurment

            Thread.Sleep(20);
            bool gotData = false;
            SpanByte buffer = new SpanByte(new byte[6]);
            while (!gotData) //When we get NACK or ClockStretch, sensor is still measuring, try later
            {
                Thread.Sleep(20);
                var readStatus = I2CDevice.Read(buffer);
                if (readStatus.BytesTransferred == 6 && readStatus.Status == I2cTransferStatus.FullTransfer)
                    gotData = true;
                else
                    System.Diagnostics.Debug.WriteLine($"Status returned is {readStatus.Status.ToString()} with bytes {readStatus.BytesTransferred}");
            }
            Sleep();
            Thb = buffer[0];
            Tlb = buffer[1];
            Tcs = buffer[2];
            ushort T = (ushort)((Thb << 8) | Tlb);
            if (!CheckCRC(T, Tcs))
                return -1; //BAD CRC
            Thread.Sleep(100);
            return T;
        }

        public double ReadTemperature(TemperatureUnit readTemperatureUnit)
        {
            return CalculateTemperature(readTemperatureUnit, ReadTemperature());
        }
        /// <summary>
        /// Set measurment mode for the sensor
        /// </summary>
        /// <param name="measurementMode">Mode to use</param>
        public void SetMeasurmentMode(MeasurementMode measurementMode)
        {
            MeasurementMode = measurementMode;
        }
        private Status SetMeasurmentMode(bool temperature)
        {
            InternalMeasurementMode measurementMode;
            switch (MeasurementMode)
            {
                case MeasurementMode.SHTC3_CMD_CSE_NPM:
                    if (temperature)
                        measurementMode = InternalMeasurementMode.SHTC3_CMD_CSE_TF_NPM;
                    else
                        measurementMode = InternalMeasurementMode.SHTC3_CMD_CSE_RHF_NPM;
                    break;
                case MeasurementMode.SHTC3_CMD_CSE_LPM:
                    if (temperature)
                        measurementMode = InternalMeasurementMode.SHTC3_CMD_CSE_TF_LPM;
                    else
                        measurementMode = InternalMeasurementMode.SHTC3_CMD_CSE_RHF_LPM;
                    break;
                case MeasurementMode.SHTC3_CMD_CSD_NPM:
                    if (temperature)
                        measurementMode = InternalMeasurementMode.SHTC3_CMD_CSD_TF_NPM;
                    else
                        measurementMode = InternalMeasurementMode.SHTC3_CMD_CSD_RHF_NPM;
                    break;
                case MeasurementMode.SHTC3_CMD_CSD_LPM:
                    if (temperature)
                        measurementMode = InternalMeasurementMode.SHTC3_CMD_CSD_TF_LPM;
                    else
                        measurementMode = InternalMeasurementMode.SHTC3_CMD_CSD_RHF_LPM;
                    break;
                default:
                    return Status.SHTC3_Status_Error;
            }
            var result = I2CDevice.Write(new SpanByte(new byte[] { (byte)(((ushort)measurementMode) >> 8), (byte)(((ushort)measurementMode) & 0x00FF) }));
            if (result.Status == I2cTransferStatus.FullTransfer)
            {
                return Status.SHTC3_Status_Nominal;
            }
            return Status.SHTC3_Status_Error;
        }

        public void Sleep()
        {
            if (IsSleeping) //We are already asleep
                return;
            if (WriteCommand(Commands.SHTC3_CMD_SLEEP) == Status.SHTC3_Status_Nominal)
                IsSleeping = true;
        }

        public override void Start()
        {
            base.Start();
            IsSleeping = true; // Assume the sensor is asleep to begin (there won't be any harm in waking it up if it is already awake)
            WakeUp();
            ReadDeviceId();
        }

        public override void Stop()
        {
            WakeUp();
            WriteCommand(Commands.SHTC3_CMD_SFT_RST);
            base.Stop();
        }

        public void WakeUp()
        {
            if (!IsSleeping) //We are already awake
                return;
            if (WriteCommand(Commands.SHTC3_CMD_WAKE) == Status.SHTC3_Status_Nominal)
                IsSleeping = false;
        }

        public Status WriteCommand(Commands command)
        {
            var result = I2CDevice.Write(new SpanByte(new byte[] { (byte)(((ushort)command) >> 8), (byte)(((ushort)command) & 0x00FF) }));
            if (result.Status == I2cTransferStatus.FullTransfer)
                return Status.SHTC3_Status_Nominal;
            return Status.SHTC3_Status_Error;
        }

        public override void WriteData(params byte[] data)
        {
            I2CDevice.Write(data);
        }

        #endregion Public Methods
    }
}