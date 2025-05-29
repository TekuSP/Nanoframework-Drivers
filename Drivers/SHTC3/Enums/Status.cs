namespace TekuSP.Drivers.SHTC3.Enums
{
    /// <summary>
    /// Status messages for SHTC3
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// The one and only "all is good" return value
        /// </summary>
        SHTC3_Status_Nominal = 0,

        /// <summary>
        /// The most general of error values - can mean anything depending on the context
        /// </summary>
        SHTC3_Status_Error,

        /// <summary>
        /// This return value means the computed checksum did not match the provided value
        /// </summary>
        SHTC3_Status_CRC_Fail,

        /// <summary>
        /// This status means that the ID of the device did not match the format for SHTC3
        /// </summary>
        SHTC3_Status_ID_Fail
    }
}