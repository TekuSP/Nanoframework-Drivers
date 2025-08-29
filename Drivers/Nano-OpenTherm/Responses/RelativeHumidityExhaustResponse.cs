using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Relative Humidity (u8/u8). For ID 78 the OTGW map treats both bytes as percentage values (0..100).
    /// Typically HB may carry supply/ambient and LB the exhaust; treat both as raw percentages.
    /// </summary>
    public class RelativeHumidityExhaustResponse : Response
    {
        public RelativeHumidityExhaustResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public RelativeHumidityExhaustResponse(Response r) : base(r) { }

        /// <summary>High byte relative humidity in percent (0-100).</summary>
        public byte HighPercent { get; set; }

        /// <summary>Low byte relative humidity in percent (0-100).</summary>
        public byte LowPercent { get; set; }

        protected override uint GetRawDataCore()
            => ProcessResponse(Utilities.MakeUShort((byte)Utilities.Normalize(HighPercent), (byte)Utilities.Normalize(LowPercent)));
        protected override void SetRawDataCore(uint value)
        {
            HighPercent = Utilities.GetHighByte(value);
            LowPercent = Utilities.GetLowByte(value);
        }

    public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.RHexhaust;
    }
}
