using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetVentilationFHBSizeRequest : ReadRequest
    {
        public GetVentilationFHBSizeRequest() : base() { }
        public GetVentilationFHBSizeRequest(Request baseReq) : base(baseReq) { }

        protected override ulong GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(ulong value) { }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.FHBsizeVentilationHeatRecovery;
    }
}
