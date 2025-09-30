using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class OTCHeatCurveRatioResponse : Response
    {
        #region Public Constructors

        public OTCHeatCurveRatioResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public OTCHeatCurveRatioResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Outdoor temperature compensation heat curve factor (ID58). Raw F8.8 value, engineering range 0..40.
        /// This is a dimensionless slope-like factor, NOT a percentage. Expressed directly as a double.
        /// </summary>
        public double CurveFactor { get; set; }

        public override MessageID MessageID => MessageID.OTCHeatCurveRatio;

        public override MessageType MessageType { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            // Clamp to spec range before encoding.
            var v = CurveFactor;
            if (v < 0) v = 0; else if (v > 40) v = 40;
            return ProcessResponse(Utilities.GetRawF88((float)v, 0, 40));
        }

        protected override void SetRawDataCore(uint value)
        {
            var v = Utilities.GetFloat(value);
            if (v < 0) v = 0; else if (v > 40) v = 40; // defensive clamp
            CurveFactor = v;
        }

        #endregion Protected Methods
    }
}