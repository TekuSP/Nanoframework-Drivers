using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class YearResponse : Response
    {
        public YearResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public YearResponse(Response r) : base(r) { }

        public ushort Year { get; set; }

        protected override uint GetRawDataCore() => ProcessResponse(Year);
        protected override void SetRawDataCore(uint value) => Year = Utilities.GetLowUShort(value);
        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.Year;
    }
}
