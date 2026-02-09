using System;
using System.Device.I2c;

using TekuSP.Drivers.DriverBase;
using TekuSP.Drivers.DriverBase.Interfaces;
using TekuSP.Drivers.QMP6988.Helpers;
using TekuSP.Drivers.QMP6988.Enums;
using TekuSP.Drivers.QMP6988.Constants;

using UnitsNet;
using UnitsNet.Units;

namespace TekuSP.Drivers.QMP6988
{
    /// <summary>
    /// Driver for the QMP6988 barometric pressure and temperature sensor.
    /// Provides raw reads, compensated temperature/pressure values using factory OTP calibration,
    /// and support for periodic measurement modes.
    /// </summary>
    public class QMP6988 : DriverBaseI2C, ITemperatureSensor, IPressureSensor, IPeriodicMeasurement, IOversamplingControl, IIirFilterControl
    {
        /// <summary>
        /// Creates a new QMP6988 driver instance using the provided I2C bus ID and device address.
        /// </summary>
        /// <param name="I2CBusID">I2C bus controller ID.</param>
        /// <param name="deviceAddress">I2C device address (7-bit).</param>
        public QMP6988(int I2CBusID, int deviceAddress = 0x70) : base("QMP9688", I2CBusID, deviceAddress)
        {
        }

        /// <summary>
        /// Creates a new QMP6988 driver instance using provided I2C connection settings.
        /// </summary>
        /// <param name="I2CBusID">I2C bus controller ID.</param>
        /// <param name="connectionSettings">I2C connection settings to use.</param>
        /// <param name="deviceAddress">I2C device address (7-bit).</param>
        public QMP6988(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress = 0x70) : base("QMP9688", I2CBusID, connectionSettings, deviceAddress)
        {
        }

        public CalibrationData FactoryCalibrationData
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns whether the sensor is in Normal (periodic) mode.
        /// </summary>
        public bool IsPeriodicMeasurementRunning => GetPowerMode() == PowerMode.Normal;

        /// <summary>
        /// Reads the configured standby (period) setting from <c>IO_SETUP</c>.
        /// </summary>
        public byte PeriodicMeasurementRate => (byte)((ReadRegister((byte)ControlRegister.IO_SETUP) >> Qmp6988Constants.IoSetupStandbyShift) & 0x07);

        /// <summary>
        /// Multiplier applied to estimated measurement time when computing timeouts. Default is 3.0 (conservative).
        /// </summary>
        public double TimeoutMarginMultiplier { get; set; } = 3.0;

        private uint LastMeasuredPressureRaw
        {
            get; set;
        }

        private uint LastMeasuredTemperatureRaw
        {
            get; set;
        }

        /// <inheritdoc/>
        Pressure IPressureSensor.CalculatePressure(PressureUnit type, double rawPressure) => CalculatePressure(type, (uint)rawPressure);

        /// <summary>
        /// Compensates a raw pressure ADC value using the factory calibration coefficients loaded from OTP.
        /// </summary>
        /// <param name="type">The pressure unit to return.</param>
        /// <param name="rawPressure">Raw 24-bit pressure ADC value.</param>
        /// <returns>Compensated <see cref="Pressure"/> in the requested unit.</returns>
        public Pressure CalculatePressure(PressureUnit type, uint rawPressure)
        {
            if (FactoryCalibrationData == null)
            {
                throw new InvalidOperationException("Calibration data not loaded. Call Start() to initialize the sensor and load calibration data.");
            }
            LastMeasuredPressureRaw = rawPressure;
            // Use the factory calibration data to compensate the raw pressure reading
            return FactoryCalibrationData.CompensatePressure(rawPressure, LastMeasuredTemperatureRaw, type);
        }

        /// <inheritdoc/>
        Temperature ITemperatureSensor.CalculateTemperature(TemperatureUnit type, double rawTemperature) => CalculateTemperature(type, (uint)rawTemperature);

