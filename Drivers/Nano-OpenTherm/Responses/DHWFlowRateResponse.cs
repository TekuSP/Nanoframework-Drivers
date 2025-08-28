using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class DHWFlowRateResponse : Response
    {
        public DHWFlowRateResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public DHWFlowRateResponse(Response r) : base(r) { }
        public float FlowLitresPerMinute { get; set; }
        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawTemperature(FlowLitresPerMinute));
        protected override void SetRawDataCore(uint value) => FlowLitresPerMinute = Utilities.GetFloat(value);
        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.DHWFlowRate;
    }
}
