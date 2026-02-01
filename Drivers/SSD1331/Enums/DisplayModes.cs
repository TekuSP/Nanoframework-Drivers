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
}
