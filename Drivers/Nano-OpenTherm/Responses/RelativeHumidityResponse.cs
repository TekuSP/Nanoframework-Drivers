using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class RelativeHumidityResponse : Response
    {
        public RelativeHumidityResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public RelativeHumidityResponse(Response r) : base(r) { }
        public float RelativeHumidityPercent { get; set; }
        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawPercentage(RelativeHumidityPercent));
        protected override void SetRawDataCore(uint value) => RelativeHumidityPercent = Utilities.GetPercentage(value);
        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.RelativeHumidity;
    }
}
