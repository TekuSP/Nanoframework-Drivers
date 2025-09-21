using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads vendor-specific Remeha Data-ID 133 (payload semantics are OEM-specific).
    /// </summary>
    public class GetRemeha133Request : ReadRequest
    {
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.Remeha133Response);
        public GetRemeha133Request() : base() { }
        public GetRemeha133Request(Request baseReq) : base(baseReq) { }

        public override MessageID MessageID => MessageID.Remeha133;
        public override MessageType MessageType => MessageType.READ_DATA;

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { /* no-op */ }
    }
}
