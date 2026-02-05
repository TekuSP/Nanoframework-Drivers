using System;
using System.Device.I2c;
using System.Threading;

using TekuSP.Drivers.DriverBase;
using TekuSP.Drivers.DriverBase.Enums;
using TekuSP.Drivers.DriverBase.Interfaces;
using TekuSP.Drivers.SHT3x.Constants;
using TekuSP.Drivers.SHT3x.Enums;

using UnitsNet;
using UnitsNet.Units;

namespace TekuSP.Drivers.SHT3x
{
    /// <summary>
    /// Sensirion SHT3x humidity and temperature sensor family.
    /// </summary>
    public class SHT3x : DriverBaseI2C, ITemperatureSensor, IHumiditySensor, IHeaterControl, IPeriodicMeasurement, IStatusProvider
    {
        private SHT3xStatus _lastStatus;
        private Status _lastStatusResult;
        private bool _statusValid;

        #region Public Constructors

        /// <summary>
        /// Initializes SHT3x with default I2C settings.
        /// </summary>
        /// <param name="I2CBusID">I2C bus ID.</param>
        /// <param name="deviceAddress">I2C device address.</param>
        public SHT3x(int I2CBusID, int deviceAddress = 0x44)
            : base("SHT3x", I2CBusID, deviceAddress)
        {
            _lastStatusResult = Status.SHT3x_Status_Error;
            _statusValid = false;
        }

