using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// CO2 in exhaust measured value. Encoding per spec/device (8.8 fixed point).
    /// </summary>
    public class CO2ExhaustResponse : Response
    {
        public CO2ExhaustResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public CO2ExhaustResponse(Response r) : base(r) { }

        public float Value { get; set; }

        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawTemperature(Value));
        protected override void SetRawDataCore(uint value) => Value = Utilities.GetFloat(value);

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.CO2exhaust;
    }
}
