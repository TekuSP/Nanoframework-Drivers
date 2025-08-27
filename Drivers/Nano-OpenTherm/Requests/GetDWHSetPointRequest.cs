using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the current domestic hot water (DHW) setpoint temperature.
    /// </summary>
    /// <remarks>
    /// Response payload: low 16 bits as 8.8 fixed-point degrees Celsius.
    /// </remarks>
    public class GetDWHSetPointRequest : ReadRequest
    {
        #region Public Constructors

        public GetDWHSetPointRequest() : base()
        {
        }

        public GetDWHSetPointRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Tdhw;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(0);

        protected override void SetRawDataCore(uint value)
        { }

        #endregion Protected Methods
    }
}