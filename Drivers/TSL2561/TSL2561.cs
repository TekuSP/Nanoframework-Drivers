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

        public TSL2561(int I2CBusID, IntegrationTime integrationTime, Gain gain, int deviceAddress = 0x39) : base("TSL2561", I2CBusID, deviceAddress)
        {
            TSL2561IntegrationTime = integrationTime;
            TSL25615Gain = gain;
        }

        public TSL2561(int I2CBusID, I2cConnectionSettings connectionSettings, IntegrationTime integrationTime, Gain gain, int deviceAddress = 0x39) : base("TSL2561", I2CBusID, connectionSettings, deviceAddress)
        {
            TSL2561IntegrationTime = integrationTime;
            TSL25615Gain = gain;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Start()
        {
            base.Start();
            SetIntegrationTime(TSL2561IntegrationTime);
            SetGain(TSL25615Gain);
            Sleep();
        }
        public override long ReadData(byte pointer)
        {
            byte[] result = new byte[1];
            I2CDevice.WriteRead(new byte[] { (byte)((pointer & 0x0F) | Commands.TSL2561_COMMAND_BIT) }, result);
            return result[0];
        }

        public override long ReadData(params byte[] data)
        {
            byte[] result = new byte[1];
            I2CDevice.WriteRead(data, result);
            return result[0];
        }

        public override string ReadDeviceId()
        {
            return ReadData(0x0A).ToString();
        }

        public override string ReadManufacturerId()
        {
            return "Not supported";
        }

        public long ReadResultData(byte pointer)
        {
            byte[] result = new byte[2];
            I2CDevice.WriteRead(new byte[] { (byte)((pointer & 0x0F) | Commands.TSL2561_COMMAND_BIT) }, result);
            return ((uint)result[0]).LowWord().HighWord(result[1]);
        }

        public override string ReadSerialNumber()
        {
            return "Not supported";
        }

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
        public float GetLux()
        {
            Wakeup();
            Thread.Sleep(GetIntegrationTimeMillis(TSL2561IntegrationTime));
            var result = ReadResultData(Commands.TSL2561_WORD_BIT | (byte)Registers.TSL2561_REGISTER_CHAN0_LOW);
            Sleep();
            return result;
        }
        public float GetIR()
        {
            Wakeup();
            Thread.Sleep(GetIntegrationTimeMillis(TSL2561IntegrationTime));
            var result = ReadResultData(Commands.TSL2561_WORD_BIT | (byte)Registers.TSL2561_REGISTER_CHAN1_LOW);
            Sleep();
            return result;
        }

        public void SetIntegrationTime(byte integrationTime)
        {
            SetIntegrationTime((IntegrationTime)integrationTime);
        }

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

        public void Sleep()
        {
            WriteData(new byte[] { (byte)Registers.TSL2561_REGISTER_CONTROL | Commands.TSL2561_CONTROL_POWEROFF });
        }

        public void Wakeup()
        {
            WriteData(new byte[] { (byte)Registers.TSL2561_REGISTER_CONTROL | Commands.TSL2561_CONTROL_POWERON });
        }

        #endregion Public Methods
    }
}