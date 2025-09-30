using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetOTCHeatCurveRatioBoundsRequest : ReadRequest
    {
        #region Public Constructors

        public GetOTCHeatCurveRatioBoundsRequest() : base()
        {
        }

        public GetOTCHeatCurveRatioBoundsRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.OTCHCRatioBoundsResponse);
        public byte LowerBound { get; protected set; }
        public override MessageID MessageID => MessageID.OTCHCRatioBounds;
        public override MessageType MessageType => MessageType.READ_DATA;

        public byte UpperBound { get; protected set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            ushort payload = Utilities.MakeUShort(UpperBound, LowerBound);
            return ProcessRequest(payload);
        }

        protected override void SetRawDataCore(uint value)
        {
            UpperBound = Utilities.GetHighByte(value);
            LowerBound = Utilities.GetLowByte(value);
        }

        #endregion Protected Methods
    }
}