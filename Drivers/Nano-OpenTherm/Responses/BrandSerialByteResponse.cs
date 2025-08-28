using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Single byte of brand/serial number in low byte.</summary>
    public class BrandSerialByteResponse : Response
    {
        public BrandSerialByteResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public BrandSerialByteResponse(Response r) : base(r) { }
        public byte Data { get; set; }
        protected override uint GetRawDataCore() => ProcessResponse(Data);
        protected override void SetRawDataCore(uint value) => Data = Utilities.GetLowByte(value);
        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.BrandSerialNumber;
    }
}
