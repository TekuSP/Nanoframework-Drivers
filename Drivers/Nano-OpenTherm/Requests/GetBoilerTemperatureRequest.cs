using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetBoilerTemperatureRequest : ReadRequest
    {
        public GetBoilerTemperatureRequest() : base() { }
        public GetBoilerTemperatureRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { /* no properties to populate */ }
        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.Tboiler;
    }
}
