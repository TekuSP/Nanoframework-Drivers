using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetRelativeVentilationLevelRequest : ReadRequest
    {
        public GetRelativeVentilationLevelRequest() : base() { }
        public GetRelativeVentilationLevelRequest(Request baseReq) : base(baseReq) { }

        protected override ulong GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(ulong value) { }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.RelVentLevel;
    }
}
