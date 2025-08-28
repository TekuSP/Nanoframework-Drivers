using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// OpenTherm protocol version in Slave. Packed as BCD major.minor: high byte = major, low byte = minor.
    /// </summary>
    public class OpenThermVersionSlaveResponse : Response
    {
        public OpenThermVersionSlaveResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public OpenThermVersionSlaveResponse(Response r) : base(r) { }

        public byte Major { get; set; }
        public byte Minor { get; set; }

        protected override uint GetRawDataCore()
        {
            ushort payload = Utilities.MakeUShort(Major, Minor);
            return ProcessResponse(payload);
        }
        protected override void SetRawDataCore(uint value)
        {
            Major = Utilities.GetHighByte(value);
            Minor = Utilities.GetLowByte(value);
        }
        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.OpenThermVersionSlave;
    }
}
