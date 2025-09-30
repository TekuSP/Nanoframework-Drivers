using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the exhaust fan speed of the ventilation unit.
    /// </summary>
    /// <remarks>
    /// Response payload: low 16 bits as unsigned RPM.
    /// </remarks>
    public class GetExhaustFanSpeedRequest : ReadRequest
    {
        #region Public Constructors

        public GetExhaustFanSpeedRequest() : base()
        {
        }

        public GetExhaustFanSpeedRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.ExhaustFanSpeedResponse);
        public override MessageID MessageID => MessageID.RPMexhaust;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(0);

        protected override void SetRawDataCore(uint value)
        { }

        #endregion Protected Methods
    }
}