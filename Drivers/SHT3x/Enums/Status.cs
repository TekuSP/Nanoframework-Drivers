namespace TekuSP.Drivers.SHT3x.Enums
{
    /// <summary>
    /// Status results for SHT3x operations.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Operation completed successfully.
        /// </summary>
        SHT3x_Status_Nominal = 0,

        /// <summary>
        /// General error.
        /// </summary>
        SHT3x_Status_Error = 1,

        /// <summary>
        /// CRC check failed.
        /// </summary>
        SHT3x_Status_CRC_Fail = 2,

        /// <summary>
        /// Read did not complete.
        /// </summary>
        SHT3x_Status_Read_Failed = 3
    }
}
