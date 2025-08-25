using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetDHWBurnerStartsRequest : ReadRequest
    {
        public GetDHWBurnerStartsRequest() : base() { }
        public GetDHWBurnerStartsRequest(Request baseReq) : base(baseReq) { }

        public ushort Count { get; set; }

        protected override ulong GetRawDataCore() => ProcessRequest(Count);
        protected override void SetRawDataCore(ulong value) { Count = Utilities.GetLowUShort(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.DHWBurnerStarts;
    }
}
