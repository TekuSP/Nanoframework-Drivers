using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the adjustable bounds for the maximum CH water setpoint (upper/lower).
    /// </summary>
    public class GetMaxTSetBoundsRequest : ReadRequest
    {
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.MaxTSetBoundsResponse);
        #region Public Constructors

        public GetMaxTSetBoundsRequest() : base()
        {
        }

        public GetMaxTSetBoundsRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Lower bound for the Max CH setpoint in °C (low byte).
        /// </summary>
        public byte LowerBound { get; set; }

        public override MessageID MessageID => MessageID.MaxTSetUBMaxTSetLB;

        public override MessageType MessageType => MessageType.READ_DATA;

        /// <summary>
        /// Upper bound for the Max CH setpoint in °C (high byte).
        /// </summary>
        public byte UpperBound { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
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