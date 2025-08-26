using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the adjustable bounds for the maximum CH water setpoint (upper/lower).
    /// </summary>
    public class GetMaxTSetBoundsRequest : ReadRequest
    {
        public GetMaxTSetBoundsRequest() : base() { }
        public GetMaxTSetBoundsRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Upper bound for the Max CH setpoint in °C (high byte).
    /// </summary>
    public byte UpperBound { get; set; }
    /// <summary>
    /// Lower bound for the Max CH setpoint in °C (low byte).
    /// </summary>
    public byte LowerBound { get; set; }

        protected override uint GetRawDataCore()
        {
            uint raw = (uint)((UpperBound << 8) | LowerBound);
            return ProcessRequest(raw);
        }
        protected override void SetRawDataCore(uint value)
        {
            UpperBound = Utilities.GetHighByte(value);
            LowerBound = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.MaxTSetUBMaxTSetLB;
    }
}
