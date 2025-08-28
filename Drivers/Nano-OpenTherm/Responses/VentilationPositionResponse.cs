using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Relative ventilation position (0-100%).
    /// High/low payload stores percent in 8.8 format by convention across repo helpers.
    /// </summary>
    public class VentilationPositionResponse : Response
    {
        public VentilationPositionResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public VentilationPositionResponse(Response r) : base(r) { }

        /// <summary>Ventilation position in percent (0-100).</summary>
        public float Percent { get; set; }

        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawPercentage(Percent));
        protected override void SetRawDataCore(uint value) => Percent = Utilities.GetPercentage(value);

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.Vset;
    }
}
