using System;
using System.Device.I2c;
using System.Threading;

using TekuSP.Drivers.DriverBase;
using TekuSP.Drivers.DriverBase.Interfaces;

using TekuSP.Drivers.SHTC3.Enums;
using UnitsNet;
using UnitsNet.Units;

namespace TekuSP.Drivers.SHTC3
{
    /// <summary>
    /// Sensirion SHTC3 humidity and temperature sensor, https://www.mouser.com/datasheet/2/682/Sensirion_04202018_HT_DS_SHTC3_Preliminiary_D2-1323493.pdf
    /// </summary>
    public class SHTC3 : DriverBaseI2C, ISleepSupport, ITemperatureSensor, IHumiditySensor
    {
        #region Public Constructors

        /// <summary>
        /// Initializes SHTC3 with default I2C settings.
        /// </summary>
        /// <param name="I2CBusID">I2C bus ID.</param>
        /// <param name="deviceAddress">I2C device address.</param>
        public SHTC3(int I2CBusID, int deviceAddress = 0x70) : base("SHTC3", I2CBusID, deviceAddress)
        {
        }

        /// <summary>
        /// Initializes SHTC3 with custom I2C settings.
        /// </summary>
        /// <param name="I2CBusID">I2C bus ID.</param>
        /// <param name="connectionSettings">Custom I2C connection settings.</param>
        /// <param name="deviceAddress">I2C device address.</param>
        public SHTC3(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress = 0x70) : base("SHTC3", I2CBusID, connectionSettings, deviceAddress)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <inheritdoc/>
        public bool IsSleeping { get; private set; }

        /// <summary>Current measurement mode.</summary>
        public MeasurementMode MeasurementMode { get; private set; } = MeasurementMode.SHTC3_CMD_CSD_NPM;

        #endregion Public Properties

        #region Public Methods

        /// <inheritdoc/>
        public RelativeHumidity CalculateHumidity(RelativeHumidityUnit readHumidityType, double rawHumidity)
        {
            double humidityPercent = 100f * ((double)rawHumidity / 65535f);
            return RelativeHumidity.FromPercent(humidityPercent).ToUnit(readHumidityType);
        }

        /// <inheritdoc/>
        public Temperature CalculateTemperature(TemperatureUnit readTemperatureUnit, double rawTemperature)
        {
            double celsius = -45f + (175f * ((double)rawTemperature / 65535f));
            return Temperature.FromDegreesCelsius(celsius).ToUnit(readTemperatureUnit);
        }

        /// <summary>
        /// Validates CRC for a 16-bit packet.
        /// </summary>
        /// <param name="packet">Packet data.</param>
        /// <param name="cs">Checksum byte.</param>
        /// <returns>True if CRC is valid.</returns>
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
        /// <inheritdoc/>
        public override long ReadData(byte pointer)
        {
            WriteData(new byte[] { pointer });
            return -1;
        }

        /// <inheritdoc/>
        public override long ReadData(byte[] data)
        {
            return I2CDevice.Read(data).BytesTransferred;
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public RelativeHumidity ReadHumidity(RelativeHumidityUnit readHumidityType)
        {
            return CalculateHumidity(readHumidityType, ReadHumidity());
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public Temperature ReadTemperature(TemperatureUnit readTemperatureUnit)
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

        /// <summary>
        /// Puts the sensor into sleep mode to reduce power consumption.
        /// </summary>
        public void Sleep()
        {
            if (IsSleeping) //We are already asleep
                return;
            if (WriteCommand(Commands.SHTC3_CMD_SLEEP) == Status.SHTC3_Status_Nominal)
                IsSleeping = true;
        }

        /// <inheritdoc/>
        public override void Start()
        {
            base.Start();
            IsSleeping = true; // Assume the sensor is asleep to begin (there won't be any harm in waking it up if it is already awake)
            WakeUp();
            ReadDeviceId();
        }

        /// <inheritdoc/>
        public override void Stop()
        {
            WakeUp();
            WriteCommand(Commands.SHTC3_CMD_SFT_RST);
            base.Stop();
        }

        /// <summary>
        /// Wakes the sensor from sleep mode.
        /// </summary>
        public void WakeUp()
        {
            if (!IsSleeping) //We are already awake
                return;
            if (WriteCommand(Commands.SHTC3_CMD_WAKE) == Status.SHTC3_Status_Nominal)
                IsSleeping = false;
        }

        /// <summary>
        /// Writes a command to the sensor.
        /// </summary>
        /// <param name="command">The command to send.</param>
        /// <returns>The operation status.</returns>
        public Status WriteCommand(Commands command)
        {
            var result = I2CDevice.Write(new SpanByte(new byte[] { (byte)(((ushort)command) >> 8), (byte)(((ushort)command) & 0x00FF) }));
            if (result.Status == I2cTransferStatus.FullTransfer)
                return Status.SHTC3_Status_Nominal;
            return Status.SHTC3_Status_Error;
        }

        /// <inheritdoc/>
        public override void WriteData(params byte[] data)
        {
            I2CDevice.Write(data);
        }

        #endregion Public Methods
    }
}