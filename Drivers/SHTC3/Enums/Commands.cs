namespace TekuSP.Drivers.SHTC3.Enums
{
    /// <summary>
    /// SHTC3 Control commands
    /// </summary>
    public enum Commands
    {
        /// <summary>
        /// Wake-up command
        /// </summary>
        SHTC3_CMD_WAKE = 0x3517,

        /// <summary>
        /// Sleep command
        /// </summary>
        SHTC3_CMD_SLEEP = 0xB098,

        /// <summary>
        /// Reset command
        /// </summary>
        SHTC3_CMD_SFT_RST = 0x805D,

        /// <summary>
        /// Read ID command
        /// </summary>
        SHTC3_CMD_READ_ID = 0xEFC8,
    }
}