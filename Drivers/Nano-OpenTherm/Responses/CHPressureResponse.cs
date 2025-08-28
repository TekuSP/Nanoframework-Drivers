using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class CHPressureResponse : Response
    {
        public CHPressureResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public CHPressureResponse(Response r) : base(r) { }
        public float PressureBar { get; set; }
        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawTemperature(PressureBar));
        protected override void SetRawDataCore(uint value) => PressureBar = Utilities.GetFloat(value);
        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.CHPressure;
    }
}