        /// <summary>
        /// Compensates a raw temperature ADC value using the factory calibration coefficients loaded from OTP.
        /// </summary>
        /// <param name="type">The temperature unit to return.</param>
        /// <param name="rawTemperature">Raw 24-bit temperature ADC value.</param>
        /// <returns>Compensated <see cref="Temperature"/> in the requested unit.</returns>
        public Temperature CalculateTemperature(TemperatureUnit type, uint rawTemperature)
        {
            if (FactoryCalibrationData == null)
            {
                throw new InvalidOperationException("Calibration data not loaded. Call Start() to initialize the sensor and load calibration data.");
            }
            LastMeasuredTemperatureRaw = rawTemperature;
            // Use the factory calibration data to compensate the raw temperature reading
            return FactoryCalibrationData.CompensateTemperature(rawTemperature, type);
        }

        /// <summary>
        /// Estimates the measurement time (Duration) needed for a measurement given the oversampling settings.
        /// Values are conservative and intended for timeout/ scheduling guidance.
        /// </summary>
        /// <summary>
        /// Estimates the measurement time using the datasheet table for common oversampling combinations.
        /// Falls back to a simple multiplier-based estimate for unsupported combinations.
        /// </summary>
        public Duration EstimateMeasurementTime(Oversampling temp, Oversampling press)
        {
            // Datasheet table (1.6 Characteristics by Oversampling setting, force-mode):
            // High speed    => P=x2  T=x1  => 5.5 ms
            // Low power     => P=x4  T=x1  => 7.2 ms
            // Standard      => P=x8  T=x1  => 10.6 ms
            // High accuracy => P=x16 T=x2  => 18.3 ms
            // Ultra high    => P=x32 T=x4  => 33.7 ms
            if (press == Enums.Oversampling.x2 && temp == Enums.Oversampling.x1) return Duration.FromMilliseconds((long)Math.Ceiling(5.5));
            if (press == Enums.Oversampling.x4 && temp == Enums.Oversampling.x1) return Duration.FromMilliseconds((long)Math.Ceiling(7.2));
            if (press == Enums.Oversampling.x8 && temp == Enums.Oversampling.x1) return Duration.FromMilliseconds((long)Math.Ceiling(10.6));
            if (press == Enums.Oversampling.x16 && temp == Enums.Oversampling.x2) return Duration.FromMilliseconds((long)Math.Ceiling(18.3));
            if (press == Enums.Oversampling.x32 && temp == Enums.Oversampling.x4) return Duration.FromMilliseconds((long)Math.Ceiling(33.7));

            // Fallback: basic per-sample multiplier (conservative)
            const double baseMsPerSample = 2.0;
            int tMul = OversamplingToMultiplier(temp);
            int pMul = OversamplingToMultiplier(press);
            double totalMs = (tMul + pMul) * baseMsPerSample + 2.0;
            return Duration.FromMilliseconds((long)Math.Ceiling(totalMs));
        }

        /// <summary>
        /// Gets the currently configured IIR filter value.
        /// </summary>
        public IirFilter GetIirFilter()
        {
            return (IirFilter)ReadRegister((byte)ControlRegister.IIR_FILTER);
        }

        byte IIirFilterControl.GetIirFilter() => (byte)GetIirFilter();

        /// <summary>
        /// Gets the current oversampling configuration.
        /// </summary>
        public void GetOversampling(out Oversampling temp, out Oversampling press)
        {
            byte ctrl = ReadRegister((byte)ControlRegister.CTRL_MEAS);
            temp = (Oversampling)((ctrl & Qmp6988Constants.CtrlTempMask) >> Qmp6988Constants.CtrlTempShift);
            press = (Oversampling)((ctrl & Qmp6988Constants.CtrlPressMask) >> Qmp6988Constants.CtrlPressShift);
        }

        void IOversamplingControl.GetOversampling(out byte temp, out byte press)
        {
            GetOversampling(out Oversampling t, out Oversampling p); temp = (byte)t; press = (byte)p;
        }

