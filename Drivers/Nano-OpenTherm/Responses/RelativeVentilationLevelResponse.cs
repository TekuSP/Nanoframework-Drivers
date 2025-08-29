using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Relative ventilation level percentage (0..100%).
    /// Note: Use U8 encoding (0..100) per project decision for ID77.
    /// </summary>
    public class RelativeVentilationLevelResponse : Response
    {
        public RelativeVentilationLevelResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public RelativeVentilationLevelResponse(Response r) : base(r) { }

        public byte Percent { get; set; }

        protected override uint GetRawDataCore()
            => ProcessResponse(Utilities.MakeUShort(0, (byte)Utilities.Normalize(Percent)));
        protected override void SetRawDataCore(uint value) => Percent = Utilities.GetLowByte(value);

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.RelVentLevel;
    }
}
