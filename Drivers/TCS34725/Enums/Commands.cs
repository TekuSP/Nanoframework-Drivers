namespace TCS34725.Enums
{
    public enum Commands
    {
        TCS34725_CMD_BIT = 0x80,
        TCS34725_CMD_Read_Byte = 0x00,
        TCS34725_CMD_Read_Word = 0x20,
        TCS34725_CMD_Clear_INT = 0x66
    }
}
