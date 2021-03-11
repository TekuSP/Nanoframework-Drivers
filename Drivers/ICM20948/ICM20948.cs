using System;
using System.Device.I2c;

using DriverBase;

namespace ICM20948
{
    public class ICM20948 : DriverBaseI2C
    {
        /// <summary>
        /// AK09916 I2C address on the bus
        /// </summary>
        public int SecondaryDeviceAddress { get; }
        /// <summary>
        /// Read I2C address on the bus
        /// </summary>
        public int ReadDeviceAddress { get; }
        /// <summary>
        /// Write I2C address on the bus
        /// </summary>
        public int WriteDeviceAddress { get; }
        protected I2cDevice SecondaryI2CDevice;
        protected I2cDevice ReadI2CDevice;
        protected I2cDevice WriteI2CDevice;
        public ICM20948(int I2CBusID, int deviceAddress = 0x68, int secondaryAddress = 0x0C, int deviceReadAddress = 0x80, int deviceWriteAddress = 0x00) : base("ICM20948", I2CBusID, deviceAddress)
        {
            SecondaryDeviceAddress = secondaryAddress;
            ReadDeviceAddress = deviceReadAddress;
            WriteDeviceAddress = deviceWriteAddress;
        }

        public ICM20948(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress = 0x68, int secondaryAddress = 0x0C, int deviceReadAddress = 0x80, int deviceWriteAddress = 0x00) : base("ICM20948", I2CBusID, connectionSettings, deviceAddress)
        {
            SecondaryDeviceAddress = secondaryAddress;
            ReadDeviceAddress = deviceReadAddress;
            WriteDeviceAddress = deviceWriteAddress;
        }

        public override long ReadData(byte pointer)
        {
            throw new NotImplementedException();
        }

        public override long ReadData(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override string ReadDeviceId()
        {
            throw new NotImplementedException();
        }

        public override string ReadManufacturerId()
        {
            throw new NotImplementedException();
        }

        public override string ReadSerialNumber()
        {
            throw new NotImplementedException();
        }

        public override void WriteData(byte[] data)
        {
            throw new NotImplementedException();
        }
        public override bool IsRunning => I2CDevice != null && SecondaryI2CDevice != null && ReadI2CDevice != null && WriteI2CDevice != null;
        public override void Start()
        {
            base.Start();
            SecondaryI2CDevice = new I2cDevice(new I2cConnectionSettings(I2CConnectionSettings.BusId, SecondaryDeviceAddress, I2CConnectionSettings.BusSpeed));
            ReadI2CDevice = new I2cDevice(new I2cConnectionSettings(I2CConnectionSettings.BusId, ReadDeviceAddress, I2CConnectionSettings.BusSpeed));
            WriteI2CDevice = new I2cDevice(new I2cConnectionSettings(I2CConnectionSettings.BusId, WriteDeviceAddress, I2CConnectionSettings.BusSpeed));
        }
        public override void Stop()
        {
            base.Stop();
            SecondaryI2CDevice?.Dispose();
            SecondaryI2CDevice = null;
            ReadI2CDevice?.Dispose();
            ReadI2CDevice = null;
            WriteI2CDevice?.Dispose();
            WriteI2CDevice = null;
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
