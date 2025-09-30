using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetDHWBurnerOperationHoursRequest : ReadRequest
    {
        #region Public Constructors

        public GetDHWBurnerOperationHoursRequest() : base()
        {
        }

        public GetDHWBurnerOperationHoursRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.DHWBurnerOperationHoursResponse);

        /// <summary>
        /// Number of hours the burner has operated during DHW mode (low 16 bits, unsigned).
        /// Units: hours.
        /// </summary>
        public ushort Hours { get; set; }

        public override MessageID MessageID => MessageID.DHWBurnerOperationHours;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Hours);

        protected override void SetRawDataCore(uint value)
        { Hours = Utilities.GetLowUShort(value); }

        #endregion Protected Methods
    }
}