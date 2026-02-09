namespace TekuSP.Drivers.ICM20948.Constants
{
    /// <summary>
    /// AK09916 I2C read/write flags for the slave address.
    /// </summary>
    public enum MagI2cReadWriteFlag : byte
    {
        /// <summary>Write flag for the I2C slave address.</summary>
        Write = 0x00,
        /// <summary>Read flag for the I2C slave address.</summary>
        Read = 0x80
    }
}
