using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of entries available in the slave's fault-history buffer.
    /// </summary>
    public class GetFaultHistoryBufferSizeRequest : ReadRequest
    {
        #region Public Constructors

        public GetFaultHistoryBufferSizeRequest() : base()
        {
        }

        public GetFaultHistoryBufferSizeRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.FaultHistoryBufferSizeResponse);
        public override MessageID MessageID => MessageID.FHBsize;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(0);

        protected override void SetRawDataCore(uint value)
        { }

        #endregion Protected Methods
    }
}