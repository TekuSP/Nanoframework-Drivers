using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the DHW pump/valve total operation hours.
    /// </summary>
    public class GetDHWPumpValveOperationHoursRequest : ReadRequest
    {
        #region Public Constructors

        public GetDHWPumpValveOperationHoursRequest() : base()
        {
        }

        public GetDHWPumpValveOperationHoursRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// DHW pump/valve operation hours (low 16 bits, unsigned).
        /// Units: hours.
        /// </summary>
        public ushort Hours { get; set; }

        public override MessageID MessageID => MessageID.DHWPumpValveOperationHours;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Hours);

        protected override void SetRawDataCore(uint value)
        { Hours = Utilities.GetLowUShort(value); }

        #endregion Protected Methods
    }
}