namespace TekuSP.Drivers.SSD1331
{
    /// <summary>
    /// Supported SSD1331 display modes.
    /// </summary>
    public enum DisplayModes
    {
        /// <summary>
        /// Normal display output.
        /// </summary>
        Normal = 0xA4,

        /// <summary>
        /// Entire display ON, all pixels turn ON at GS63.
        /// </summary>
        AllPixelsOn = 0xA5,

        /// <summary>
        /// Entire display OFF, all pixels turn OFF.
        /// </summary>
        AllPixelsOff = 0xA6,

        /// <summary>
        /// Inverse display output.
        /// </summary>
        Inverse = 0xA7
    }

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
