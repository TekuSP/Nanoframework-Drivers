using System;
using System.Device.I2c;
using System.Threading;

using TekuSP.Drivers.DriverBase;

namespace TekuSP.Drivers.ICM20948
{
    public class ICM20948 : DriverBaseI2C
    {
        #region Private Fields

        private const byte I2C_ADD_ICM20948_AK09916 = 0x0C;
        private const byte I2C_ADD_ICM20948_AK09916_READ = 0x80;
        private const byte I2C_ADD_ICM20948_AK09916_WRITE = 0x00;

        #endregion Private Fields

        #region Public Constructors

        public ICM20948(int I2CBusID, int deviceAddress = 0x68) : base("ICM20948", I2CBusID, deviceAddress)
        {
        }

        public ICM20948(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress = 0x68) : base("ICM20948", I2CBusID, connectionSettings, deviceAddress)
        {
        }

        #endregion Public Constructors

        #region Private Enums

        private enum ICM20948_BANK0
        {
            REG_ADD_WIA = 0x00,
            REG_VAL_WIA = 0xEA,
            REG_ADD_USER_CTRL = 0x03,
            REG_VAL_BIT_DMP_EN = 0x80,
            REG_VAL_BIT_FIFO_EN = 0x40,
            REG_VAL_BIT_I2C_MST_EN = 0x20,
            REG_VAL_BIT_I2C_IF_DIS = 0x10,
            REG_VAL_BIT_DMP_RST = 0x08,
            REG_VAL_BIT_DIAMOND_DMP_RST = 0x04,
            REG_ADD_PWR_MIGMT_1 = 0x06,
            REG_VAL_ALL_RGE_RESET = 0x80,
            REG_VAL_RUN_MODE = 0x01, //Nonlow-powermode
            REG_ADD_LP_CONFIG = 0x05,
            REG_ADD_PWR_MGMT_1 = 0x06,
            REG_ADD_PWR_MGMT_2 = 0x07,
            REG_ADD_ACCEL_XOUT_H = 0x2D,
            REG_ADD_ACCEL_XOUT_L = 0x2E,
            REG_ADD_ACCEL_YOUT_H = 0x2F,
            REG_ADD_ACCEL_YOUT_L = 0x30,
            REG_ADD_ACCEL_ZOUT_H = 0x31,
            REG_ADD_ACCEL_ZOUT_L = 0x32,
            REG_ADD_GYRO_XOUT_H = 0x33,
            REG_ADD_GYRO_XOUT_L = 0x34,
            REG_ADD_GYRO_YOUT_H = 0x35,
            REG_ADD_GYRO_YOUT_L = 0x36,
            REG_ADD_GYRO_ZOUT_H = 0x37,
            REG_ADD_GYRO_ZOUT_L = 0x38,
            REG_ADD_EXT_SENS_DATA_00 = 0x3B,
            REG_ADD_REG_BANK_SEL = 0x7F,
            REG_VAL_REG_BANK_0 = 0x00,
            REG_VAL_REG_BANK_1 = 0x10,
            REG_VAL_REG_BANK_2 = 0x20,
            REG_VAL_REG_BANK_3 = 0x30
        }

