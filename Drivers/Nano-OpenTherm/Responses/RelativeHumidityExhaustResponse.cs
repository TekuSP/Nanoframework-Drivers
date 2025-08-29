using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Relative humidity in the exhaust air (%). Use U8 encoding (0..100) per project decision for ID78.
    /// </summary>
    public class RelativeHumidityExhaustResponse : Response
    {
        public RelativeHumidityExhaustResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public RelativeHumidityExhaustResponse(Response r) : base(r) { }

        public byte Percent { get; set; }

        protected override uint GetRawDataCore()
            => ProcessResponse(Utilities.MakeUShort(0, (byte)Utilities.Normalize(Percent)));
        protected override void SetRawDataCore(uint value) => Percent = Utilities.GetLowByte(value);

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.RHexhaust;
    }
}
