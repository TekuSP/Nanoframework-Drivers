namespace TekuSP.Drivers.QMP6988.Enums
{
    /// <summary>
    /// Registers containing live sensor data. 
    /// These are read repeatedly in the measurement loop.
    /// </summary>
    public enum DataRegister : byte
    {
        /// <summary>
        /// Pressure Data (MSB). 
        /// <para>Start reading 6 bytes from here to get both Pressure and Temperature.</para>
        /// </summary>
        PRESS_MSB = 0xF7,

        /// <summary>
        /// Pressure Data (LSB).
        /// </summary>
        PRESS_LSB = 0xF8,

        /// <summary>
        /// Pressure Data (XLSB).
        /// </summary>
        PRESS_XLSB = 0xF9,

        /// <summary>
        /// Temperature Data (MSB).
        /// </summary>
        TEMP_MSB = 0xFA,

        /// <summary>
        /// Temperature Data (LSB).
        /// </summary>
        TEMP_LSB = 0xFB,

        /// <summary>
        /// Temperature Data (XLSB).
        /// </summary>
        TEMP_XLSB = 0xFC
    }
}
