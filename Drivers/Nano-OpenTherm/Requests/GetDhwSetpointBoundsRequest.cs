using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the adjustable bounds for the DHW setpoint temperature (upper/lower).
    /// </summary>
    public class GetDhwSetpointBoundsRequest : ReadRequest
    {
        public GetDhwSetpointBoundsRequest() : base() { }
        public GetDhwSetpointBoundsRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Upper bound for DHW setpoint in °C (high byte).
        /// </summary>
        public byte UpperBound { get; set; }
        /// <summary>
        /// Lower bound for DHW setpoint in °C (low byte).
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
