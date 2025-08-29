using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class OTCHeatCurveRatioResponse : Response
    {
        public OTCHeatCurveRatioResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public OTCHeatCurveRatioResponse(Response r) : base(r) { }

        public float Ratio { get; set; }
        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawTemperature(Ratio));
        protected override void SetRawDataCore(uint value) => Ratio = Utilities.GetFloat(value);
        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.OTCHeatCurveRatio;
    }
}
