using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Relative humidity in the exhaust air (%). 8.8 fixed-point.
    /// </summary>
    public class RelativeHumidityExhaustResponse : Response
    {
        public RelativeHumidityExhaustResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public RelativeHumidityExhaustResponse(Response r) : base(r) { }

        public float Percent { get; set; }

        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawPercentage(Percent));
        protected override void SetRawDataCore(uint value) => Percent = Utilities.GetPercentage(value);

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.RHexhaust;
    }
}
