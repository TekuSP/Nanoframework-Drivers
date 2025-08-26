using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetSolarStorageFHBEntryRequest : ReadRequest
    {
        public GetSolarStorageFHBEntryRequest() : base() { }
        public GetSolarStorageFHBEntryRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Fault history buffer index to read (0-based).
    /// </summary>
    public byte Index { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Index);
        protected override void SetRawDataCore(uint value) { Index = Utilities.GetLowByte(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.FHBindexFHBvalueSolarStorage;
    }
}
