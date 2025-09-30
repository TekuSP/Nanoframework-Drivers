using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the CO₂ concentration in the exhaust air.
    /// </summary>
    /// <remarks>
    /// Response payload: low 16 bits as 8.8 fixed-point percent or ppm depending on device; consult vendor docs.
    /// </remarks>
    public class GetCO2ExhaustRequest : ReadRequest
    {
        #region Public Constructors

        public GetCO2ExhaustRequest() : base()
        {
        }

        public GetCO2ExhaustRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.CO2ExhaustResponse);

        public override MessageID MessageID => MessageID.CO2exhaust;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(0);

        protected override void SetRawDataCore(uint value)
        { }

        #endregion Protected Methods
    }
}