        private enum ICM20948_BANK2
        {
            REG_ADD_GYRO_SMPLRT_DIV = 0x00,
            REG_ADD_GYRO_CONFIG_1 = 0x01,
            REG_VAL_BIT_GYRO_DLPCFG_2 = 0x10,//bit[5:3]
            REG_VAL_BIT_GYRO_DLPCFG_4 = 0x20,//bit[5:3]
            REG_VAL_BIT_GYRO_DLPCFG_6 = 0x30,//bit[5:3]
            REG_VAL_BIT_GYRO_FS_250DPS = 0x00,//bit[2:1]
            REG_VAL_BIT_GYRO_FS_500DPS = 0x02,//bit[2:1]
            REG_VAL_BIT_GYRO_FS_1000DPS = 0x04,//bit[2:1]
            REG_VAL_BIT_GYRO_FS_2000DPS = 0x06,//bit[2:1]
            REG_VAL_BIT_GYRO_DLPF = 0x01,//bit[0]
            REG_ADD_ACCEL_SMPLRT_DIV_2 = 0x11,
            REG_ADD_ACCEL_CONFIG = 0x14,
            REG_VAL_BIT_ACCEL_DLPCFG_2 = 0x10,//bit[5:3]
            REG_VAL_BIT_ACCEL_DLPCFG_4 = 0x20,//bit[5:3]
            REG_VAL_BIT_ACCEL_DLPCFG_6 = 0x30,//bit[5:3]
            REG_VAL_BIT_ACCEL_FS_2g = 0x00,//bit[2:1]
            REG_VAL_BIT_ACCEL_FS_4g = 0x02,//bit[2:1]
            REG_VAL_BIT_ACCEL_FS_8g = 0x04,//bit[2:1]
            REG_VAL_BIT_ACCEL_FS_16g = 0x06,//bit[2:1]
            REG_VAL_BIT_ACCEL_DLPF = 0x01//bit[0]
        }

        private enum ICM20948_BANK3
        {
            REG_ADD_I2C_SLV0_ADDR = 0x03,
            REG_ADD_I2C_SLV0_REG = 0x04,
            REG_ADD_I2C_SLV0_CTRL = 0x05,
            REG_VAL_BIT_SLV0_EN = 0x80,
            REG_VAL_BIT_MASK_LEN = 0x07,
            REG_ADD_I2C_SLV0_DO = 0x06,
            REG_ADD_I2C_SLV1_ADDR = 0x07,
            REG_ADD_I2C_SLV1_REG = 0x08,
            REG_ADD_I2C_SLV1_CTRL = 0x09,
            REG_ADD_I2C_SLV1_DO = 0x0A
        }

        private enum ICM20948_MAG
        {
            REG_ADD_MAG_WIA1 = 0x00,
            REG_VAL_MAG_WIA1 = 0x48,
            REG_ADD_MAG_WIA2 = 0x01,
            REG_VAL_MAG_WIA2 = 0x09,
            REG_ADD_MAG_ST2 = 0x10,
            REG_ADD_MAG_DATA = 0x11,
            REG_ADD_MAG_CNTL2 = 0x31,
            REG_VAL_MAG_MODE_PD = 0x00,
            REG_VAL_MAG_MODE_SM = 0x01,
            REG_VAL_MAG_MODE_10HZ = 0x02,
            REG_VAL_MAG_MODE_20HZ = 0x04,
            REG_VAL_MAG_MODE_50HZ = 0x05,
            REG_VAL_MAG_MODE_100HZ = 0x08,
            REG_VAL_MAG_MODE_ST = 0x10
        }

        #endregion Private Enums

        #region Public Properties

        /// <summary>
        /// Gyroscope calibration offset, calculated by <see cref="ReGenerateGyroscopeOffset"/>
        /// </summary>
        public int[] GyroscopeCalibrationOffset { get; set; }

        public double Pitch { get; private set; }

        public double Roll { get; private set; }

        public double Yaw { get; private set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Gyroscope with acceleration reading from device
        /// </summary>
        /// <param name="acceleration">Returns accelerations - Array of three</param>
        /// <param name="gyroscope">Returns gyroscopes - Array of three</param>
        /// <param name="gyroscopeOffset">Requires gyroscope offset calibration - Array of three, <see cref="GyroscopeCalibrationOffset"/> and <see cref="ReGenerateGyroscopeOffset"/></param>
        //TODO: Should this be interface? This is crazy method right now
        public void GyroscopeAccelerationRead(out int[] acceleration, out int[] gyroscope, int[] gyroscopeOffset = null)
        {
            if (gyroscopeOffset == null)
                gyroscopeOffset = GyroscopeCalibrationOffset;
            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_0);
            var data = ReadWritePrimary((byte)ICM20948_BANK0.REG_ADD_ACCEL_XOUT_H, 12);
            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_2);

            acceleration = new int[3];
            gyroscope = new int[3];

