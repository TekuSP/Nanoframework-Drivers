namespace TekuSP.Drivers.ICM20948.Enums
{
    /// <summary>
    /// ICM20948 register map (user bank 2) for gyro/accel configuration.
    /// </summary>
    public enum ICM20948_BANK2
    {
        /// <summary>Gyro sample rate divider register.</summary>
        REG_ADD_GYRO_SMPLRT_DIV = 0x00,
        /// <summary>Gyro configuration register 1.</summary>
        REG_ADD_GYRO_CONFIG_1 = 0x01,
        /// <summary>Gyro DLPF config (bit[5:3]) - 2.</summary>
        REG_VAL_BIT_GYRO_DLPCFG_2 = 0x10,//bit[5:3]
        /// <summary>Gyro DLPF config (bit[5:3]) - 4.</summary>
        REG_VAL_BIT_GYRO_DLPCFG_4 = 0x20,//bit[5:3]
        /// <summary>Gyro DLPF config (bit[5:3]) - 6.</summary>
        REG_VAL_BIT_GYRO_DLPCFG_6 = 0x30,//bit[5:3]
        /// <summary>Gyro full-scale 250 dps (bit[2:1]).</summary>
        REG_VAL_BIT_GYRO_FS_250DPS = 0x00,//bit[2:1]
        /// <summary>Gyro full-scale 500 dps (bit[2:1]).</summary>
        REG_VAL_BIT_GYRO_FS_500DPS = 0x02,//bit[2:1]
        /// <summary>Gyro full-scale 1000 dps (bit[2:1]).</summary>
        REG_VAL_BIT_GYRO_FS_1000DPS = 0x04,//bit[2:1]
        /// <summary>Gyro full-scale 2000 dps (bit[2:1]).</summary>
        REG_VAL_BIT_GYRO_FS_2000DPS = 0x06,//bit[2:1]
        /// <summary>Gyro DLPF enable bit (bit[0]).</summary>
        REG_VAL_BIT_GYRO_DLPF = 0x01,//bit[0]
        /// <summary>Accelerometer sample rate divider 2 register.</summary>
        REG_ADD_ACCEL_SMPLRT_DIV_2 = 0x11,
        /// <summary>Accelerometer configuration register.</summary>
        REG_ADD_ACCEL_CONFIG = 0x14,
        /// <summary>Accel DLPF config (bit[5:3]) - 2.</summary>
        REG_VAL_BIT_ACCEL_DLPCFG_2 = 0x10,//bit[5:3]
        /// <summary>Accel DLPF config (bit[5:3]) - 4.</summary>
        REG_VAL_BIT_ACCEL_DLPCFG_4 = 0x20,//bit[5:3]
        /// <summary>Accel DLPF config (bit[5:3]) - 6.</summary>
        REG_VAL_BIT_ACCEL_DLPCFG_6 = 0x30,//bit[5:3]
        /// <summary>Accel full-scale 2g (bit[2:1]).</summary>
        REG_VAL_BIT_ACCEL_FS_2g = 0x00,//bit[2:1]
        /// <summary>Accel full-scale 4g (bit[2:1]).</summary>
        REG_VAL_BIT_ACCEL_FS_4g = 0x02,//bit[2:1]
        /// <summary>Accel full-scale 8g (bit[2:1]).</summary>
        REG_VAL_BIT_ACCEL_FS_8g = 0x04,//bit[2:1]
        /// <summary>Accel full-scale 16g (bit[2:1]).</summary>
        REG_VAL_BIT_ACCEL_FS_16g = 0x06,//bit[2:1]
        /// <summary>Accel DLPF enable bit (bit[0]).</summary>
        REG_VAL_BIT_ACCEL_DLPF = 0x01//bit[0]
    }
}
