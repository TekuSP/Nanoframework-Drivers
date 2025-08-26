using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Max CH water Setpoint upper & lower bounds for adjustment (°C)
    /// </summary>
    public class GetMaxTSetBoundsRequest : ReadRequest
    {
        public GetMaxTSetBoundsRequest() : base() { }
        public GetMaxTSetBoundsRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Upper bound for the Max CH setpoint in °C (encoded in the high byte).
    /// </summary>
    public byte UpperBound { get; set; }
    /// <summary>
    /// Lower bound for the Max CH setpoint in °C (encoded in the low byte).
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