        /// <summary>
        /// Initializes SHT3x with custom I2C settings.
        /// </summary>
        /// <param name="I2CBusID">I2C bus ID.</param>
        /// <param name="connectionSettings">Custom I2C connection settings.</param>
        /// <param name="deviceAddress">I2C device address.</param>
        public SHT3x(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress = 0x44)
            : base("SHT3x", I2CBusID, connectionSettings, deviceAddress)
        {
            _lastStatusResult = Status.SHT3x_Status_Error;
            _statusValid = false;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the repeatability for single shot measurements.
        /// </summary>
        public Repeatability Repeatability { get; private set; } = Repeatability.High;

        /// <summary>
        /// Gets or sets the measurement mode for single shot measurements.
        /// </summary>
        public MeasurementMode MeasurementMode { get; private set; } = MeasurementMode.SingleShot;

        /// <inheritdoc/>
        public bool IsPeriodicMeasurementRunning { get; private set; }

        /// <inheritdoc/>
        public PeriodicMeasurementRate PeriodicMeasurementRate { get; private set; } = PeriodicMeasurementRate.EveryTwoSeconds;

        /// <inheritdoc/>
        public bool HasErrors
        {
            get
            {
                if (!_statusValid)
                    return _lastStatusResult != Status.SHT3x_Status_Nominal;

                return _lastStatusResult != Status.SHT3x_Status_Nominal
                    || _lastStatus.CommandStatusError
                    || _lastStatus.WriteDataCrcError;
            }
        }

        /// <inheritdoc/>
        public int StatusCode => (int)_lastStatusResult;

        /// <inheritdoc/>
        public string StatusSummary
        {
            get
            {
                if (_lastStatusResult != Status.SHT3x_Status_Nominal)
                    return _lastStatusResult.ToString();

                if (!_statusValid)
                    return "Status not available";

                if (_lastStatus.CommandStatusError)
                    return "Command error reported";
                if (_lastStatus.WriteDataCrcError)
                    return "Write CRC error reported";
                if (_lastStatus.SystemResetDetected)
                    return "System reset detected";
                if (_lastStatus.AlertPending)
                    return "Alert pending";

                return "OK";
            }
        }

        /// <inheritdoc/>
        public uint RawStatus => _statusValid ? _lastStatus.RawValue : 0u;

        /// <inheritdoc/>
        public bool IsHeaterActive
        {
            get
            {
                RefreshStatus();
                return _statusValid && _lastStatus.HeaterActive;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <inheritdoc/>
        public RelativeHumidity CalculateHumidity(RelativeHumidityUnit readHumidityType, double rawHumidity)
        {
            double humidityPercent = 100f * (rawHumidity / 65535f);
            return RelativeHumidity.FromPercent(humidityPercent).ToUnit(readHumidityType);
        }

        /// <inheritdoc/>
        public Temperature CalculateTemperature(TemperatureUnit readTemperatureUnit, double rawTemperature)
        {
            double celsius = -45f + (175f * (rawTemperature / 65535f));
            return Temperature.FromDegreesCelsius(celsius).ToUnit(readTemperatureUnit);
        }

        /// <summary>
        /// Reads the raw humidity ticks from the sensor.
        /// </summary>
        /// <returns>Raw humidity ticks, or -1 on failure.</returns>
        public double ReadHumidity()
        {
            Status status = ReadLatestMeasurementRaw(out ushort temperatureTicks, out ushort humidityTicks);
            if (status != Status.SHT3x_Status_Nominal)
                return -1;
            return humidityTicks;
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
        /// Not supported.
        /// </summary>
        /// <returns>Not supported.</returns>
        public override string ReadSerialNumber()
        {
            return "Not supported";
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <returns>Not supported.</returns>
        public override string ReadDeviceId()
        {
            return "Not supported";
        }

        /// <summary>
        /// Reads the raw temperature ticks from the sensor.
        /// </summary>
        /// <returns>Raw temperature ticks, or -1 on failure.</returns>
        public double ReadTemperature()
        {
            Status status = ReadLatestMeasurementRaw(out ushort temperatureTicks, out ushort humidityTicks);
            if (status != Status.SHT3x_Status_Nominal)
                return -1;
            return temperatureTicks;
        }

        /// <summary>
        /// Starts periodic measurement using the current repeatability.
        /// </summary>
        /// <param name="measurementRate">Measurement rate.</param>
        /// <returns>Operation status.</returns>
        public Status StartPeriodicMeasurement(PeriodicMeasurementRate measurementRate)
        {
            return StartPeriodicMeasurement(measurementRate, Repeatability);
        }

        /// <summary>
        /// Prepares a single shot measurement by selecting repeatability and measurement mode.
        /// </summary>
        /// <param name="repeatability">Measurement repeatability.</param>
        /// <param name="measurementMode">Measurement mode.</param>
        /// <returns>Operation status.</returns>
        public Status StartSingleShotMeasurement(Repeatability repeatability, MeasurementMode measurementMode)
        {
            if (IsPeriodicMeasurementRunning)
                StopPeriodicMeasurement();

            Repeatability = repeatability;
            MeasurementMode = measurementMode;
            return Status.SHT3x_Status_Nominal;
        }

        /// <summary>
        /// Prepares a single shot measurement using current repeatability and measurement mode.
        /// </summary>
        /// <returns>Operation status.</returns>
        public Status StartSingleShotMeasurement()
        {
            if (IsPeriodicMeasurementRunning)
                StopPeriodicMeasurement();

            return Status.SHT3x_Status_Nominal;
        }

        /// <summary>
        /// Starts periodic measurement with the specified rate and repeatability.
        /// </summary>
        /// <param name="measurementRate">Measurement rate.</param>
        /// <param name="repeatability">Measurement repeatability.</param>
        /// <returns>Operation status.</returns>
        public Status StartPeriodicMeasurement(PeriodicMeasurementRate measurementRate, Repeatability repeatability)
        {
            if (IsPeriodicMeasurementRunning)
            {
                if (PeriodicMeasurementRate == measurementRate && Repeatability == repeatability)
                    return Status.SHT3x_Status_Nominal;

                StopPeriodicMeasurement();
            }

            Repeatability = repeatability;
            Commands command = GetPeriodicMeasurementCommand(measurementRate, repeatability);
            Status status = WriteCommand(command);
            if (status == Status.SHT3x_Status_Nominal)
            {
                IsPeriodicMeasurementRunning = true;
                PeriodicMeasurementRate = measurementRate;
            }
            return status;
        }

        /// <summary>
        /// Stops periodic measurement.
        /// </summary>
        /// <returns>Operation status.</returns>
        public Status StopPeriodicMeasurement()
        {
            Status status = WriteCommand(Commands.StopMeasurement);
            IsPeriodicMeasurementRunning = false;
            return status;
        }

        /// <summary>
        /// Reads a periodic measurement sample.
        /// </summary>
        /// <param name="temperatureTicks">Raw temperature ticks.</param>
        /// <param name="humidityTicks">Raw humidity ticks.</param>
        /// <returns>Operation status.</returns>
        public Status ReadPeriodicMeasurementRaw(out ushort temperatureTicks, out ushort humidityTicks)
        {
            temperatureTicks = 0;
            humidityTicks = 0;

            if (WriteCommand(Commands.ReadMeasurement) != Status.SHT3x_Status_Nominal)
                return Status.SHT3x_Status_Error;

            SpanByte buffer = new SpanByte(new byte[6]);
            var readStatus = I2CDevice.Read(buffer);
            if (readStatus.BytesTransferred != 6 || readStatus.Status != I2cTransferStatus.FullTransfer)
                return Status.SHT3x_Status_Read_Failed;

            ushort rawTemperature = (ushort)((buffer[0] << 8) | buffer[1]);
            ushort rawHumidity = (ushort)((buffer[3] << 8) | buffer[4]);

            if (!CheckCrc(rawTemperature, buffer[2]) || !CheckCrc(rawHumidity, buffer[5]))
                return Status.SHT3x_Status_CRC_Fail;

            temperatureTicks = rawTemperature;
            humidityTicks = rawHumidity;
            return Status.SHT3x_Status_Nominal;
        }

        /// <inheritdoc/>
        public Temperature ReadTemperature(TemperatureUnit readTemperatureUnit)
        {
            return CalculateTemperature(readTemperatureUnit, ReadTemperature());
        }

        /// <summary>
        /// Reads the status register.
        /// </summary>
        /// <param name="statusRegister">Status register value.</param>
        /// <returns>Operation status.</returns>
        public Status ReadStatusRegister(out ushort statusRegister)
        {
            statusRegister = 0;
            if (WriteCommand(Commands.ReadStatusRegister) != Status.SHT3x_Status_Nominal)
                return Status.SHT3x_Status_Error;

            SpanByte buffer = new SpanByte(new byte[3]);
            var readStatus = I2CDevice.Read(buffer);
            if (readStatus.BytesTransferred != 3 || readStatus.Status != I2cTransferStatus.FullTransfer)
                return Status.SHT3x_Status_Read_Failed;

            ushort rawStatus = (ushort)((buffer[0] << 8) | buffer[1]);
            if (!CheckCrc(rawStatus, buffer[2]))
                return Status.SHT3x_Status_CRC_Fail;

            statusRegister = rawStatus;
            return Status.SHT3x_Status_Nominal;
        }

        /// <summary>
        /// Clears the status register.
        /// </summary>
        /// <returns>Operation status.</returns>
        public Status ClearStatusRegister()
        {
            return WriteCommand(Commands.ClearStatusRegister);
        }

        /// <summary>
        /// Enables the internal heater.
        /// </summary>
        /// <returns>Operation status.</returns>
        public Status EnableHeater()
        {
            return WriteCommand(Commands.EnableHeater);
        }

        /// <summary>
        /// Disables the internal heater.
        /// </summary>
        /// <returns>Operation status.</returns>
        public Status DisableHeater()
        {
            return WriteCommand(Commands.DisableHeater);
        }

        /// <inheritdoc/>
        public void RefreshStatus()
        {
            _lastStatusResult = ReadStatusRegister(out ushort rawStatus);
            if (_lastStatusResult == Status.SHT3x_Status_Nominal)
            {
                _lastStatus = ParseStatus(rawStatus);
                _statusValid = true;
            }
            else
            {
                _statusValid = false;
            }
        }

        /// <summary>
        /// Reads and parses the status register.
        /// </summary>
        /// <param name="status">Parsed status.</param>
        /// <returns>Operation status.</returns>
        public Status GetStatus(out SHT3xStatus status)
        {
            RefreshStatus();
            status = _lastStatus;
            return _lastStatusResult;
        }

        /// <summary>
        /// Performs a soft reset.
        /// </summary>
        /// <returns>Operation status.</returns>
        public Status SoftReset()
        {
            return WriteCommand(Commands.SoftReset);
        }

        /// <summary>
        /// Reads a single shot measurement.
        /// </summary>
        /// <param name="temperatureTicks">Raw temperature ticks.</param>
        /// <param name="humidityTicks">Raw humidity ticks.</param>
        /// <returns>Operation status.</returns>
        public Status ReadMeasurementRaw(out ushort temperatureTicks, out ushort humidityTicks)
        {
            temperatureTicks = 0;
            humidityTicks = 0;

            Commands command = GetSingleShotCommand(Repeatability, MeasurementMode == MeasurementMode.SingleShotClockStretching);
            if (WriteCommand(command) != Status.SHT3x_Status_Nominal)
                return Status.SHT3x_Status_Error;

            Thread.Sleep(GetMeasurementDelayMs(Repeatability));

            SpanByte buffer = new SpanByte(new byte[6]);
            bool gotData = false;
            for (int attempt = 0; attempt < SHT3xConstants.MaxReadRetries && !gotData; attempt++)
            {
                var readStatus = I2CDevice.Read(buffer);
                if (readStatus.BytesTransferred == 6 && readStatus.Status == I2cTransferStatus.FullTransfer)
                    gotData = true;
                else
                    Thread.Sleep(5);
            }

            if (!gotData)
                return Status.SHT3x_Status_Read_Failed;

            ushort rawTemperature = (ushort)((buffer[0] << 8) | buffer[1]);
            ushort rawHumidity = (ushort)((buffer[3] << 8) | buffer[4]);

            if (!CheckCrc(rawTemperature, buffer[2]) || !CheckCrc(rawHumidity, buffer[5]))
                return Status.SHT3x_Status_CRC_Fail;

            temperatureTicks = rawTemperature;
            humidityTicks = rawHumidity;
            return Status.SHT3x_Status_Nominal;
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="pointer">Data to write.</param>
        /// <returns>-1.</returns>
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
        public override void Start()
        {
            base.Start();
            SoftReset();
            StartSingleShotMeasurement();
        }

        /// <inheritdoc/>
        public override void Stop()
        {
            StopPeriodicMeasurement();
            base.Stop();
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
                return Status.SHT3x_Status_Nominal;
            return Status.SHT3x_Status_Error;
        }

        /// <inheritdoc/>
        public override void WriteData(params byte[] data)
        {
            I2CDevice.Write(data);
        }

        /// <inheritdoc/>
        void IHeaterControl.EnableHeater()
        {
            EnableHeater();
        }

        /// <inheritdoc/>
        void IHeaterControl.DisableHeater()
        {
            DisableHeater();
        }

        /// <inheritdoc/>
        void IPeriodicMeasurement.StartPeriodicMeasurement(PeriodicMeasurementRate measurementRate)
        {
            StartPeriodicMeasurement(measurementRate);
        }

        /// <inheritdoc/>
        void IPeriodicMeasurement.StopPeriodicMeasurement()
        {
            StopPeriodicMeasurement();
        }

        #endregion Public Methods

        #region Private Methods

        private static Commands GetSingleShotCommand(Repeatability repeatability, bool clockStretching)
        {
            switch (repeatability)
            {
                case Repeatability.High:
                    return clockStretching
                        ? Commands.MeasureSingleShotHighRepeatabilityClockStretching
                        : Commands.MeasureSingleShotHighRepeatability;
                case Repeatability.Medium:
                    return clockStretching
                        ? Commands.MeasureSingleShotMediumRepeatabilityClockStretching
                        : Commands.MeasureSingleShotMediumRepeatability;
                case Repeatability.Low:
                    return clockStretching
                        ? Commands.MeasureSingleShotLowRepeatabilityClockStretching
                        : Commands.MeasureSingleShotLowRepeatability;
                default:
                    return Commands.MeasureSingleShotHighRepeatability;
            }
        }

        private static Commands GetPeriodicMeasurementCommand(PeriodicMeasurementRate measurementRate, Repeatability repeatability)
        {
            switch (measurementRate)
            {
                case PeriodicMeasurementRate.EveryTwoSeconds:
                    return GetPeriodicCommandForRepeatability(
                        repeatability,
                        Commands.StartMeasurement05MpsHighRepeatability,
                        Commands.StartMeasurement05MpsMediumRepeatability,
                        Commands.StartMeasurement05MpsLowRepeatability);
                case PeriodicMeasurementRate.OnePerSecond:
                    return GetPeriodicCommandForRepeatability(
                        repeatability,
                        Commands.StartMeasurement1MpsHighRepeatability,
                        Commands.StartMeasurement1MpsMediumRepeatability,
                        Commands.StartMeasurement1MpsLowRepeatability);
                case PeriodicMeasurementRate.TwoPerSecond:
                    return GetPeriodicCommandForRepeatability(
                        repeatability,
                        Commands.StartMeasurement2MpsHighRepeatability,
                        Commands.StartMeasurement2MpsMediumRepeatability,
                        Commands.StartMeasurement2MpsLowRepeatability);
                case PeriodicMeasurementRate.FourPerSecond:
                    return GetPeriodicCommandForRepeatability(
                        repeatability,
                        Commands.StartMeasurement4MpsHighRepeatability,
                        Commands.StartMeasurement4MpsMediumRepeatability,
                        Commands.StartMeasurement4MpsLowRepeatability);
                case PeriodicMeasurementRate.TenPerSecond:
                    return GetPeriodicCommandForRepeatability(
                        repeatability,
                        Commands.StartMeasurement10MpsHighRepeatability,
                        Commands.StartMeasurement10MpsMediumRepeatability,
                        Commands.StartMeasurement10MpsLowRepeatability);
                default:
                    return Commands.StartMeasurement1MpsHighRepeatability;
            }
        }

        private static Commands GetPeriodicCommandForRepeatability(Repeatability repeatability, Commands high, Commands medium, Commands low)
        {
            switch (repeatability)
            {
                case Repeatability.High:
                    return high;
                case Repeatability.Medium:
                    return medium;
                case Repeatability.Low:
                    return low;
                default:
                    return high;
            }
        }

        private static int GetMeasurementDelayMs(Repeatability repeatability)
        {
            switch (repeatability)
            {
                case Repeatability.High:
                    return (int)MeasurementDelayMs.HighRepeatability;
                case Repeatability.Medium:
                    return (int)MeasurementDelayMs.MediumRepeatability;
                case Repeatability.Low:
                    return (int)MeasurementDelayMs.LowRepeatability;
                default:
                    return (int)MeasurementDelayMs.HighRepeatability;
            }
        }

        private static SHT3xStatus ParseStatus(ushort rawStatus)
        {
            return new SHT3xStatus
            {
                RawValue = rawStatus,
                AlertPending = (rawStatus & SHT3xConstants.StatusAlertPendingMask) != 0,
                HeaterActive = (rawStatus & SHT3xConstants.StatusHeaterOnMask) != 0,
                HumidityTrackingAlert = (rawStatus & SHT3xConstants.StatusHumidityTrackingAlertMask) != 0,
                TemperatureTrackingAlert = (rawStatus & SHT3xConstants.StatusTemperatureTrackingAlertMask) != 0,
                SystemResetDetected = (rawStatus & SHT3xConstants.StatusSystemResetMask) != 0,
                CommandStatusError = (rawStatus & SHT3xConstants.StatusCommandStatusMask) != 0,
                WriteDataCrcError = (rawStatus & SHT3xConstants.StatusWriteCrcStatusMask) != 0
            };
        }

        private Status ReadLatestMeasurementRaw(out ushort temperatureTicks, out ushort humidityTicks)
        {
            if (IsPeriodicMeasurementRunning)
                return ReadPeriodicMeasurementRaw(out temperatureTicks, out humidityTicks);

            return ReadMeasurementRaw(out temperatureTicks, out humidityTicks);
        }

        private static bool CheckCrc(ushort packet, byte checksum)
        {
            byte upper = (byte)(packet >> 8);
            byte lower = (byte)(packet & 0x00FF);
            byte[] data = new byte[] { upper, lower };
            byte crc = 0xFF;
            const byte poly = 0x31;

            for (int indi = 0; indi < data.Length; indi++)
            {
                crc ^= data[indi];
                for (int indj = 0; indj < 8; indj++)
                {
                    if ((crc & 0x80) != 0)
                        crc = (byte)((crc << 1) ^ poly);
                    else
                        crc <<= 1;
                }
            }

            return crc == checksum;
        }

        #endregion Private Methods
    }
}
