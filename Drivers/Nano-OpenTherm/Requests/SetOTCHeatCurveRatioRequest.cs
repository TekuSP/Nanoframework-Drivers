using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class SetOTCHeatCurveRatioRequest : WriteRequest
    {
        public SetOTCHeatCurveRatioRequest() : base() { }
        public SetOTCHeatCurveRatioRequest(Request baseReq) : base(baseReq) { }

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.OTCHeatCurveRatioResponse);

        public override MessageID MessageID => MessageID.OTCHeatCurveRatio;
        public override MessageType MessageType => MessageType.WRITE_DATA;

        public float Ratio { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Utilities.GetRawTemperature(Ratio));
        protected override void SetRawDataCore(uint value) => Ratio = Utilities.GetFloat(value);
    }
}
