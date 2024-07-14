namespace TekuSP.Drivers.DriverBase.Enums.OpenTherm
{
    /// <summary>
    /// Message operation type for OpenTherm
    /// </summary>
    public enum MessageType : byte
    {
        /*  Master to Slave */
        READ_DATA = 0b000,
        WRITE_DATA = 0b001,
        INVALID_DATA = 0b010,
        RESERVED = 0b011,
        /* Slave to Master */
        READ_ACK = 0b100,
        WRITE_ACK = 0b101,
        DATA_INVALID = 0b110,
        UNKNOWN_DATA_ID = 0b111
    }
}
