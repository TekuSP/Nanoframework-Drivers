using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Cooling control signal (%).</summary>
    public class CoolingControlResponse : Response
    {
        public CoolingControlResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public CoolingControlResponse(Response r) : base(r) { }

        public float Percent { get; set; }

        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawPercentage(Percent));
        protected override void SetRawDataCore(uint value) => Percent = Utilities.GetPercentage(value);

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.CoolingControl;
    }
}
