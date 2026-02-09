namespace TekuSP.Drivers.ICM20948.Enums
{
    /// <summary>
    /// AK09916 magnetometer register map and mode values.
    /// </summary>
    public enum ICM20948_MAG
    {
        /// <summary>Magnetometer WIA1 register address.</summary>
        REG_ADD_MAG_WIA1 = 0x00,
        /// <summary>Expected WIA1 value.</summary>
        REG_VAL_MAG_WIA1 = 0x48,
        /// <summary>Magnetometer WIA2 register address.</summary>
        REG_ADD_MAG_WIA2 = 0x01,
        /// <summary>Expected WIA2 value.</summary>
        REG_VAL_MAG_WIA2 = 0x09,
        /// <summary>Status 2 register address.</summary>
        REG_ADD_MAG_ST2 = 0x10,
        /// <summary>Magnetometer data register base address.</summary>
        REG_ADD_MAG_DATA = 0x11,
        /// <summary>Control 2 register address (mode select).</summary>
        REG_ADD_MAG_CNTL2 = 0x31,
        /// <summary>Power-down mode.</summary>
        REG_VAL_MAG_MODE_PD = 0x00,
        /// <summary>Single measurement mode.</summary>
        REG_VAL_MAG_MODE_SM = 0x01,
        /// <summary>Continuous 10 Hz mode.</summary>
        REG_VAL_MAG_MODE_10HZ = 0x02,
        /// <summary>Continuous 20 Hz mode.</summary>
        REG_VAL_MAG_MODE_20HZ = 0x04,
        /// <summary>Continuous 50 Hz mode.</summary>
        REG_VAL_MAG_MODE_50HZ = 0x05,
        /// <summary>Continuous 100 Hz mode.</summary>
        REG_VAL_MAG_MODE_100HZ = 0x08,
        /// <summary>Self-test mode.</summary>
        REG_VAL_MAG_MODE_ST = 0x10
    }
}
