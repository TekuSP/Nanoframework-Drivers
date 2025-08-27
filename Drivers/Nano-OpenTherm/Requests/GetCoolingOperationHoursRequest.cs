using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of hours the slave has operated in Cooling mode.
    /// </summary>
    /// <summary>
    /// Number of hours that the slave is in Cooling Mode
    /// </summary>
    public class GetCoolingOperationHoursRequest : ReadRequest
    {
        #region Public Constructors

        public GetCoolingOperationHoursRequest() : base()
        {
        }

        public GetCoolingOperationHoursRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Total cooling operation hours (low 16 bits, unsigned).
        /// Units: hours.
        /// </summary>
        public ushort Hours { get; private set; }

        public override MessageID MessageID => MessageID.CoolingOperationHours;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(0);

        protected override void SetRawDataCore(uint value)
        {
            Hours = Utilities.GetLowUShort(value);
        }

        #endregion Protected Methods
    }
}