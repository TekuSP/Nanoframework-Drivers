namespace TekuSP.Drivers.ICM20948.Enums
{
    /// <summary>
    /// ICM20948 register map (user bank 3) for I2C master/slave configuration.
    /// </summary>
    public enum ICM20948_BANK3
    {
        /// <summary>I2C slave 0 address register.</summary>
        REG_ADD_I2C_SLV0_ADDR = 0x03,
        /// <summary>I2C slave 0 register address.</summary>
        REG_ADD_I2C_SLV0_REG = 0x04,
        /// <summary>I2C slave 0 control register.</summary>
        REG_ADD_I2C_SLV0_CTRL = 0x05,
        /// <summary>I2C slave 0 enable bit.</summary>
        REG_VAL_BIT_SLV0_EN = 0x80,
        /// <summary>I2C slave 0 length mask.</summary>
        REG_VAL_BIT_MASK_LEN = 0x07,
        /// <summary>I2C slave 0 data out register.</summary>
        REG_ADD_I2C_SLV0_DO = 0x06,
        /// <summary>I2C slave 1 address register.</summary>
        REG_ADD_I2C_SLV1_ADDR = 0x07,
        /// <summary>I2C slave 1 register address.</summary>
        REG_ADD_I2C_SLV1_REG = 0x08,
        /// <summary>I2C slave 1 control register.</summary>
        REG_ADD_I2C_SLV1_CTRL = 0x09,
        /// <summary>I2C slave 1 data out register.</summary>
        REG_ADD_I2C_SLV1_DO = 0x0A
    }
}
