using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetRemoteRequestRequest : ReadRequest
    {
        public GetRemoteRequestRequest() : base() { }
        public GetRemoteRequestRequest(Request baseReq) : base(baseReq) { }

        public MasterStatus MasterStatus { get; private set; }

        protected override ulong GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(ulong value)
        {
            MasterStatus = Utilities.GetMasterStatus(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.RemoteRequest;
    }
}
