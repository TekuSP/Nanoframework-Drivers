namespace TekuSP.Drivers.Nano_OpenTherm.Enums
{
    [System.Flags]
    public enum SlaveStatus : byte
    {
        /// <summary>
        /// Fault indication.
        /// </summary>
        FaultIndication = 1 << 0,
        /// <summary>
        /// CH mode.
        /// </summary>
        CHMode = 1 << 1,
        /// <summary>
        /// DHW mode.
        /// </summary>
        DHWMode = 1 << 2,
        /// <summary>
        /// Flame status.
        /// </summary>
        FlameStatus = 1 << 3,
        /// <summary>
        /// Cooling status.
        /// </summary>
        CoolingStatus = 1 << 4,
        /// <summary>
        /// CH2 mode.
        /// </summary>
        CH2Mode = 1 << 5,
        /// <summary>
        /// Diagnostic indication.
        /// </summary>
        DiagnosticIndication = 1 << 6,
        /// <summary>
        /// Reserved bit 7.
        /// </summary>
        Reserved = 1 << 7,
    }
}
