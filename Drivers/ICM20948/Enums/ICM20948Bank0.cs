namespace TekuSP.Drivers.ICM20948.Enums
{
    /// <summary>
    /// ICM20948 register map (user bank 0).
    /// </summary>
    public enum ICM20948_BANK0
    {
        /// <summary>WHO_AM_I register address.</summary>
        REG_ADD_WIA = 0x00,
        /// <summary>Expected WHO_AM_I value.</summary>
        REG_VAL_WIA = 0xEA,
        /// <summary>User control register address.</summary>
        REG_ADD_USER_CTRL = 0x03,
        /// <summary>DMP enable bit.</summary>
        REG_VAL_BIT_DMP_EN = 0x80,
        /// <summary>FIFO enable bit.</summary>
        REG_VAL_BIT_FIFO_EN = 0x40,
        /// <summary>I2C master enable bit.</summary>
        REG_VAL_BIT_I2C_MST_EN = 0x20,
        /// <summary>I2C interface disable bit.</summary>
        REG_VAL_BIT_I2C_IF_DIS = 0x10,
        /// <summary>DMP reset bit.</summary>
        REG_VAL_BIT_DMP_RST = 0x08,
        /// <summary>Diamond DMP reset bit.</summary>
        REG_VAL_BIT_DIAMOND_DMP_RST = 0x04,
        /// <summary>Power management 1 register address (legacy alias).</summary>
        REG_ADD_PWR_MIGMT_1 = 0x06,
        /// <summary>Reset all registers bit.</summary>
        REG_VAL_ALL_RGE_RESET = 0x80,
        /// <summary>Run (non-low-power) mode value.</summary>
        REG_VAL_RUN_MODE = 0x01, //Nonlow-powermode
        /// <summary>Low power configuration register address.</summary>
        REG_ADD_LP_CONFIG = 0x05,
        /// <summary>Power management 1 register address.</summary>
        REG_ADD_PWR_MGMT_1 = 0x06,
        /// <summary>Power management 2 register address.</summary>
        REG_ADD_PWR_MGMT_2 = 0x07,
        /// <summary>Accelerometer X high byte register address.</summary>
        REG_ADD_ACCEL_XOUT_H = 0x2D,
        /// <summary>Accelerometer X low byte register address.</summary>
        REG_ADD_ACCEL_XOUT_L = 0x2E,
        /// <summary>Accelerometer Y high byte register address.</summary>
        REG_ADD_ACCEL_YOUT_H = 0x2F,
        /// <summary>Accelerometer Y low byte register address.</summary>
        REG_ADD_ACCEL_YOUT_L = 0x30,
        /// <summary>Accelerometer Z high byte register address.</summary>
        REG_ADD_ACCEL_ZOUT_H = 0x31,
        /// <summary>Accelerometer Z low byte register address.</summary>
        REG_ADD_ACCEL_ZOUT_L = 0x32,
        /// <summary>Gyroscope X high byte register address.</summary>
        REG_ADD_GYRO_XOUT_H = 0x33,
        /// <summary>Gyroscope X low byte register address.</summary>
        REG_ADD_GYRO_XOUT_L = 0x34,
        /// <summary>Gyroscope Y high byte register address.</summary>
        REG_ADD_GYRO_YOUT_H = 0x35,
        /// <summary>Gyroscope Y low byte register address.</summary>
        REG_ADD_GYRO_YOUT_L = 0x36,
        /// <summary>Gyroscope Z high byte register address.</summary>
        REG_ADD_GYRO_ZOUT_H = 0x37,
        /// <summary>Gyroscope Z low byte register address.</summary>
        REG_ADD_GYRO_ZOUT_L = 0x38,
        /// <summary>External sensor data 0 register address.</summary>
        REG_ADD_EXT_SENS_DATA_00 = 0x3B,
        /// <summary>Register bank select register address.</summary>
        REG_ADD_REG_BANK_SEL = 0x7F,
        /// <summary>Bank 0 select value.</summary>
        REG_VAL_REG_BANK_0 = 0x00,
        /// <summary>Bank 1 select value.</summary>
        REG_VAL_REG_BANK_1 = 0x10,
        /// <summary>Bank 2 select value.</summary>
        REG_VAL_REG_BANK_2 = 0x20,
        /// <summary>Bank 3 select value.</summary>
        REG_VAL_REG_BANK_3 = 0x30
    }
}
