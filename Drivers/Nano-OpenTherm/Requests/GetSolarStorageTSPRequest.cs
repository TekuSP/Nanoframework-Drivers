using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetSolarStorageTSPRequest : ReadRequest
    {
        public GetSolarStorageTSPRequest() : base() { }
        public GetSolarStorageTSPRequest(Request baseReq) : base(baseReq) { }

        public byte Index { get; set; }

        protected override ulong GetRawDataCore() => ProcessRequest(Index);
        protected override void SetRawDataCore(ulong value) { Index = Utilities.GetLowByte(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.TSPindexTSPvalueSolarStorage;
    }
}
