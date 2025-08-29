using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads vendor-specific Remeha Data-ID 132 (payload semantics are OEM-specific).
    /// </summary>
    public class GetRemeha132Request : ReadRequest
    {
        public GetRemeha132Request() : base() { }
        public GetRemeha132Request(Request baseReq) : base(baseReq) { }

        public override MessageID MessageID => MessageID.Remeha132;
        public override MessageType MessageType => MessageType.READ_DATA;

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { /* no-op */ }
    }
}
