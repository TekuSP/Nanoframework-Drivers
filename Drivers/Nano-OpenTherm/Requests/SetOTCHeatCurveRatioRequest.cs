using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class SetOTCHeatCurveRatioRequest : WriteRequest
    {
        #region Public Constructors

        public SetOTCHeatCurveRatioRequest() : base()
        {
        }

        public SetOTCHeatCurveRatioRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// OTC heat curve factor to write (0..40, F8.8). Dimensionless slope-like factor.
        /// </summary>
        public double CurveFactor { get; set; }

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.OTCHeatCurveRatioResponse);

        public override MessageID MessageID => MessageID.OTCHeatCurveRatio;
        public override MessageType MessageType => MessageType.WRITE_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            var v = CurveFactor;
            if (v < 0) v = 0; else if (v > 40) v = 40;
            return ProcessRequest(Utilities.GetRawF88((float)v, 0, 40));
        }

        protected override void SetRawDataCore(uint value)
        {
            var v = Utilities.GetFloat(value);
            if (v < 0) v = 0; else if (v > 40) v = 40;
            CurveFactor = v;
        }

        #endregion Protected Methods
    }
}