        /// <summary>
        /// Reads the raw OTP area bytes from <c>0xA0</c> to <c>0xB8</c> (inclusive).
        /// Useful for diagnostics or unit-validation of factory coefficients.
        /// </summary>
        /// <returns>Raw bytes read from the OTP area (25 bytes).</returns>
        public byte[] GetRawOtpBytes()
        {
            const byte start = 0xA0;
            const int len = 0xB8 - 0xA0 + 1; // 25 bytes
            byte[] buff = new byte[len];
            // Read entire OTP area starting at 0xA0
            I2CDevice.WriteRead(new byte[] { start }, buff);
            return buff;
        }

        /// <inheritdoc/>
        public override long ReadData(byte pointer)
        {
            WriteData(new byte[] { pointer });
            return -1;
        }

        /// <inheritdoc/>
        public override long ReadData(byte[] data)
        {
            SpanByte buff = new SpanByte(data);
            I2CDevice.Read(buff);
            return data.Length;
        }

        /// <inheritdoc/>
        public override string ReadDeviceId()
        {
            return ReadRegister((byte)ControlRegister.CHIP_ID).ToString("X2");
        }

        /// <inheritdoc/>
        public override string ReadManufacturerId()
        {
            return "QST";
        }

        /// <summary>
        /// Performs a single forced measurement and returns both compensated temperature and pressure values.
        /// This avoids performing two separate forced measurements when both values are required.
        /// </summary>
        /// <param name="tempUnit">Temperature unit to return.</param>
        /// <param name="pressUnit">Pressure unit to return.</param>
        /// <param name="temperature">Output compensated temperature.</param>
        /// <param name="pressure">Output compensated pressure.</param>
        public void ReadMeasurement(TemperatureUnit tempUnit, PressureUnit pressUnit, out Temperature temperature, out Pressure pressure)
        {
            TriggerAndReadRaw(out uint rawTemp, out uint rawPress);
            temperature = CalculateTemperature(tempUnit, rawTemp);
            pressure = CalculatePressure(pressUnit, rawPress);
        }

        /// <summary>
        /// Triggers a forced measurement and returns the raw pressure ADC value (24-bit).
        /// The method reads temperature first to ensure compensation uses the corresponding temperature.
        /// </summary>
        /// <returns>Raw pressure ADC value (24-bit).</returns>
        public double ReadPressure()
        {
            TriggerAndReadRaw(out _, out uint p);
            return p;
        }

        /// <summary>
        /// Triggers a forced measurement and returns the compensated pressure value in the requested unit.
        /// </summary>
        /// <param name="type">Desired pressure unit.</param>
        /// <returns>Compensated pressure.</returns>
        public Pressure ReadPressure(PressureUnit type)
        {
            double raw = ReadPressure();
            return CalculatePressure(type, (uint)raw);
        }

        /// <inheritdoc/>
        public override string ReadSerialNumber()
        {
            return "Not Supported";
        }

        /// <summary>
        /// Triggers a forced temperature measurement and returns the raw temperature ADC value (24-bit).
        /// </summary>
        /// <returns>Raw temperature ADC value (24-bit).</returns>
        public double ReadTemperature()
        {
            // Trigger a forced measurement and then read temperature registers
            TriggerForcedMeasurement();

            byte[] buff = new byte[3];
            I2CDevice.WriteRead(new byte[] { (byte)Enums.DataRegister.TEMP_MSB }, buff);

            uint raw = ((uint)buff[0] << 16) | ((uint)buff[1] << 8) | (uint)buff[2];

            // If the first read returns an impossible zero temperature (a startup artifact), retry once.
            if (raw == 0)
            {
                TriggerForcedMeasurement();
                I2CDevice.WriteRead(new byte[] { (byte)Enums.DataRegister.TEMP_MSB }, buff);
                raw = ((uint)buff[0] << 16) | ((uint)buff[1] << 8) | (uint)buff[2];
            }

            LastMeasuredTemperatureRaw = raw;
            return raw;
        }

