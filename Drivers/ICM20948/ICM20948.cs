using System;
using System.Device.I2c;
using System.Threading;

using DriverBase;

namespace ICM20948
{
    public class ICM20948 : DriverBaseI2C
    {
        private const byte I2C_ADD_ICM20948_AK09916 = 0x0C;
        private const byte I2C_ADD_ICM20948_AK09916_READ = 0x80;
        private const byte I2C_ADD_ICM20948_AK09916_WRITE = 0x00;

        public ICM20948(int I2CBusID, int deviceAddress = 0x68) : base("ICM20948", I2CBusID, deviceAddress)
        {
        }

        public ICM20948(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress = 0x68) : base("ICM20948", I2CBusID, connectionSettings, deviceAddress)
        {
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
        public byte ReadPrimary()
        {
            return I2CDevice.ReadByte();
        }
        public byte[] ReadSecondary(byte I2CAddr, byte registerAddr, byte length)
        {
            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_3);
            WritePrimary((byte)ICM20948_BANK3.REG_ADD_I2C_SLV1_ADDR, I2CAddr);
            WritePrimary((byte)ICM20948_BANK3.REG_ADD_I2C_SLV1_REG, registerAddr);
            WritePrimary((byte)ICM20948_BANK3.REG_ADD_I2C_SLV1_CTRL, (byte)(((byte)ICM20948_BANK3.REG_VAL_BIT_SLV0_EN) | length));

            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_0);
            var temp = ReadWritePrimary((byte)ICM20948_BANK0.REG_ADD_USER_CTRL);
            temp = (byte)(temp | (byte)ICM20948_BANK0.REG_VAL_BIT_I2C_MST_EN);
            WritePrimary((byte)ICM20948_BANK0.REG_ADD_USER_CTRL, temp);
            Thread.Sleep(10);
            temp = (byte)(temp & (~(byte)ICM20948_BANK0.REG_VAL_BIT_I2C_MST_EN));
            WritePrimary((byte)ICM20948_BANK0.REG_ADD_USER_CTRL, temp);

            byte[] returnValue = new byte[length];
            for (byte i = 0; i < length; i++)
                returnValue[i] = ReadWritePrimary((byte)(((byte)ICM20948_BANK0.REG_VAL_REG_BANK_0) + i));

            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_3);
            temp = ReadWritePrimary((byte)ICM20948_BANK3.REG_ADD_I2C_SLV0_CTRL);
            temp = (byte)(temp & ~(((byte)ICM20948_BANK0.REG_VAL_BIT_I2C_MST_EN) & ((byte)ICM20948_BANK3.REG_VAL_BIT_MASK_LEN)));
            WritePrimary((byte)ICM20948_BANK3.REG_ADD_I2C_SLV0_CTRL, temp);

            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_0);
            return returnValue;
        }
        private byte ReadWritePrimary(byte command)
        {
            WritePrimary(command);
            return ReadPrimary();
        }
        private byte[] ReadWriteSecondary(byte I2Caddress, byte registerAddress, byte command)
        {
            WriteSecondary(I2Caddress,registerAddress,command);
            return ReadSecondary(I2Caddress, registerAddress, command);
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

            //TODO Gyro Offset here

            if (!MagSelfTest()) //Magnetic field Self Test
            {
                Stop();
                throw new SystemException("ICM20948 Magnetic Self-Test routine failed!");
            }
            WriteSecondary(I2C_ADD_ICM20948_AK09916 | I2C_ADD_ICM20948_AK09916_WRITE, (byte)ICM20948_MAG.REG_ADD_MAG_CNTL2, (byte)ICM20948_MAG.REG_VAL_MAG_MODE_20HZ); //20 HZ mode
        }
        public override void Stop()
        {
            SwitchBanks(ICM20948_BANK0.REG_VAL_REG_BANK_0);
            WritePrimary((byte)ICM20948_BANK0.REG_ADD_PWR_MIGMT_1, (byte)ICM20948_BANK0.REG_VAL_ALL_RGE_RESET); //Reset
            base.Stop();
        }
        public override string ReadDeviceId()
        {
            return "Not supported";
        }

        public override string ReadManufacturerId()
        {
            return "TDK InvenSense";
        }

        public override string ReadSerialNumber()
        {
            return "Not supported";
        }

        private bool SelfTest()
        {
            return ReadWritePrimary((byte)ICM20948_BANK0.REG_ADD_WIA) == ((byte)ICM20948_BANK0.REG_VAL_WIA);
        }
        private bool MagSelfTest()
        {
            var result = ReadSecondary(I2C_ADD_ICM20948_AK09916 | I2C_ADD_ICM20948_AK09916_READ, (byte)ICM20948_MAG.REG_ADD_MAG_WIA1, 2);
            return (result[0] == (byte)ICM20948_MAG.REG_VAL_MAG_WIA1 && result[1] == (byte)ICM20948_MAG.REG_VAL_MAG_WIA2);
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
        #region Registers
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
        #endregion
    }
}
