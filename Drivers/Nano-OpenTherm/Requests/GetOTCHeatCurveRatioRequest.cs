using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetOTCHeatCurveRatioRequest : ReadRequest
    {
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.OTCHeatCurveRatioResponse);
        public GetOTCHeatCurveRatioRequest() : base() { }
        public GetOTCHeatCurveRatioRequest(Request baseReq) : base(baseReq) { }

        public override MessageID MessageID => MessageID.OTCHeatCurveRatio;
        public override MessageType MessageType => MessageType.READ_DATA;

        public float Ratio { get; protected set; }

        protected override uint GetRawDataCore() => ProcessRequest(Utilities.GetRawTemperature(Ratio));
        protected override void SetRawDataCore(uint value) => Ratio = Utilities.GetFloat(value);
    }
}