            acceleration[0] = (data[0] << 8) | data[1];
            acceleration[1] = (data[2] << 8) | data[3];
            acceleration[2] = (data[4] << 8) | data[5];
            gyroscope[0] = ((data[6] << 8) | data[7]) - gyroscopeOffset[0];
            gyroscope[1] = ((data[8] << 8) | data[9]) - gyroscopeOffset[1];
            gyroscope[2] = ((data[10] << 8) | data[11]) - gyroscopeOffset[2];

            if (acceleration[0] >= 32767)
                acceleration[0] = acceleration[0] - 65535;
            else if (acceleration[0] <= -32767)
                acceleration[0] = acceleration[0] + 65535;
            if (acceleration[1] >= 32767)
                acceleration[1] = acceleration[1] - 65535;
            else if (acceleration[1] <= -32767)
                acceleration[1] = acceleration[1] + 65535;
            if (acceleration[2] >= 32767)
                acceleration[2] = acceleration[2] - 65535;
            else if (acceleration[2] <= -32767)
                acceleration[2] = acceleration[2] + 65535;
            if (gyroscope[0] >= 32767)
                gyroscope[0] = gyroscope[0] - 65535;
            else if (gyroscope[0] <= -32767)
                gyroscope[0] = gyroscope[0] + 65535;
            if (gyroscope[1] >= 32767)
                gyroscope[1] = gyroscope[1] - 65535;
            else if (gyroscope[1] <= -32767)
                gyroscope[1] = gyroscope[1] + 65535;
            if (gyroscope[2] >= 32767)
                gyroscope[2] = gyroscope[2] - 65535;
            else if (gyroscope[2] <= -32767)
                gyroscope[2] = gyroscope[2] + 65535;
        }

