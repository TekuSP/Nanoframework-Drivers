namespace TekuSP.Drivers.ICM20948.Constants
{
    /// <summary>
    /// ICM20948 device-specific I2C constants for the embedded AK09916 magnetometer.
    /// </summary>
    public static class ICM20948Constants
    {
        /// <summary>AK09916 I2C address on the internal I2C master.</summary>
        public const byte I2C_ADD_ICM20948_AK09916 = 0x0C;
        /// <summary>AK09916 read flag for I2C slave address.</summary>
        public const byte I2C_ADD_ICM20948_AK09916_READ = 0x80;
        /// <summary>AK09916 write flag for I2C slave address.</summary>
        public const byte I2C_ADD_ICM20948_AK09916_WRITE = 0x00;
    }
}
