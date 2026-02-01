namespace TekuSP.Drivers.SHTC3.Enums
{
    /// <summary>
    /// High-level measurement modes for SHTC3 operation.
    /// </summary>
    public enum MeasurementMode
    {
        /// <summary>
        /// Clock stretching, Normal power mode.
        /// </summary>
        SHTC3_CMD_CSE_NPM = 1,

        /// <summary>
        /// Clock stretching, Low power mode.
        /// </summary>
        SHTC3_CMD_CSE_LPM = 2,

        /// <summary>
        /// Polling, Normal power mode.
        /// </summary>
        SHTC3_CMD_CSD_NPM = 3,

        /// <summary>
        /// Polling, Low power mode.
        /// </summary>
        SHTC3_CMD_CSD_LPM = 4
    }
}
