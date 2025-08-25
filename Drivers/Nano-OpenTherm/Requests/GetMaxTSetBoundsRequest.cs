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

        public byte UpperBound { get; set; }
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
