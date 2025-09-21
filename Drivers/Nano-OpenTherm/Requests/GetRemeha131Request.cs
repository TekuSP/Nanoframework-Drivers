using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads vendor-specific Remeha Data-ID 131 (payload semantics are OEM-specific).
    /// </summary>
    public class GetRemeha131Request : ReadRequest
    {
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.Remeha131Response);
        public GetRemeha131Request() : base() { }
        public GetRemeha131Request(Request baseReq) : base(baseReq) { }

        public override MessageID MessageID => MessageID.Remeha131;
        public override MessageType MessageType => MessageType.READ_DATA;

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { /* no-op */ }
    }
}
