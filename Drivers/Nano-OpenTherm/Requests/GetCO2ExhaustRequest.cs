using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetCO2ExhaustRequest : ReadRequest
    {
        public GetCO2ExhaustRequest() : base() { }
        public GetCO2ExhaustRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.CO2exhaust;
    }
}
