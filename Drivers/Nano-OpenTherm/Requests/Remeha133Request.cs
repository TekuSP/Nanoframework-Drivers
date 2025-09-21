using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Vendor-specific Remeha Data-ID 133 request (READ only per OTGW map).
    /// Payload semantics are OEM-defined; exposes a raw 16-bit <see cref="Value"/> for completeness.
    /// </summary>
    public class Remeha133Request : ReadRequest
    {
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.Remeha133Response);
        public Remeha133Request() : base() { }

        public Remeha133Request(Request baseReq) : base(baseReq) { }

        public override MessageID MessageID => MessageID.Remeha133;
        public override MessageType MessageType => MessageType.READ_DATA;

        /// <summary>Raw 16-bit payload to send/receive.</summary>
        public ushort Value { get; set; }

    protected override uint GetRawDataCore() => ProcessRequest(Value);
    protected override void SetRawDataCore(uint value)
    { Value = (ushort)(value & 0xFFFF); }
    }
}