        /// <summary>
        /// Triggers a forced measurement and returns the compensated temperature in the requested unit.
        /// </summary>
        /// <param name="readTemperatureUnit">Desired temperature unit.</param>
        /// <returns>Compensated temperature.</returns>
        public Temperature ReadTemperature(TemperatureUnit readTemperatureUnit)
        {
            double raw = ReadTemperature();
            return CalculateTemperature(readTemperatureUnit, (uint)raw);
        }

        /// <summary>
        /// Sets the IIR filter coefficient (public API).
        /// </summary>
        public void SetIirFilter(IirFilter filter)
        {
            WriteRegister((byte)ControlRegister.IIR_FILTER, (byte)filter);
        }

        void IIirFilterControl.SetIirFilter(byte filter) => SetIirFilter((IirFilter)filter);

        /// <summary>
        /// Sets temperature and pressure oversampling fields in <c>CTRL_MEAS</c>.
        /// </summary>
        public void SetOversampling(Oversampling temp, Oversampling press)
        {
            byte ctrl = ReadRegister((byte)ControlRegister.CTRL_MEAS);
            // Clear temp and press fields
            ctrl = (byte)(ctrl & ~(Qmp6988Constants.CtrlTempMask | Qmp6988Constants.CtrlPressMask));
            ctrl = (byte)(ctrl | ((byte)((byte)temp << Qmp6988Constants.CtrlTempShift)) | ((byte)((byte)press << Qmp6988Constants.CtrlPressShift)));
            WriteRegister((byte)ControlRegister.CTRL_MEAS, ctrl);
        }

        // Explicit interface implementations for DriverBase generic interfaces (byte-based)
        void IOversamplingControl.SetOversampling(byte temp, byte press) => SetOversampling((Oversampling)temp, (Oversampling)press);

        /// <summary>
        /// Sets the standby (t_standby) field in <c>IO_SETUP</c>.
        /// </summary>
        public void SetStandby(StandbyTime st)
        {
            byte ioSetup = ReadRegister((byte)ControlRegister.IO_SETUP);
            // Clear standby bits (bits 7..5) then set new value
            ioSetup = (byte)((ioSetup & (byte)(0xFF ^ Qmp6988Constants.IoSetupStandbyMask)) | (byte)(((byte)st & 0x07) << Qmp6988Constants.IoSetupStandbyShift));
            WriteRegister((byte)ControlRegister.IO_SETUP, ioSetup);
        }

        /// <inheritdoc/>
        public override void Start()
        {
            base.Start();

            // Soft reset and wait for POR timing
            WriteRegister((byte)ControlRegister.RESET, Qmp6988Constants.ResetValue);
            System.Threading.Thread.Sleep(20);

            // Verify CHIP ID
            byte chip = ReadRegister((byte)ControlRegister.CHIP_ID);
            if (chip != Qmp6988Constants.ExpectedChipId)
                throw new InvalidOperationException($"Unexpected CHIP ID: 0x{chip:X2}");

            // Read calibration coefficients from OTP and validate them
            ReadCalibrationCoefficients();
            if (!ValidateCalibrationData(out string validateReason))
                throw new InvalidOperationException($"Calibration data validation failed: {validateReason}");

            // Apply M5Stack-recommended defaults: IIR filter = Coeff_4, temp x1, press x8
            WriteRegister((byte)ControlRegister.IIR_FILTER, (byte)IirFilter.Coeff_4);

            // Configure IO_SETUP standby time to Ms250
            SetStandby(StandbyTime.Ms250);

            // Configure CTRL_MEAS with M5Stack's recommended oversampling (temp x1, press x8) and ensure Sleep mode
            SetOversampling(Oversampling.x1, Oversampling.x8);
            SetPowerMode(PowerMode.Sleep);

            // Allow the sensor to complete any startup/first conversion (avoid first-sample artifacts)
            System.Threading.Thread.Sleep(100);

            // Discard one initial measurement (commonly flaky) so subsequent reads are stable
            TriggerAndReadRaw(out _, out _);
        }

