using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class OTCHCRatioBoundsResponse : Response
    {
        public OTCHCRatioBoundsResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public OTCHCRatioBoundsResponse(Response r) : base(r) { }

    public sbyte UpperBound { get; set; }
    public sbyte LowerBound { get; set; }

        protected override uint GetRawDataCore()
        {
            ushort payload = Utilities.MakeUShort((byte)UpperBound, (byte)LowerBound);
            return ProcessResponse(payload);
        }
        protected override void SetRawDataCore(uint value)
        {
            UpperBound = (sbyte)Utilities.GetHighByte(value);
            LowerBound = (sbyte)Utilities.GetLowByte(value);
        }
        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.OTCHCRatioBounds;
    }
}
