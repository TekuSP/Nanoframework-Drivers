using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the current domestic hot water (DHW) temperature (°C).
    /// </summary>
    public class GetDHWTemperatureRequest : ReadRequest
    {
        #region Public Constructors

        public GetDHWTemperatureRequest() : base()
        {
        }

        public GetDHWTemperatureRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.DHWTemperatureResponse);
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