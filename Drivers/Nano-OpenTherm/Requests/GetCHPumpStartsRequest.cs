using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetCHPumpStartsRequest : ReadRequest
    {
        public GetCHPumpStartsRequest() : base() { }
        public GetCHPumpStartsRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Number of starts of the central heating pump (encoded in the low 16 bits).
    /// Units: count.
    /// </summary>
    /// <summary>
    /// Number of central heating pump starts, encoded as a 16-bit unsigned integer (OpenTherm ID 117, low 16 bits).
    /// Units: count.
    /// </summary>
    public ushort Count { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Count);
        protected override void SetRawDataCore(uint value) { Count = Utilities.GetLowUShort(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.CHPumpStarts;
    }
}