        /// <summary>
        /// Magnetic field rading from device
        /// </summary>
        /// <param name="magneticField">Returns magnetic field - Array of three</param>
        public void MagneticFieldRead(out int[] magneticField)
        {
            byte counter = 20;
            int[] tempX = new int[8];
            int[] tempY = new int[8];
            int[] tempZ = new int[8];
            magneticField = new int[3];
            while (counter > 0)
            {
                Thread.Sleep(10);
                byte result = ReadSecondary(I2C_ADD_ICM20948_AK09916 | I2C_ADD_ICM20948_AK09916_READ, (byte)ICM20948_MAG.REG_ADD_MAG_ST2, 1)[0];
                if ((result & 0x01) != 0)
                    break;
                counter -= 1;
            }
            if (counter != 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    var readData = ReadSecondary(I2C_ADD_ICM20948_AK09916 | I2C_ADD_ICM20948_AK09916_READ, (byte)ICM20948_MAG.REG_ADD_MAG_DATA, 6);
                    tempX[i] = (readData[1] << 8) | readData[0];
                    tempY[i] = (readData[3] << 8) | readData[2];
                    tempZ[i] = (readData[5] << 8) | readData[4];
                }
                magneticField[0] = (tempX[0] + tempX[1] + tempX[2] + tempX[3] + tempX[4] + tempX[5] + tempX[6] + tempX[7]) / 8;
                magneticField[1] = -(tempY[0] + tempY[1] + tempY[2] + tempY[3] + tempY[4] + tempY[5] + tempY[6] + tempY[7]) / 8;
                magneticField[2] = -(tempZ[0] + tempZ[1] + tempZ[2] + tempZ[3] + tempZ[4] + tempZ[5] + tempZ[6] + tempZ[7]) / 8;
            }
            if (magneticField[0] >= 32767)
                magneticField[0] = magneticField[0] - 65535;
            else if (magneticField[0] <= -32767)
                magneticField[0] = magneticField[0] + 65535;
            if (magneticField[1] >= 32767)
                magneticField[1] = magneticField[1] - 65535;
            else if (magneticField[1] <= -32767)
                magneticField[1] = magneticField[1] + 65535;
            if (magneticField[2] >= 32767)
                magneticField[2] = magneticField[2] - 65535;
            else if (magneticField[2] <= -32767)
                magneticField[2] = magneticField[2] + 65535;
        }

        public override long ReadData(byte pointer)
        {
            WriteData(new byte[] { pointer });
            return -1;
        }

        /// <summary>
        /// Not supported, calls <see cref="ReadPrimary(byte[])"/>
        /// </summary>
        /// <param name="data">Data which should be returned</param>
        /// <returns>Number of read bytes</returns>
        public override long ReadData(byte[] data)
        {
            data[0] = ReadPrimary();
            return data.Length;
        }

        public override string ReadDeviceId()
        {
            return "Not supported";
        }

        public override string ReadManufacturerId()
        {
            return "TDK InvenSense";
        }

        public byte ReadPrimary()
        {
            return I2CDevice.ReadByte();
        }

        public byte[] ReadPrimary(byte length)
        {
            SpanByte buffer = new SpanByte(new byte[length]);
            I2CDevice.Read(buffer);
            return buffer.ToArray();
        }

        public byte[] ReadSecondary(byte I2CAddr, byte registerAddr, byte length)
        {
            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_3);
            WritePrimary((byte)ICM20948_BANK3.REG_ADD_I2C_SLV0_ADDR, I2CAddr);
            WritePrimary((byte)ICM20948_BANK3.REG_ADD_I2C_SLV0_REG, registerAddr);
            WritePrimary((byte)ICM20948_BANK3.REG_ADD_I2C_SLV0_CTRL, (byte)(((byte)ICM20948_BANK3.REG_VAL_BIT_SLV0_EN) | length));

            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_0);
            var temp = ReadWritePrimary((byte)ICM20948_BANK0.REG_ADD_USER_CTRL);
            temp = (byte)(temp | (byte)ICM20948_BANK0.REG_VAL_BIT_I2C_MST_EN);
            WritePrimary((byte)ICM20948_BANK0.REG_ADD_USER_CTRL, temp);
            Thread.Sleep(10);
            temp = (byte)(temp & (~(byte)ICM20948_BANK0.REG_VAL_BIT_I2C_MST_EN));
            WritePrimary((byte)ICM20948_BANK0.REG_ADD_USER_CTRL, temp);

            byte[] returnValue = new byte[length];
            for (byte i = 0; i < length; i++)
                returnValue[i] = ReadWritePrimary((byte)(((byte)ICM20948_BANK0.REG_ADD_EXT_SENS_DATA_00) + i));

            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_3);
            temp = ReadWritePrimary((byte)ICM20948_BANK3.REG_ADD_I2C_SLV0_CTRL);
            temp = (byte)(temp & ~(((byte)ICM20948_BANK0.REG_VAL_BIT_I2C_MST_EN) & ((byte)ICM20948_BANK3.REG_VAL_BIT_MASK_LEN)));
            WritePrimary((byte)ICM20948_BANK3.REG_ADD_I2C_SLV0_CTRL, temp);

            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_0);
            return returnValue;
        }

        public override string ReadSerialNumber()
        {
            return "Not supported";
        }

        /// <summary>
        /// Calibrates new Gyroscope Offset for <see cref="GyroscopeCalibrationOffset"/>
        /// </summary>
        public void ReGenerateGyroscopeOffset()
        {
            int tempGx = 0;
            int tempGy = 0;
            int tempGz = 0;
            GyroscopeCalibrationOffset = new int[3];
            for (int i = 0; i < 32; i++)
            {
                GyroscopeAccelerationRead(out _, out int[] gyro, GyroscopeCalibrationOffset);
                tempGx += gyro[0];
                tempGy += gyro[1];
                tempGz += gyro[2];
                Thread.Sleep(10);
            }
            GyroscopeCalibrationOffset[0] = tempGx >> 5;
            GyroscopeCalibrationOffset[1] = tempGy >> 5;
            GyroscopeCalibrationOffset[2] = tempGz >> 5;
        }

        public override void Start()
        {
            base.Start();
            if (!SelfTest())
            {
                Stop();
                throw new SystemException("ICM20948 Self-Test routine failed!");
            }
            Thread.Sleep(500);
            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_0);
            WritePrimary((byte)ICM20948_BANK0.REG_ADD_PWR_MIGMT_1, (byte)ICM20948_BANK0.REG_VAL_ALL_RGE_RESET); //Reset
            Thread.Sleep(100);
            WritePrimary((byte)ICM20948_BANK0.REG_ADD_PWR_MIGMT_1, (byte)ICM20948_BANK0.REG_VAL_RUN_MODE); //Power-ON

            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_2);
            //INIT GYRO
            WritePrimary((byte)ICM20948_BANK2.REG_ADD_GYRO_SMPLRT_DIV, 0x07);
            WritePrimary((byte)ICM20948_BANK2.REG_ADD_GYRO_CONFIG_1, (byte)ICM20948_BANK2.REG_VAL_BIT_GYRO_DLPCFG_6 | (byte)ICM20948_BANK2.REG_VAL_BIT_GYRO_FS_1000DPS | (byte)ICM20948_BANK2.REG_VAL_BIT_GYRO_DLPF);
            WritePrimary((byte)ICM20948_BANK2.REG_ADD_ACCEL_SMPLRT_DIV_2, 0x07);
            WritePrimary((byte)ICM20948_BANK2.REG_ADD_ACCEL_CONFIG, (byte)ICM20948_BANK2.REG_VAL_BIT_ACCEL_DLPCFG_6 | (byte)ICM20948_BANK2.REG_VAL_BIT_ACCEL_FS_2g | (byte)ICM20948_BANK2.REG_VAL_BIT_ACCEL_DLPF);

            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_0);
            Thread.Sleep(100);

            ReGenerateGyroscopeOffset();

            if (!MagSelfTest()) //Magnetic field Self Test
            {
                Stop();
                throw new SystemException("AK09916 Magnetic Self-Test routine failed!");
            }
            WriteSecondary(I2C_ADD_ICM20948_AK09916 | I2C_ADD_ICM20948_AK09916_WRITE, (byte)ICM20948_MAG.REG_ADD_MAG_CNTL2, (byte)ICM20948_MAG.REG_VAL_MAG_MODE_20HZ); //20 HZ mode
        }

        public override void Stop()
        {
            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_0);
            WritePrimary((byte)ICM20948_BANK0.REG_ADD_PWR_MIGMT_1, (byte)ICM20948_BANK0.REG_VAL_ALL_RGE_RESET); //Reset
            base.Stop();
        }

        /// <summary>
        /// Updates <see cref="Pitch"/> and <see cref="Roll"/> and <see cref="Yaw"/> <br></br>
        /// <b>WARNING:</b> This method can take some time to calculate!
        /// </summary>
        /// <param name="acceleration">Measured acceleration - Array of three</param>
        /// <param name="gyroscope">Measured gyroscope - Array of three</param>
        /// <param name="magneticField">Measured magnetic field - Array of three</param>
        public void UpdatePitchRollYaw(in int[] acceleration, in int[] gyroscope, in int[] magneticField)
        {
            ImuAHRSUpdate(acceleration, gyroscope, magneticField);
        }

        /// <summary>
        /// Not Supported, automatically calls <see cref="WritePrimary(byte[])"/>
        /// </summary>
        /// <param name="data">Data to write</param>
        public override void WriteData(byte[] data)
        {
            WritePrimary(data);
        }

        public void WritePrimary(params byte[] data)
        {
            I2CDevice.Write(data);
        }

        public void WriteSecondary(byte I2CAddr, byte registerAddr, byte data)
        {
            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_3);
            WritePrimary((byte)ICM20948_BANK3.REG_ADD_I2C_SLV1_ADDR, I2CAddr);
            WritePrimary((byte)ICM20948_BANK3.REG_ADD_I2C_SLV1_REG, registerAddr);
            WritePrimary((byte)ICM20948_BANK3.REG_ADD_I2C_SLV1_DO, data);
            WritePrimary((byte)ICM20948_BANK3.REG_ADD_I2C_SLV1_CTRL, ((byte)ICM20948_BANK3.REG_VAL_BIT_SLV0_EN) | 1);

            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_0);
            var temp = ReadWritePrimary((byte)ICM20948_BANK0.REG_ADD_USER_CTRL);
            temp = (byte)(temp | (byte)ICM20948_BANK0.REG_VAL_BIT_I2C_MST_EN);
            WritePrimary((byte)ICM20948_BANK0.REG_ADD_USER_CTRL, temp);
            Thread.Sleep(10);
            temp = (byte)(temp & (~(byte)ICM20948_BANK0.REG_VAL_BIT_I2C_MST_EN));
            WritePrimary((byte)ICM20948_BANK0.REG_ADD_USER_CTRL, temp);

            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_3);
            temp = ReadWritePrimary((byte)ICM20948_BANK3.REG_ADD_I2C_SLV0_CTRL);
            temp = (byte)(temp & ~(((byte)ICM20948_BANK0.REG_VAL_BIT_I2C_MST_EN) & ((byte)ICM20948_BANK3.REG_VAL_BIT_MASK_LEN)));
            WritePrimary((byte)ICM20948_BANK3.REG_ADD_I2C_SLV0_CTRL, temp);

            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_0);
        }

        #endregion Public Methods

        #region Private Methods

        private void ImuAHRSUpdate(in int[] acceleration, in int[] gyroscope, in int[] magneticField)
        {
            double[] realGyro = new double[3];
            double[] realAccl = new double[3];
            double[] realMgnt = new double[3];
            realGyro[0] = (gyroscope[0] / 32.8f) * 0.0175f;
            realGyro[1] = (gyroscope[1] / 32.8f) * 0.0175f;
            realGyro[2] = (gyroscope[2] / 32.8f) * 0.0175f;
            realAccl[0] = acceleration[0];
            realAccl[1] = acceleration[1];
            realAccl[2] = acceleration[2];
            realMgnt[0] = magneticField[0];
            realMgnt[1] = magneticField[1];
            realMgnt[2] = magneticField[2];
            double gx = realGyro[0];
            double gy = realGyro[1];
            double gz = realGyro[2];
            double ax = realAccl[0];
            double ay = realAccl[1];
            double az = realAccl[2];
            double mx = realMgnt[0];
            double my = realMgnt[1];
            double mz = realMgnt[2];
            double exInt = 0f, eyInt = 0f, ezInt = 0f;
            double q1 = 0f, q2 = 0f, q3 = 0f;
            double q0 = 1.0f;
            double halfT = 0.024f;
            double Ki = 1.0f;
            double Kp = 4.50f;
            double q0q0 = q0 * q0;
            double q0q1 = q0 * q1;
            double q0q2 = q0 * q2;
            double q0q3 = q0 * q3;
            double q1q1 = q1 * q1;
            double q1q2 = q1 * q2;
            double q1q3 = q1 * q3;
            double q2q2 = q2 * q2;
            double q2q3 = q2 * q3;
            double q3q3 = q3 * q3;

            double norm = 1 / Math.Sqrt(ax * ax + ay * ay + az * az);
            ax *= norm;
            ay *= norm;
            az *= norm;

            norm = 1 / Math.Sqrt(mx * mx + my * my + mz * mz); //Normalize magnetometer
            mx *= norm;
            my *= norm;
            mz *= norm;

            // compute reference direction of flux
            double hx = 2f * mx * (0.5f - q2q2 - q3q3) + 2f * my * (q1q2 - q0q3) + 2f * mz * (q1q3 + q0q2);
            double hy = 2f * mx * (q1q2 + q0q3) + 2f * my * (0.5f - q1q1 - q3q3) + 2f * mz * (q2q3 - q0q1);
            double hz = 2f * mx * (q1q3 - q0q2) + 2f * my * (q2q3 + q0q1) + 2 * mz * (0.5f - q1q1 - q2q2);
            double bx = Math.Sqrt(hx * hx + hy * hy);
            double bz = hz;

            // estimated direction of gravity and flux (v and w)
            double vx = 2f * (q1q3 - q0q2);
            double vy = 2f * (q0q1 + q2q3);
            double vz = q0q0 - q1q1 - q2q2 + q3q3;
            double wx = 2f * bx * (0.5f - q2q2 - q3q3) + 2f * bz * (q1q3 - q0q2);
            double wy = 2f * bx * (q1q2 - q0q3) + 2f * bz * (q0q1 + q2q3);
            double wz = 2f * bx * (q0q2 + q1q3) + 2f * bz * (0.5f - q1q1 - q2q2);

            //error is sum of cross product between reference direction of fields and direction measured by sensors
            double ex = ay * vz - az * vy + (my * wz - mz * wy);
            double ey = az * vx - ax * vz + (mz * wx - mx * wz);
            double ez = ax * vy - ay * vx + (mx * wy - my * wx);

            if (ex != 0.0 && ey != 0.0 && ez != 0.0)
            {
                exInt += ex * Ki * halfT;
                eyInt += ey * Ki * halfT;
                ezInt += ez * Ki * halfT;

                gx = gx + Kp * ex + exInt;
                gy = gy + Kp * ey + eyInt;
                gz = gz + Kp * ez + ezInt;
            }

            q0 += (-q1 * gx - q2 * gy - q3 * gz) * halfT;
            q1 += (q0 * gx + q2 * gz - q3 * gy) * halfT;
            q2 += (q0 * gy - q1 * gz + q3 * gx) * halfT;
            q3 += (q0 * gz + q1 * gy - q2 * gx) * halfT;

            norm = (1 / Math.Sqrt(q0 * q0 + q1 * q1 + q2 * q2 + q3 * q3));
            q0 *= norm;
            q1 *= norm;
            q2 *= norm;
            q3 *= norm;

            Pitch = Math.Asin(-2 * q1 * q3 + 2 * q0 * q2) * 57.3f;
            Roll = Math.Atan2(2 * q2 * q3 + 2 * q0 * q1, -2 * q1 * q1 - 2 * q2 * q2 + 1) * 57.3f;
            Yaw = Math.Atan2(-2 * q1 * q2 - 2 * q0 * q3, 2 * q2 * q2 + 2 * q3 * q3 - 1) * 57.3f;
        }

        private bool MagSelfTest()
        {
            var result = ReadSecondary(I2C_ADD_ICM20948_AK09916 | I2C_ADD_ICM20948_AK09916_READ, (byte)ICM20948_MAG.REG_ADD_MAG_WIA1, 2);
            return (result[0] == (byte)ICM20948_MAG.REG_VAL_MAG_WIA1 && result[1] == (byte)ICM20948_MAG.REG_VAL_MAG_WIA2);
        }

        private byte ReadWritePrimary(byte command)
        {
            WritePrimary(command);
            return ReadPrimary();
        }

        private byte[] ReadWritePrimary(byte command, byte length)
        {
            WritePrimary(command);
            return ReadPrimary(length);
        }

        private byte[] ReadWriteSecondary(byte I2Caddress, byte registerAddress, byte command)
        {
            WriteSecondary(I2Caddress, registerAddress, command);
            return ReadSecondary(I2Caddress, registerAddress, command);
        }

        private bool SelfTest()
        {
            return ReadWritePrimary((byte)ICM20948_BANK0.REG_ADD_WIA) == ((byte)ICM20948_BANK0.REG_VAL_WIA);
        }

        /// <summary>
        /// Switches banks
        /// </summary>
        /// <param name="targetBank">
        /// Bank to switch to, accepts: <see cref="ICM20948_BANK0.REG_VAL_REG_BANK_0"/>, <see cref="ICM20948_BANK0.REG_VAL_REG_BANK_1"/>, <see cref="ICM20948_BANK0.REG_VAL_REG_BANK_2"/>, <see cref="ICM20948_BANK0.REG_VAL_REG_BANK_3"/>
        /// </param>
        private void SwitchBanks(ICM20948_BANK0 targetBank)
        {
            if (targetBank != ICM20948_BANK0.REG_VAL_REG_BANK_0 && targetBank != ICM20948_BANK0.REG_VAL_REG_BANK_1 && targetBank != ICM20948_BANK0.REG_VAL_REG_BANK_2 && targetBank != ICM20948_BANK0.REG_VAL_REG_BANK_3) //Switch only banks
                return;
            WritePrimary((byte)ICM20948_BANK0.REG_ADD_REG_BANK_SEL, (byte)targetBank);
        }

        #endregion Private Methods
    }
}