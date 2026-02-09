namespace TekuSP.Drivers.QMP6988.Enums
{
    /// <summary>
    /// Control and Status registers.
    /// Used during initialization and configuration.
    /// </summary>
    public enum ControlRegister : byte
    {
        /// <summary>
        /// Chip Identification. Read-only. Expected value: 0x5C.
        /// </summary>
        CHIP_ID = 0xD1,

        /// <summary>
        /// Soft Reset. Write-only. Write 0xE6 to reset.
        /// </summary>
        RESET = 0xE0,

        /// <summary>
        /// Device Status. Read-only.
        /// </summary>
        DEVICE_STAT = 0xF0,

        /// <summary>
        /// IIR Filter settings.
        /// </summary>
        IIR_FILTER = 0xF1,

        /// <summary>
        /// Control Measurement (Power Mode, Oversampling).
        /// </summary>
        CTRL_MEAS = 0xF4,

        /// <summary>
        /// IO Setup (Standby Time).
        /// </summary>
        IO_SETUP = 0xF5,
    }
}
