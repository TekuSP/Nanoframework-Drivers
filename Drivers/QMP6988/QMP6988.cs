using System;
using System.Device.I2c;

using TekuSP.Drivers.DriverBase;
using TekuSP.Drivers.DriverBase.Interfaces;
using TekuSP.Drivers.QMP6988.Helpers;

using UnitsNet;
using UnitsNet.Units;

namespace TekuSP.Drivers.QMP6988
{
    public class QMP6988 : DriverBaseI2C, ITemperatureSensor, IPressureSensor, IPeriodicMeasurement
    {
        public QMP6988(int I2CBusID, int deviceAddress = 0x70) : base("QMP9688", I2CBusID, deviceAddress)
        {
        }

        public QMP6988(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress = 0x70) : base("QMP9688", I2CBusID, connectionSettings, deviceAddress)
        {
        }

        public override void Start()
        {
            base.Start();

        }

        public override void Stop()
        {
            base.Stop();
            FactoryCalibrationData = null;
        }

        private uint LastMeasuredTemperatureRaw { get; set; }
        private uint LastMeasuredPressureRaw { get; set; }

        public bool IsPeriodicMeasurementRunning => throw new NotImplementedException();

        public byte PeriodicMeasurementRate => throw new NotImplementedException();

        public CalibrationData FactoryCalibrationData
        {
            get;
            private set;
        }

        Pressure IPressureSensor.CalculatePressure(PressureUnit type, double rawPressure) => CalculatePressure(type, (uint)rawPressure);
        Temperature ITemperatureSensor.CalculateTemperature(TemperatureUnit type, double rawTemperature) => CalculateTemperature(type, (uint)rawTemperature);

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
        public override long ReadData(byte pointer) => throw new NotImplementedException();
        public override long ReadData(byte[] data) => throw new NotImplementedException();
        public override string ReadDeviceId() => throw new NotImplementedException();
        public override string ReadManufacturerId() => throw new NotImplementedException();
        public double ReadPressure() => throw new NotImplementedException();
        public Pressure ReadPressure(PressureUnit type) => throw new NotImplementedException();
        public override string ReadSerialNumber() => throw new NotImplementedException();
        public double ReadTemperature() => throw new NotImplementedException();
        public Temperature ReadTemperature(TemperatureUnit readTemperatureUnit) => throw new NotImplementedException();
        public void StartPeriodicMeasurement(byte measurementRate) => throw new NotImplementedException();
        public void StopPeriodicMeasurement() => throw new NotImplementedException();
        public override void WriteData(byte[] data) => throw new NotImplementedException();
    }
}
