using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// OT version implemented in the ventilation / heat recovery system
    /// </summary>
    public class GetOpenThermVersionVentilationRequest : ReadRequest
    {
        public GetOpenThermVersionVentilationRequest() : base() { }
        public GetOpenThermVersionVentilationRequest(Request baseReq) : base(baseReq) { }

        public byte Major { get; set; }
        public byte Minor { get; set; }

        protected override ulong GetRawDataCore()
        {
            uint raw = (uint)((Major << 8) | Minor);
            return ProcessRequest(raw);
        }
        protected override void SetRawDataCore(ulong value)
        {
            Major = Utilities.GetHighByte(value);
            Minor = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.OpenThermVersionVentilationHeatRecovery;
    }
}
