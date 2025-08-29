using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Relative ventilation position (0-100%).
    /// Note: Use U8 encoding (0..100) in low byte per project decision for ID71.
    /// TODO: If device expects f8.8, introduce per-ID switch.
    /// </summary>
    public class VentilationPositionResponse : Response
    {
        public VentilationPositionResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public VentilationPositionResponse(Response r) : base(r) { }

        /// <summary>Ventilation position in percent (0-100).</summary>
        public byte Percent { get; set; }

        protected override uint GetRawDataCore()
            => ProcessResponse(Utilities.MakeUShort(0, (byte)Utilities.Normalize(Percent)));
        protected override void SetRawDataCore(uint value) => Percent = Utilities.GetLowByte(value);

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.Vset;
    }
}