        /// <summary>
        /// Starts periodic measurements using a typed <see cref="StandbyTime"/> value.
        /// </summary>
        /// <param name="standby">Standby time between measurements.</param>
        public void StartPeriodicMeasurement(StandbyTime standby)
        {
            SetStandby(standby);
            SetPowerMode(PowerMode.Normal);
        }

        /// <inheritdoc/>
        void IPeriodicMeasurement.StartPeriodicMeasurement(byte measurementRate)
        {
            if (measurementRate > 7)
                throw new ArgumentOutOfRangeException(nameof(measurementRate));

            StartPeriodicMeasurement((StandbyTime)measurementRate);
        }

        /// <inheritdoc/>
        public override void Stop()
        {
            SetPowerMode(PowerMode.Sleep);
            base.Stop();
            FactoryCalibrationData = null;
            LastMeasuredTemperatureRaw = 0;
            LastMeasuredPressureRaw = 0;
        }

        /// <summary>
        /// Performs a device-specific restart: attempt a soft reset while running, then call the base restart
        /// (Stop/Start) to re-initialize the device and re-load calibration data.
        /// </summary>
        public override void Restart()
        {
            // If device is running, attempt a soft-reset first (avoids stale internal state)
            if (IsPeriodicMeasurementRunning || IsRunning)
            {
                try
                {
                    WriteRegister((byte)ControlRegister.RESET, Qmp6988Constants.ResetValue);
                    System.Threading.Thread.Sleep(20);
                }
                catch
                {
                    // Ignore write errors here and fall back to base restart which will Stop/Start the bus.
                }
            }

            // Use the base class Restart (Stop + Start) to ensure full reinitialization.
            base.Restart();
        }

        /// <summary>
        /// Stops periodic (normal) measurement mode and places the device into sleep mode.
        /// </summary>
        public void StopPeriodicMeasurement()
        {
            // Set device to Sleep mode
            SetPowerMode(PowerMode.Sleep);
        }

