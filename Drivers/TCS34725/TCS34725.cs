using TekuSP.Drivers.DriverBase;
using TekuSP.Drivers.DriverBase.Event_Handlers;
using TekuSP.Drivers.DriverBase.Interfaces;

using System;
using System.Device.Gpio;
using System.Threading;

using TekuSP.Drivers.TCS34725.Enums;
using System.Device.I2c;
using UnitsNet;
using UnitsNet.Units;

namespace TekuSP.Drivers.TCS34725
{
    /// <summary>
    /// TAOS TCS34725 Color sensor <a href="https://cdn-shop.adafruit.com/datasheets/TCS34725.pdf"/>
    /// </summary>
    public class TCS34725 : DriverBaseI2C, IColorSensor, ILightSensor, IPowerSaving, ISensitivity, IAdvancedColorSensor
    {
        #region Private Fields

        private GpioPin interruptPin;
        private Gain tcs34725Gain;
        private IntegrationTime tcs34725IntegrationTime;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes TCS34725 with default I2C settings.
        /// </summary>
        /// <param name="I2CBusID">I2C bus ID.</param>
        /// <param name="integrationTime">Integration time setting.</param>
        /// <param name="gain">Gain setting.</param>
        /// <param name="deviceAddress">I2C device address.</param>
        public TCS34725(int I2CBusID, IntegrationTime integrationTime, Gain gain, int deviceAddress = 0x29) : base("TCS34725", I2CBusID, deviceAddress)
        {
            tcs34725IntegrationTime = integrationTime;
            tcs34725Gain = gain;
        }

        /// <summary>
        /// Initializes TCS34725 with custom I2C settings.
        /// </summary>
        /// <param name="name">Device name.</param>
        /// <param name="I2CBusID">I2C bus ID.</param>
        /// <param name="connectionSettings">Custom I2C connection settings.</param>
        /// <param name="integrationTime">Integration time setting.</param>
        /// <param name="gain">Gain setting.</param>
        /// <param name="deviceAddress">I2C device address.</param>
        public TCS34725(string name, int I2CBusID, I2cConnectionSettings connectionSettings, IntegrationTime integrationTime, Gain gain, int deviceAddress) : base(name, I2CBusID, connectionSettings, deviceAddress)
        {
            tcs34725IntegrationTime = integrationTime;
            tcs34725Gain = gain;
        }

        #endregion Public Constructors

        #region Public Events

        /// <inheritdoc/>
        public event EventHandlers.IColorDataEventHandler OnLimitRawReached;

        /// <inheritdoc/>
        public event EventHandlers.IColorDataEventHandler OnLimitReached;

        #endregion Public Events

        #region Public Methods

        /// <inheritdoc/>
        public int GetColorTemperature()
        {
            var data = GetRawData();
            if (data.C == 0)
                return 0;
            float r2, b2, sat, ir;
            var intTime = GetIntegrationTimeMillis(tcs34725IntegrationTime);
            if (256 - intTime > 63)
            {
                sat = ushort.MaxValue;  /* Track digital saturation */
            }
            else
            {
                sat = 1024 * (256 - intTime); /* Track analog saturation */
            }
            if ((256 - intTime) <= 63)
            {
                /* Adjust sat to 75% to avoid analog saturation if atime < 153.6ms */
                sat -= sat / 4;
            }

            if (data.C >= sat)
            {
                /* Check for saturation and mark the sample as invalid if true */
                return 0;
            }
            ir = (data.R + data.G + data.B > data.C) ? (data.R + data.G + data.B - data.C) / 2 : 0; //Calculate IR channel

            r2 = data.R - ir;  /* Remove the IR component from the raw RGB values */
            b2 = data.B - ir;

            if (r2 == 0)
                return 0;
            int cct = (3810 * (int)b2) / /* Color temp coefficient. */ (int)r2 + 1391; /* Color temp offset. */
            return cct;
        }

        /// <inheritdoc/>
        public Illuminance GetLux()
        {
            var data = GetRawData();
            double lux = (-0.32466F * data.R) + (1.57837F * data.G) + (-0.73191F * data.B);
            return Illuminance.FromLux(lux);
        }

        /// <inheritdoc/>
        public IColorData GetRawData()
        {
            CRGBData data;
            Wakeup();
            data = GetRawDataInternal();
            Sleep();
            return data;
        }

        /// <inheritdoc/>
        public IColorData GetRGB()
        {
            CRGBData data = (CRGBData)GetRawData();
            if (data.C == 0)
            {
                return new CRGBData();
            }
            data.R = data.R / data.C * 255;
            data.G = data.G / data.C * 255;
            data.B = data.B / data.C * 255;
            return data;
        }

