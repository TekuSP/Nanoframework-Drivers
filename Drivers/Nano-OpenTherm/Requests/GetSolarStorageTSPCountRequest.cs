using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of Transparent Slave Parameters (TSP) supported by the Solar Storage.
    /// </summary>
    /// <remarks>
    /// The count is returned in the low 16 bits of the response payload.
    /// </remarks>
    public class GetSolarStorageTSPCountRequest : ReadRequest
    {
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.SolarStorageTSPCountResponse);
        #region Public Constructors

        public GetSolarStorageTSPCountRequest() : base()
        {
        }

        public GetSolarStorageTSPCountRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.TSPSolarStorage;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(0);

        protected override void SetRawDataCore(uint value)
        { }

        #endregion Protected Methods
    }
}