        /// <summary>
        /// Performs basic validation of the loaded factory calibration coefficients.
        /// Checks for obvious invalid values and returns a human-readable reason when invalid.
        /// </summary>
        /// <param name="reason">A text reason describing the validation failure, if any.</param>
        /// <returns>True if the calibration data looks valid; false otherwise.</returns>
        public bool ValidateCalibrationData(out string reason)
        {
            reason = null;
            if (FactoryCalibrationData == null)
            {
                reason = "Calibration data not loaded.";
                return false;
            }

            // Ensure not all other coefficients are zero (unlikely/invalid OTP)
            if (FactoryCalibrationData.A1 == 0 || FactoryCalibrationData.A2 == 0 ||
                FactoryCalibrationData.Bt1 == 0 || FactoryCalibrationData.Bt2 == 0 ||
                FactoryCalibrationData.Bp1 == 0 || FactoryCalibrationData.B11 == 0 ||
                FactoryCalibrationData.Bp2 == 0 || FactoryCalibrationData.B12 == 0 ||
                FactoryCalibrationData.B21 == 0 || FactoryCalibrationData.Bp3 == 0)
            {
                reason = "Some or all calibration coefficients (besides A0/B00) are zero.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Calculates approximate altitude in meters using the barometric formula and the supplied pressure and temperature.
        /// Uses the standard international barometric formula (troposphere approximation) — same approach as the M5Stack reference driver.
        /// </summary>
        /// <param name="pressure">Measured pressure (UnitsNet Pressure).</param>
        /// <param name="temperature">Measured temperature (UnitsNet Temperature).</param>
        /// <param name="targetUnit">The target unit for the altitude.</param>
        /// <returns>Altitude as a UnitsNet.Length.</returns>
        public Length CalculateAltitude(UnitsNet.Pressure pressure, UnitsNet.Temperature temperature, UnitsNet.Units.LengthUnit targetUnit)
        {
            // Avoid divide-by-zero or invalid pressures
            double p = Math.Max(1.0, pressure.Pascals);
            double t = temperature.DegreesCelsius;

            // Barometric formula (approximate for troposphere):
            // h = ( (P0 / P)^(1/5.257) - 1 ) * (T + 273.15) / 0.0065
            const double P0 = 101325.0; // sea-level reference pressure in Pa
            double exponent = 1.0 / 5.257;
            double h = (Math.Pow(P0 / p, exponent) - 1.0) * (t + 273.15) / 0.0065;
            return Length.FromMeters(h).ToUnit(targetUnit);
        }

        /// <inheritdoc/>
        public override void WriteData(byte[] data)
        {
            I2CDevice.Write(new SpanByte(data));
        }

        /// <summary>
        /// Reads and returns the current power mode from the <c>CTRL_MEAS</c> register.
        /// </summary>
        private PowerMode GetPowerMode()
        {
            byte ctrl = ReadRegister((byte)ControlRegister.CTRL_MEAS);
            return (PowerMode)(ctrl & Qmp6988Constants.PowerModeMask);
        }

        private int OversamplingToMultiplier(Oversampling o)
        {
            switch (o)
            {
                case Enums.Oversampling.Skipped: return 0;
                case Enums.Oversampling.x1: return 1;
                case Enums.Oversampling.x2: return 2;
                case Enums.Oversampling.x4: return 4;
                case Enums.Oversampling.x8: return 8;
                case Enums.Oversampling.x16: return 16;
                case Enums.Oversampling.x32: return 32;
                case Enums.Oversampling.x64: return 64;
                default: return 1;
            }
        }

        private void ReadCalibrationCoefficients()
        {
            // Read raw OTP block once (0xA0..0xB8)
            byte[] otp = GetRawOtpBytes();
            if (otp == null || otp.Length != 25)
                throw new InvalidOperationException("Failed to read OTP coefficients.");

            // Per datasheet and M5Stack mapping:
            // B00: bytes at 0xA0 (otp[0]) MSB, 0xA1 (otp[1]) mid, and upper nibble of otp[24] (0xB8)
            int b00Combined = (otp[0] << 12) | (otp[1] << 4) | ((otp[24] & 0xF0) >> 4);
            // Match C++ sequence: combine -> left-shift 12 -> arithmetic right-shift 12 to sign-extend 20-bit two's-complement
            int b00Signed = (int)(b00Combined << 12);
            b00Signed = b00Signed >> 12;
            int b00 = b00Signed;

            // bt1..bp3 are stored sequentially at offsets 0xA2..0xB1 (otp[2]..otp[17]) as 16-bit signed words
            short bt1 = (short)((otp[2] << 8) | otp[3]);
            short bt2 = (short)((otp[4] << 8) | otp[5]);
            short bp1 = (short)((otp[6] << 8) | otp[7]);
            short b11 = (short)((otp[8] << 8) | otp[9]);
            short bp2 = (short)((otp[10] << 8) | otp[11]);
            short b12 = (short)((otp[12] << 8) | otp[13]);
            short b21 = (short)((otp[14] << 8) | otp[15]);
            short bp3 = (short)((otp[16] << 8) | otp[17]);

            // A0: bytes at 0xB2 (otp[18]) MSB, 0xB3 (otp[19]) mid, and lower nibble of otp[24] (0xB8)
            int a0Combined = (otp[18] << 12) | (otp[19] << 4) | (otp[24] & 0x0F);
            int a0Signed = (int)(a0Combined << 12);
            a0Signed = a0Signed >> 12; // sign-extend 20-bit (C++ style)
            int a0 = a0Signed;

            short a1 = (short)((otp[20] << 8) | otp[21]);
            short a2 = (short)((otp[22] << 8) | otp[23]);

            FactoryCalibrationData = new CalibrationData(a0, a1, a2, b00, bt1, bt2, bp1, b11, bp2, b12, b21, bp3);
        }

        // -------------------- Helper methods --------------------
        private byte ReadRegister(byte reg)
        {
            byte[] buff = new byte[1];
            I2CDevice.WriteRead(new byte[] { reg }, buff);
            return buff[0];
        }

        /// <summary>
        /// Sets the <c>power_mode</c> bits in <c>CTRL_MEAS</c> while preserving other fields.
        /// </summary>
        /// <param name="mode">Power mode to set.</param>
        private void SetPowerMode(PowerMode mode)
        {
            byte ctrl = ReadRegister((byte)ControlRegister.CTRL_MEAS);
            ctrl = (byte)((ctrl & ~Qmp6988Constants.PowerModeMask) | ((byte)mode & Qmp6988Constants.PowerModeMask));
            WriteRegister((byte)ControlRegister.CTRL_MEAS, ctrl);
        }

        /// <summary>
        /// Triggers a forced measurement and reads raw temperature and pressure registers.
        /// </summary>
        /// <param name="rawTemp">Raw temperature ADC value (24-bit).</param>
        /// <param name="rawPress">Raw pressure ADC value (24-bit).</param>
        private void TriggerAndReadRaw(out uint rawTemp, out uint rawPress)
        {
            // Trigger a forced measurement and read both Pressure and Temperature in one 6-byte transaction
            TriggerForcedMeasurement();

            byte[] buf = new byte[6];
            I2CDevice.WriteRead(new byte[] { (byte)Enums.DataRegister.PRESS_MSB }, buf);
            rawPress = ((uint)buf[0] << 16) | ((uint)buf[1] << 8) | (uint)buf[2];
            rawTemp = ((uint)buf[3] << 16) | ((uint)buf[4] << 8) | (uint)buf[5];

            // If we see an impossible measurement (e.g., temperature == 0) retry once to avoid first-sample/stale data issues.
            if (rawTemp == 0 || rawPress == 0)
            {
                // Retry once: retrigger a measurement and read again
                TriggerForcedMeasurement();
                I2CDevice.WriteRead(new byte[] { (byte)Enums.DataRegister.PRESS_MSB }, buf);
                rawPress = ((uint)buf[0] << 16) | ((uint)buf[1] << 8) | (uint)buf[2];
                rawTemp = ((uint)buf[3] << 16) | ((uint)buf[4] << 8) | (uint)buf[5];
            }

            LastMeasuredPressureRaw = rawPress;
            LastMeasuredTemperatureRaw = rawTemp;
        }

        private void TriggerForcedMeasurement()
        {
            // Trigger a forced measurement via helper (sets power_mode = Forced) and wait for it to complete
            SetPowerMode(PowerMode.Forced);
            WaitForMeasurementComplete();
        }

        private void WaitForMeasurementComplete()
        {
            // Compute a timeout based on configured oversampling settings to avoid fixed 1s timeout.
            GetOversampling(out Oversampling t, out Oversampling p);
            Duration est = EstimateMeasurementTime(t, p);
            // Allow some margin and a minimum of 100ms to handle slower cases and startup overhead.
            int timeoutMs = Math.Max((int)Math.Ceiling(est.Milliseconds * TimeoutMarginMultiplier), 100);
            int remaining = timeoutMs;
            while (remaining > 0)
            {
                byte st = ReadRegister((byte)ControlRegister.DEVICE_STAT);
                // measure bit at bit3 - 0 means finished
                if ((st & Qmp6988Constants.DeviceStatMeasureMask) == 0)
                    return;
                System.Threading.Thread.Sleep(1);
                remaining--;
            }
            throw new TimeoutException($"Timeout waiting for measurement completion (>{timeoutMs} ms)");
        }

        private void WriteRegister(byte reg, byte value)
        {
            WriteData(new byte[] { reg, value });
        }
    }
}