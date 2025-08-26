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

    /// <summary>
    /// OpenTherm major version number (high byte).
    /// </summary>
    public byte Major { get; set; }
    /// <summary>
    /// OpenTherm minor version number (low byte).
    /// </summary>
    public byte Minor { get; set; }

        protected override uint GetRawDataCore()
        {
            uint raw = (uint)((Major << 8) | Minor);
            return ProcessRequest(raw);
        }
        protected override void SetRawDataCore(uint value)
        {
            Major = Utilities.GetHighByte(value);
            Minor = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.OpenThermVersionVentilationHeatRecovery;
    }
}
