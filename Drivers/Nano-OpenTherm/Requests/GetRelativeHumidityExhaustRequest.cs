using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the relative humidity (%RH) of the exhaust air stream.
    /// </summary>
    public class GetRelativeHumidityExhaustRequest : ReadRequest
    {
        #region Public Constructors

        public GetRelativeHumidityExhaustRequest() : base()
        {
        }

        public GetRelativeHumidityExhaustRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.RHexhaust;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(0);

        protected override void SetRawDataCore(uint value)
        { }

        #endregion Protected Methods
    }
}