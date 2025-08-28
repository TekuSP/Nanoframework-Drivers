using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Relative Modulation Level (%).
    /// </summary>
    public class RelModulationResponse : Response
    {
        public RelModulationResponse(MessageType messageType = MessageType.READ_ACK) => MessageType = messageType;
    public RelModulationResponse(Response baseResponse) : base(baseResponse) { }

        /// <summary>Relative modulation level percentage (0..100).</summary>
        public float RelativeModulationPercent { get; set; }

        protected override uint GetRawDataCore()
            => ProcessResponse(Utilities.GetRawPercentage(RelativeModulationPercent));

        protected override void SetRawDataCore(uint value)
            => RelativeModulationPercent = Utilities.GetPercentage(value);

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.RelModLevel;
    }
}
