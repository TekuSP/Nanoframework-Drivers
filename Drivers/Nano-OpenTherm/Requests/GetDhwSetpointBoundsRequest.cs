using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the adjustable bounds for the DHW setpoint temperature (upper/lower).
    /// </summary>
    public class GetDhwSetpointBoundsRequest : ReadRequest
    {
        #region Public Constructors

        public GetDhwSetpointBoundsRequest() : base()
        {
        }

        public GetDhwSetpointBoundsRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Lower bound for DHW setpoint in °C (low byte).
        /// </summary>
        public byte LowerBound { get; set; }

        public override MessageID MessageID => MessageID.TdhwSetUBTdhwSetLB;

        public override MessageType MessageType => MessageType.READ_DATA;

        /// <summary>
        /// Upper bound for DHW setpoint in °C (high byte).
        /// </summary>
        public byte UpperBound { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            // Encode any set bounds back into the data field (high=upper, low=lower)
            ushort payload = Utilities.MakeUShort(UpperBound, LowerBound);
            return ProcessRequest(payload);
        }

        protected override void SetRawDataCore(uint value)
        {
            UpperBound = Utilities.GetHighByte(value);
            LowerBound = Utilities.GetLowByte(value);
        }

        #endregion Protected Methods
    }
}