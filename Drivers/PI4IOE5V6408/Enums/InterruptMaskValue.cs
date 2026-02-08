namespace TekuSP.Drivers.PI4IOE5V6408.Enums
{
    /// <summary>
    /// Interrupt mask values (note: 0 == enabled, 1 == masked/disabled).
    /// </summary>
    public enum InterruptMaskValue : byte
    {
        /// <summary>
        /// Interrupt masked/disabled (register bit=1).
        /// </summary>
        Disabled = 1,

        /// <summary>
        /// Interrupt enabled (register bit=0).
        /// </summary>
        Enabled = 0
    }
}