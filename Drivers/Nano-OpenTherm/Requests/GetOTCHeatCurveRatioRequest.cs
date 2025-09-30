using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetOTCHeatCurveRatioRequest : ReadRequest
    {
        #region Public Constructors

        public GetOTCHeatCurveRatioRequest() : base()
        {
        }

        public GetOTCHeatCurveRatioRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public double CurveFactor { get; protected set; }
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.OTCHeatCurveRatioResponse);
        public override MessageID MessageID => MessageID.OTCHeatCurveRatio;
        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Utilities.GetRawF88((float)CurveFactor, 0, 40));

        protected override void SetRawDataCore(uint value)
        {
            var v = Utilities.GetFloat(value);
            if (v < 0) v = 0; else if (v > 40) v = 40;
            CurveFactor = v;
        }

        #endregion Protected Methods
    }
}