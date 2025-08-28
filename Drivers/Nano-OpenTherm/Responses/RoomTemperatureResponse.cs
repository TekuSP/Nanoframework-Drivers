using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class RoomTemperatureResponse : Response
    {
        public RoomTemperatureResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public RoomTemperatureResponse(Response r) : base(r) { }
        public float TemperatureC { get; set; }
        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawTemperature(TemperatureC));
        protected override void SetRawDataCore(uint value) => TemperatureC = Utilities.GetFloat(value);
        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.Tr;
    }
}
