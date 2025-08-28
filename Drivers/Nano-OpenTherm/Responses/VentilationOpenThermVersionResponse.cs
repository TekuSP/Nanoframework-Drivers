using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>OpenTherm protocol version in Ventilation/HR unit (BCD major.minor in bytes).</summary>
    public class VentilationOpenThermVersionResponse : Response
    {
        public VentilationOpenThermVersionResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public VentilationOpenThermVersionResponse(Response r) : base(r) { }

        public byte Major { get; set; }
        public byte Minor { get; set; }

        protected override uint GetRawDataCore()
            => ProcessResponse(Utilities.MakeUShort(Major, Minor));

        protected override void SetRawDataCore(uint value)
        { Major = Utilities.GetHighByte(value); Minor = Utilities.GetLowByte(value); }

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.OpenThermVersionVentilationHeatRecovery;
    }
}
