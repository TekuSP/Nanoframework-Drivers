using TekuSP.Drivers.DriverBase;
using TekuSP.Drivers.DriverBase.Enums;
using TekuSP.Drivers.DriverBase.Helpers;
using System.Device.I2c;
using TekuSP.Drivers.DriverBase.Interfaces;
using System;
using System.Threading;

namespace TekuSP.Drivers.TSL2561
{
    /// <summary>
    /// TAOS TSL2561 Light/Lux sensor driver
    /// </summary>
    public class TSL2561 : DriverBaseI2C, ILightSensor, ISensitivity, IPowerSaving, IIRSensor
    {
        private Gain TSL25615Gain;
        private IntegrationTime TSL2561IntegrationTime;
        #region Public Constructors

        /// <summary>
        /// Initializes TSL2561 with default I2C settings.
        /// </summary>
        /// <param name="I2CBusID">I2C bus ID.</param>
        /// <param name="integrationTime">Integration time setting.</param>
        /// <param name="gain">Gain setting.</param>
        /// <param name="deviceAddress">I2C device address.</param>
        public TSL2561(int I2CBusID, IntegrationTime integrationTime, Gain gain, int deviceAddress = 0x39) : base("TSL2561", I2CBusID, deviceAddress)
        {
            TSL2561IntegrationTime = integrationTime;
            TSL25615Gain = gain;
        }

        /// <summary>
        /// Initializes TSL2561 with custom I2C settings.
        /// </summary>
        /// <param name="I2CBusID">I2C bus ID.</param>
        /// <param name="connectionSettings">Custom I2C connection settings.</param>
        /// <param name="integrationTime">Integration time setting.</param>
        /// <param name="gain">Gain setting.</param>
        /// <param name="deviceAddress">I2C device address.</param>
        public TSL2561(int I2CBusID, I2cConnectionSettings connectionSettings, IntegrationTime integrationTime, Gain gain, int deviceAddress = 0x39) : base("TSL2561", I2CBusID, connectionSettings, deviceAddress)
        {
            TSL2561IntegrationTime = integrationTime;
            TSL25615Gain = gain;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc/>
        public override void Start()
        {
            base.Start();
            SetIntegrationTime(TSL2561IntegrationTime);
            SetGain(TSL25615Gain);
            Sleep();
        }
        /// <inheritdoc/>
        public override long ReadData(byte pointer)
        {
            byte[] result = new byte[1];
            I2CDevice.WriteRead(new byte[] { (byte)((pointer & 0x0F) | Commands.TSL2561_COMMAND_BIT) }, result);
            return result[0];
        }

        /// <inheritdoc/>
        public override long ReadData(params byte[] data)
        {
            byte[] result = new byte[1];
            I2CDevice.WriteRead(data, result);
            return result[0];
        }

        /// <inheritdoc/>
        public override string ReadDeviceId()
        {
            return ReadData(0x0A).ToString();
        }

        /// <inheritdoc/>
        public override string ReadManufacturerId()
        {
            return "Not supported";
        }

        /// <summary>
        /// Reads a 16-bit result from a register.
        /// </summary>
        /// <param name="pointer">Register address.</param>
        /// <returns>16-bit value.</returns>
        public long ReadResultData(byte pointer)
        {
            byte[] result = new byte[2];
            I2CDevice.WriteRead(new byte[] { (byte)((pointer & 0x0F) | Commands.TSL2561_COMMAND_BIT) }, result);
            return ((uint)result[0]).LowWord().HighWord(result[1]);
        }

        /// <inheritdoc/>
        public override string ReadSerialNumber()
        {
            return "Not supported";
        }

        /// <inheritdoc/>
        public override void WriteData(params byte[] data)
        {
            data[0] = (byte)((data[0] & 0x0F) | Commands.TSL2561_COMMAND_BIT);
            I2CDevice.Write(data);
        }

        private int GetIntegrationTimeMillis(IntegrationTime time)
        {
            switch (time)
            {
                case IntegrationTime.TSL2561_INTEGRATIONTIME_13MS:
                    return 13;
                case IntegrationTime.TSL2561_INTEGRATIONTIME_101MS:
                    return 101;
                case IntegrationTime.TSL2561_INTEGRATIONTIME_402MS:
                    return 402;
                default:
                    return 0;
            }
        }
        /// <summary>
        /// Gets lux value (raw) from channel 0.
        /// </summary>
        /// <returns>Lux value.</returns>
        public float GetLux()
        {
            Wakeup();
            Thread.Sleep(GetIntegrationTimeMillis(TSL2561IntegrationTime));
            var result = ReadResultData(Commands.TSL2561_WORD_BIT | (byte)Registers.TSL2561_REGISTER_CHAN0_LOW);
            Sleep();
            return result;
        }
        /// <summary>
        /// Gets IR value (raw) from channel 1.
        /// </summary>
        /// <returns>IR value.</returns>
        public float GetIR()
        {
            Wakeup();
            Thread.Sleep(GetIntegrationTimeMillis(TSL2561IntegrationTime));
            var result = ReadResultData(Commands.TSL2561_WORD_BIT | (byte)Registers.TSL2561_REGISTER_CHAN1_LOW);
            Sleep();
            return result;
        }

        /// <summary>
        /// Sets integration time using a raw byte value.
        /// </summary>
        /// <param name="integrationTime">Integration time register value.</param>
        public void SetIntegrationTime(byte integrationTime)
        {
            SetIntegrationTime((IntegrationTime)integrationTime);
        }

        /// <summary>
        /// Sets gain using a raw byte value.
        /// </summary>
        /// <param name="gain">Gain register value.</param>
        public void SetGain(byte gain)
        {
            SetGain((Gain)gain);
        }
        /// <summary>
        /// Sets integration time
        /// </summary>
        /// <param name="integrationTime">Integration time</param>
        public void SetIntegrationTime(IntegrationTime integrationTime)
        {
            Wakeup();
            WriteData(new byte[] { (byte)Registers.TSL2561_REGISTER_TIMING, (byte)((byte)integrationTime | (byte)TSL25615Gain) });
            TSL2561IntegrationTime = integrationTime;
            Sleep();
        }
        /// <summary>
        /// Sets gain
        /// </summary>
        /// <param name="gain">Gain</param>
        public void SetGain(Gain gain)
        {
            Wakeup();
            WriteData(new byte[] { (byte)Registers.TSL2561_REGISTER_TIMING, (byte)((byte)TSL2561IntegrationTime | (byte)gain) });
            TSL25615Gain = gain;
            Sleep();
        }

        /// <summary>
        /// Puts the sensor into power-down mode.
        /// </summary>
        public void Sleep()
        {
            WriteData(new byte[] { (byte)Registers.TSL2561_REGISTER_CONTROL | Commands.TSL2561_CONTROL_POWEROFF });
        }

        /// <summary>
        /// Wakes the sensor from power-down mode.
        /// </summary>
        public void Wakeup()
        {
            WriteData(new byte[] { (byte)Registers.TSL2561_REGISTER_CONTROL | Commands.TSL2561_CONTROL_POWERON });
        }

        #endregion Public Methods
    }
}