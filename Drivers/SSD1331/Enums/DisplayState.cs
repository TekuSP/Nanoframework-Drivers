namespace TekuSP.Drivers.SSD1331
{
    /// <summary>
    /// Supported SSD1331 display power states.
    /// </summary>
    public enum DisplayState
    {
        /// <summary>
        /// Display ON in dim mode.
        /// </summary>
        OnDim = 0xAC,

        /// <summary>
        /// Display OFF (sleep mode).
        /// </summary>
        OFF = 0xAE,

        /// <summary>
        /// Display ON in normal mode.
        /// </summary>
        ON = 0xAF
    }
}
