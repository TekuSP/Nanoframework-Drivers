using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetExhaustOutletTemperatureRequest : ReadRequest
    {
        public GetExhaustOutletTemperatureRequest() : base() { }
        public GetExhaustOutletTemperatureRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.Teo;
    }
}
