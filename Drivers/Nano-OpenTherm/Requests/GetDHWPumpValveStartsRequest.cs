using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of DHW pump starts or valve openings.
    /// </summary>
    public class GetDHWPumpValveStartsRequest : ReadRequest
    {
        public GetDHWPumpValveStartsRequest() : base() { }
        public GetDHWPumpValveStartsRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Number of DHW pump starts or valve openings (low 16 bits, unsigned).
        /// Units: count.
        /// </summary>
        public ushort Count { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Count);
        protected override void SetRawDataCore(uint value) { Count = Utilities.GetLowUShort(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.DHWPumpValveStarts;
    }
}
