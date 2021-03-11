using System;
using System.Device.I2c;
using System.Threading;

using DriverBase;
using DriverBase.Enums;
using DriverBase.Interfaces;

using SHTC3.Enums;
namespace SHTC3
{
    public class SHTC3 : DriverBaseI2C, ISleepSupport, ITemperatureSensor, IHumiditySensor
    {
        public SHTC3(int I2CBusID, int deviceAddress = 0x70) : base("SHTC3", I2CBusID, deviceAddress)
        {
        }

        public SHTC3(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress = 0x70) : base("SHTC3", I2CBusID, connectionSettings, deviceAddress)
        {
        }

        public bool IsSleeping { get; private set; }

        public float CalculateHumidity(HumidityType readHumidityType, float rawHumidity)
        {
            switch (readHumidityType)
            {
                case HumidityType.Relative:
                    return 100f * ((float)rawHumidity / 65535f);
                default:
                    throw new ArgumentException("Only Relative humidity is supported in this sensor!");
            }
        }

        public float CalculateTemperature(TemperatureUnit readTemperatureUnit, float rawTemperature)
        {
            switch (readTemperatureUnit)
            {
                case TemperatureUnit.Celsius:
                    return -45f + (175f * ((float)rawTemperature / 65535f));
                case TemperatureUnit.Fahrenheit:
                    return (-45f + (175f * ((float)rawTemperature / 65535f))) * (9.0f / 5f) + 32.0f;
                default:
                    throw new ArgumentException("Only Celsius and Fahrenheit is supported in this sensor!");
            }
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

        public float ReadHumidity()
        {
            WakeUp();
            if (MeasurementMode == MeasurementModes.SHTC3_CMD_CSD_TF_LPM)
                SetMeasurmentMode(MeasurementModes.SHTC3_CMD_CSD_RHF_LPM);
            else if (MeasurementMode == MeasurementModes.SHTC3_CMD_CSD_TF_NPM)
                SetMeasurmentMode(MeasurementModes.SHTC3_CMD_CSD_RHF_NPM);
            else
                SetMeasurmentMode(MeasurementMode);
            byte RHhb;
            byte RHlb;
            byte RHcs;
            switch (MeasurementMode) // Switch for the order of the returned results
            {
                case MeasurementModes.SHTC3_CMD_CSE_RHF_NPM:
                case MeasurementModes.SHTC3_CMD_CSE_RHF_LPM:
                    // RH First
                    RHhb = I2CDevice.ReadByte();
                    RHlb = I2CDevice.ReadByte();
                    RHcs = I2CDevice.ReadByte();

                    I2CDevice.ReadByte();
                    I2CDevice.ReadByte();
                    I2CDevice.ReadByte();
                    break;

                case MeasurementModes.SHTC3_CMD_CSE_TF_NPM:
                case MeasurementModes.SHTC3_CMD_CSE_TF_LPM:
                    // T First
                    I2CDevice.ReadByte();
                    I2CDevice.ReadByte();
                    I2CDevice.ReadByte();

                    RHhb = I2CDevice.ReadByte();
                    RHlb = I2CDevice.ReadByte();
                    RHcs = I2CDevice.ReadByte();
                    break;
                case MeasurementModes.SHTC3_CMD_CSD_RHF_LPM:
                case MeasurementModes.SHTC3_CMD_CSD_RHF_NPM:
                    Thread.Sleep(20);
                    RHhb = I2CDevice.ReadByte();
                    RHlb = I2CDevice.ReadByte();
                    RHcs = I2CDevice.ReadByte();
                    break;
                default:
                    return -1;
            }
            ushort RH = (ushort)((RHhb << 8) | RHlb);
            if (!CheckCRC(RH, RHcs))
                return -1; //BAD CRC
            Thread.Sleep(100);
            return RH;
        }

        public float ReadHumidity(HumidityType readHumidityType)
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

        public float ReadTemperature() //TODO: This requires some major refactoring, wtf is going on
        {
            WakeUp();
            if (MeasurementMode == MeasurementModes.SHTC3_CMD_CSD_RHF_LPM)
                SetMeasurmentMode(MeasurementModes.SHTC3_CMD_CSD_TF_LPM);
            else if (MeasurementMode == MeasurementModes.SHTC3_CMD_CSD_RHF_NPM)
                SetMeasurmentMode(MeasurementModes.SHTC3_CMD_CSD_TF_NPM);
            else
                SetMeasurmentMode(MeasurementMode);
            byte Thb;
            byte Tlb;
            byte Tcs;
            switch (MeasurementMode) // Switch for the order of the returned results
            {
                case MeasurementModes.SHTC3_CMD_CSE_RHF_NPM:
                case MeasurementModes.SHTC3_CMD_CSE_RHF_LPM:
                    // RH First
                    I2CDevice.ReadByte();
                    I2CDevice.ReadByte();
                    I2CDevice.ReadByte();

                    Thb = I2CDevice.ReadByte();
                    Tlb = I2CDevice.ReadByte();
                    Tcs = I2CDevice.ReadByte();
                    break;

                case MeasurementModes.SHTC3_CMD_CSE_TF_NPM:
                case MeasurementModes.SHTC3_CMD_CSE_TF_LPM:
                    // T First
                    Thb = I2CDevice.ReadByte();
                    Tlb = I2CDevice.ReadByte();
                    Tcs = I2CDevice.ReadByte();

                    I2CDevice.ReadByte();
                    I2CDevice.ReadByte();
                    I2CDevice.ReadByte();
                    break;
                case MeasurementModes.SHTC3_CMD_CSD_TF_LPM:
                case MeasurementModes.SHTC3_CMD_CSD_TF_NPM:
                    Thread.Sleep(20);
                    Thb = I2CDevice.ReadByte();
                    Tlb = I2CDevice.ReadByte();
                    Tcs = I2CDevice.ReadByte();
                    break;
                default:
                    return -1;
            }
            ushort T = (ushort)((Thb << 8) | Tlb);
            if (!CheckCRC(T, Tcs))
                return -1; //BAD CRC
            Thread.Sleep(100);
            return T;
        }
        public float ReadTemperature(TemperatureUnit readTemperatureUnit)
        {
            return CalculateTemperature(readTemperatureUnit, ReadTemperature());
        }

        public void Sleep()
        {
            if (IsSleeping) //We are already asleep
                return;
            if (WriteCommand(Commands.SHTC3_CMD_SLEEP) == Status.SHTC3_Status_Nominal)
                IsSleeping = true;
        }

        public void WakeUp()
        {
            if (!IsSleeping) //We are already awake
                return;
            if (WriteCommand(Commands.SHTC3_CMD_WAKE) == Status.SHTC3_Status_Nominal)
                IsSleeping = false;
        }

        public override void WriteData(params byte[] data)
        {
            I2CDevice.Write(data);
        }
        public override void Start()
        {
            base.Start();
            WakeUp();
            ReadDeviceId();
            SetMeasurmentMode(MeasurementModes.SHTC3_CMD_CSE_RHF_NPM);
            IsSleeping = true; // Assume the sensor is asleep to begin (there won't be any harm in waking it up if it is already awake)
        }
        public override void Stop()
        {
            WakeUp();
            WriteCommand(Commands.SHTC3_CMD_SFT_RST);
            base.Stop();
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
        public Status WriteCommand(Commands command)
        {
            var result = I2CDevice.Write(new SpanByte(new byte[] { (byte)(((ushort)command) >> 8), (byte)(((ushort)command) & 0x00FF) }));
            if (result.Status == I2cTransferStatus.FullTransfer)
                return Status.SHTC3_Status_Nominal;
            return Status.SHTC3_Status_Error;
        }
        public MeasurementModes MeasurementMode { get; private set; }
        public Status SetMeasurmentMode(MeasurementModes measurementMode)
        {
            var result = I2CDevice.Write(new SpanByte(new byte[] { (byte)(((ushort)measurementMode) >> 8), (byte)(((ushort)measurementMode) & 0x00FF) }));
            if (result.Status == I2cTransferStatus.FullTransfer)
            {
                MeasurementMode = measurementMode;
                return Status.SHTC3_Status_Nominal;
            }
            return Status.SHTC3_Status_Error;
        }
    }
}
