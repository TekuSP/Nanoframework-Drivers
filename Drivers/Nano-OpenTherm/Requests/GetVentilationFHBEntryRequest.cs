using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetVentilationFHBEntryRequest : ReadRequest
    {
        public GetVentilationFHBEntryRequest() : base() { }
        public GetVentilationFHBEntryRequest(Request baseReq) : base(baseReq) { }

        public byte Index { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Index);
        protected override void SetRawDataCore(uint value) { Index = Utilities.GetLowByte(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.FHBindexFHBvalueVentilationHeatRecovery;
    }
}
