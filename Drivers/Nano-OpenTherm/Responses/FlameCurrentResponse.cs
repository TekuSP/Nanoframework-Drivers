using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class FlameCurrentResponse : Response
    {
        public FlameCurrentResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public FlameCurrentResponse(Response r) : base(r) { }
        public float MicroAmps { get; set; }
        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawTemperature(MicroAmps));
        protected override void SetRawDataCore(uint value) => MicroAmps = Utilities.GetFloat(value);
        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.FlameCurrent;
    }
}
