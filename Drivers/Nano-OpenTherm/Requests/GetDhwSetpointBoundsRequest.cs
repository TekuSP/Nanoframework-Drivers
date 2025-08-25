using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// DHW Setpoint upper & lower bounds for adjustment (°C)
    /// </summary>
    public class GetDhwSetpointBoundsRequest : ReadRequest
    {
        public GetDhwSetpointBoundsRequest() : base() { }
        public GetDhwSetpointBoundsRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Upper bound for DHW setpoint in degrees Celsius (encoded as integer in high byte).
        /// </summary>
        public byte UpperBound { get; set; }
        /// <summary>
        /// Lower bound for DHW setpoint in degrees Celsius (encoded as integer in low byte).
        /// </summary>
        public byte LowerBound { get; set; }

        protected override uint GetRawDataCore()
        {
            // Encode any set bounds back into the data field (high=upper, low=lower)
            uint raw = (uint)((UpperBound << 8) | LowerBound);
            return ProcessRequest(raw);
        }
        protected override void SetRawDataCore(uint value)
        {
            UpperBound = Utilities.GetHighByte(value);
            LowerBound = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.TdhwSetUBTdhwSetLB;
    }
}