        /// <inheritdoc/>
        public override long ReadData(byte pointer)
        {
            return ReadData(new byte[] { pointer });
        }

        /// <inheritdoc/>
        public override long ReadData(byte[] data)
        {
            return I2CDevice.Read(data).BytesTransferred;
        }

        /// <inheritdoc/>
        public override string ReadDeviceId()
        {
            Wakeup();
            var id = ReadRegister((byte)GainControl.TCS34725_ID).ToString();
            Sleep();
            return id;
        }

        /// <inheritdoc/>
        public override string ReadManufacturerId()
        {
            return "TAOS";
        }

        /// <summary>
        /// Reads 8 bit register
        /// </summary>
        /// <param name="reg">Register to read</param>
        /// <returns>Read value</returns>
        public byte ReadRegister(byte reg)
        {
            byte[] buffer = new byte[1] { (byte)(reg | (byte)Commands.TCS34725_CMD_BIT) };
            byte[] returnBuffer = new byte[1];
            I2CDevice.WriteRead(buffer, returnBuffer);
            return returnBuffer[0];
        }

        /// <inheritdoc/>
        public override string ReadSerialNumber()
        {
            return "Not supported";
        }

        /// <summary>
        /// Reads 16 bit register
        /// </summary>
        /// <param name="reg">Register to read</param>
        /// <returns>Read value</returns>
        public ushort ReadShortRegister(byte reg)
        {
            byte[] buffer = new byte[1] { (byte)(reg | (byte)Commands.TCS34725_CMD_BIT) };
            byte[] returnBuffer = new byte[2];
            I2CDevice.WriteRead(buffer, returnBuffer);
            return (ushort)((returnBuffer[1] << 8) | (returnBuffer[0] & 0xFF));
        }

        /// <summary>
        /// Set gain
        /// </summary>
        /// <param name="gain">Gain</param>
        public void SetGain(Gain gain)
        {
            WriteRegister((byte)GainControl.TCS34725_CONTROL, (byte)gain);
            tcs34725Gain = gain;
        }

        /// <inheritdoc/>
        public void SetGain(byte gain)
        {
            SetGain((Gain)gain);
        }

        /// <summary>
        /// Sets integration time by mapping to the closest <see cref="IntegrationTime"/> value.
        /// </summary>
        /// <param name="integrationTime">Integration time.</param>
        /// <remarks>Supported discrete values are defined by <see cref="IntegrationTime"/>.</remarks>
        public void SetIntegrationTime(Duration integrationTime)
        {
            SetIntegrationTime(MapIntegrationTime(integrationTime));
        }

        /// <summary>
        /// Sets integration time
        /// </summary>
        /// <param name="integrationTime">Integration time</param>
        public void SetIntegrationTime(IntegrationTime integrationTime)
        {
            WriteRegister((byte)Time.TCS34725_ATIME, (byte)integrationTime);
            tcs34725IntegrationTime = integrationTime;
        }

        /// <inheritdoc/>
        public void SetIntegrationTime(byte integrationTime)
        {
            SetIntegrationTime((IntegrationTime)integrationTime);
        }

        /// <inheritdoc/>
        public void SetInterrupt(bool enable, int interruptPin)
        {
            byte r = ReadRegister((byte)Enable.TCS34725_ENABLE);
            if (enable)
            {
                if (this.interruptPin != null)
                    return; //Already enabled
                this.interruptPin = new GpioController().OpenPin(interruptPin, PinMode.Input);
                this.interruptPin.ValueChanged += InterruptPin_ValueChanged;
                r |= (byte)Enable.TCS34725_ENABLE_AIEN;
            }
            else
            {
                if (this.interruptPin == null)
                    return; //Already disabled
                this.interruptPin.ValueChanged -= InterruptPin_ValueChanged;
                this.interruptPin.Dispose();
                this.interruptPin = null;
                r = (byte)(r & ~(int)Enable.TCS34725_ENABLE_AIEN);
            }
            WriteRegister((byte)Enable.TCS34725_ENABLE, r);
        }

        /// <inheritdoc/>
        public void SetInterruptLimits(int low, int high)
        {
            WriteRegister(0x04, (byte)(low & 0xFF));
            WriteRegister(0x05, (byte)(low >> 8));
            WriteRegister(0x06, (byte)(high & 0xFF));
            WriteRegister(0x07, (byte)(high >> 8));
        }

