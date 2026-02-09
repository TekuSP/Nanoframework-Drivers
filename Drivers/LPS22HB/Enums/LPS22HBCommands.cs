namespace TekuSP.Drivers.LPS22HB.Enums
{
    /// <summary>
    /// LPS22HB register addresses and command values.
    /// </summary>
    public enum LPS22HBCommands
    {
        /// <summary>WHO_AM_I register address.</summary>
        LPS22HB_WHO_AM_I = 0x0F, //Who am I
        /// <summary>Resolution configuration register address.</summary>
        LPS22HB_RES_CONF = 0x1A, //Normal (0) or Low current mode (1)
        /// <summary>Control register 1 (output rate and filter settings).</summary>
        LPS22HB_CTRL_REG1 = 0x10, //Output rate and filter settings
        /// <summary>Control register 2 (BOOT, FIFO_EN, IF_ADD_INC, SWRESET, One_Shot).</summary>
        LPS22HB_CTRL_REG2 = 0x11, //BOOT FIFO_EN STOP_ON_FTH IF_ADD_INC I2C_DIS SWRESET One_Shot
        /// <summary>Status register (pressure/temperature data available).</summary>
        LPS22HB_STATUS_REG = 0x27, //Temp or Press data available bits
        /// <summary>Pressure output X_LSB register address.</summary>
        LPS22HB_PRES_OUT_XL = 0x28, //XLSB
        /// <summary>Pressure output LSB register address.</summary>
        LPS22HB_PRES_OUT_L = 0x29, //LSB
        /// <summary>Pressure output MSB register address.</summary>
        LPS22HB_PRES_OUT_H = 0x2A, //MSB
        /// <summary>Temperature output LSB register address.</summary>
        LPS22HB_TEMP_OUT_L = 0x2B, //LSB
        /// <summary>Temperature output MSB register address.</summary>
        LPS22HB_TEMP_OUT_H = 0x2C //MSB
    }
}
