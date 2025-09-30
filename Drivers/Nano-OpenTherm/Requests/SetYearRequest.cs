using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Calendar year
    /// </summary>
    public class SetYearRequest : WriteRequest
    {
        #region Public Constructors

        public SetYearRequest() : base()
        {
        }

        public SetYearRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.YearResponse);

        public override MessageID MessageID => MessageID.Year;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// Calendar year encoded in the low 16 bits of the payload.
        /// Typical valid range: 0..4095 (implementation specific).
        /// </summary>
        public ushort Year { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            // year in low 16 bits (spec uses 0..4095 typically), keep raw low 16
            return ProcessRequest(Year);
        }

        protected override void SetRawDataCore(uint value)
        {
            Year = Utilities.GetLowUShort(value);
        }

        #endregion Protected Methods
    }
}