namespace TekuSP.Drivers.TCS34725.Enums
{
    /// <summary>
    /// Command register values.
    /// </summary>
    public enum Commands
    {
        /// <summary>Command bit (selects command mode).</summary>
        TCS34725_CMD_BIT = 0x80,
        /// <summary>Read byte command modifier.</summary>
        TCS34725_CMD_Read_Byte = 0x00,
        /// <summary>Read word command modifier.</summary>
        TCS34725_CMD_Read_Word = 0x20,
        /// <summary>Clear interrupt command.</summary>
        TCS34725_CMD_Clear_INT = 0x66
    }
}
