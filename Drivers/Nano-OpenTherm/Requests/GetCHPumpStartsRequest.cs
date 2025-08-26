using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of starts of the central heating pump.
    /// </summary>
    public class GetCHPumpStartsRequest : ReadRequest
    {
        public GetCHPumpStartsRequest() : base() { }
        public GetCHPumpStartsRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Number of starts of the central heating pump (low 16 bits, unsigned).
    /// Units: count.
    /// </summary>
    public ushort Count { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Count);
        protected override void SetRawDataCore(uint value) { Count = Utilities.GetLowUShort(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.CHPumpStarts;
    }
}
