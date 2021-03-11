namespace SHTC3.Enums
{
    /// <summary>
    /// Measurment mode for SHTC3
    /// </summary>
    public enum MeasurementModes
    {
        /// <summary>
        /// Clock stretching, RH first, Normal power mode
        /// </summary>
        SHTC3_CMD_CSE_RHF_NPM = 0x5C24,

        /// <summary>
        /// Clock stretching, RH first, Low power mode
        /// </summary>
        SHTC3_CMD_CSE_RHF_LPM = 0x44DE,

        /// <summary>
        /// Clock stretching, T first, Normal power mode
        /// </summary>
        SHTC3_CMD_CSE_TF_NPM = 0x7CA2,

        /// <summary>
        /// Clock stretching, T first, Low power mode
        /// </summary>
        SHTC3_CMD_CSE_TF_LPM = 0x6458,

        /// <summary>
        /// Polling, RH first, Normal power mode
        /// </summary>
        SHTC3_CMD_CSD_RHF_NPM = 0x58E0,

        /// <summary>
        /// Polling, RH first, Low power mode
        /// </summary>
        SHTC3_CMD_CSD_RHF_LPM = 0x401A,

        /// <summary>
        /// Polling, T first, Normal power mode
        /// </summary>
        SHTC3_CMD_CSD_TF_NPM = 0x7866,

        /// <summary>
        /// Polling, T first, Low power mode
        /// </summary>
        SHTC3_CMD_CSD_TF_LPM = 0x609C
    }
}