        private static IntegrationTime MapIntegrationTime(Duration integrationTime)
        {
            double ms = integrationTime.Milliseconds;
            double[] options = { 2.4, 24, 50, 101, 154, 700 };
            IntegrationTime[] mappings =
            {
                IntegrationTime.TCS34725_INTEGRATIONTIME_2_4MS,
                IntegrationTime.TCS34725_INTEGRATIONTIME_24MS,
                IntegrationTime.TCS34725_INTEGRATIONTIME_50MS,
                IntegrationTime.TCS34725_INTEGRATIONTIME_101MS,
                IntegrationTime.TCS34725_INTEGRATIONTIME_154MS,
                IntegrationTime.TCS34725_INTEGRATIONTIME_700MS
            };

            double bestDiff = double.MaxValue;
            int bestIndex = 0;
            for (int i = 0; i < options.Length; i++)
            {
                double diff = Math.Abs(ms - options[i]);
                if (diff < bestDiff)
                {
                    bestDiff = diff;
                    bestIndex = i;
                }
            }

            return mappings[bestIndex];
        }

        /// <inheritdoc/>
        public void Sleep()
        {
            byte register = ReadRegister((byte)Enable.TCS34725_ENABLE);
            WriteRegister((byte)Enable.TCS34725_ENABLE, (byte)(register & ~((byte)Enable.TCS34725_ENABLE_PON | (byte)Enable.TCS34725_ENABLE_AEN)));
        }

        /// <inheritdoc/>
        public override void Start()
        {
            base.Start();
            SetIntegrationTime(tcs34725IntegrationTime);
            SetGain(tcs34725Gain);
        }

        /// <inheritdoc/>
        public void Wakeup()
        {
            WriteRegister((byte)Enable.TCS34725_ENABLE, (byte)Enable.TCS34725_ENABLE_PON);
            Thread.Sleep(3);
            WriteRegister((byte)Enable.TCS34725_ENABLE, ((byte)Enable.TCS34725_ENABLE_PON | (byte)Enable.TCS34725_ENABLE_AEN));
            Thread.Sleep(((256 - GetIntegrationTimeMillis(tcs34725IntegrationTime)) * 12 / 5 + 1));
        }

        /// <inheritdoc/>
        public override void WriteData(byte[] data)
        {
            I2CDevice.Write(data);
        }

        /// <summary>
        /// Writes register
        /// </summary>
        /// <param name="reg">Register to write</param>
        /// <param name="value">Value to write</param>
        public void WriteRegister(byte reg, byte value)
        {
            byte[] buffer = new byte[2] { (byte)(reg | (byte)Commands.TCS34725_CMD_BIT), value };
            I2CDevice.Write(buffer);
        }

        #endregion Public Methods

        #region Private Methods

        private int GetIntegrationTimeMillis(IntegrationTime time)
        {
            switch (time)
            {
                case IntegrationTime.TCS34725_INTEGRATIONTIME_2_4MS:
                    return 3;

                case IntegrationTime.TCS34725_INTEGRATIONTIME_24MS:
                    return 24;

                case IntegrationTime.TCS34725_INTEGRATIONTIME_50MS:
                    return 50;

                case IntegrationTime.TCS34725_INTEGRATIONTIME_101MS:
                    return 101;

                case IntegrationTime.TCS34725_INTEGRATIONTIME_154MS:
                    return 154;

                case IntegrationTime.TCS34725_INTEGRATIONTIME_700MS:
                    return 700;

                default:
                    return 0;
            }
        }

        /// <summary>
        /// Gets Raw Data from sensor
        /// </summary>
        /// <returns>Returns raw data</returns>
        private CRGBData GetRawDataInternal()
        {
            CRGBData cRGBData = new CRGBData();
            cRGBData.C = ReadShortRegister((byte)Data.TCS34725_CDATAL);
            cRGBData.R = ReadShortRegister((byte)Data.TCS34725_RDATAL);
            cRGBData.G = ReadShortRegister((byte)Data.TCS34725_GDATAL);
            cRGBData.B = ReadShortRegister((byte)Data.TCS34725_BDATAL);
            Thread.Sleep(((256 - GetIntegrationTimeMillis(tcs34725IntegrationTime)) * 12 / 5 + 1));
            return cRGBData;
        }

        private void InterruptPin_ValueChanged(object sender, PinValueChangedEventArgs e)
        {
            OnLimitReached(this, GetRGB());
            OnLimitRawReached(this, GetRawData());
            byte[] buffer = new byte[1] { ((byte)Commands.TCS34725_CMD_BIT) | 0x66 };
            I2CDevice.Write(buffer); //Clear interrupt
        }

        #endregion Private Methods
    }
}