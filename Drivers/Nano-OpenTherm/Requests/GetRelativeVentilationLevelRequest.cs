using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the relative ventilation level of the ventilation unit.
    /// </summary>
    /// <remarks>
    /// Response payload: low 16 bits as 8.8 fixed-point percent (0–100%).
    /// </remarks>
    public class GetRelativeVentilationLevelRequest : ReadRequest
    {
        #region Public Constructors

        public GetRelativeVentilationLevelRequest() : base()
        {
        }

        public GetRelativeVentilationLevelRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.RelVentLevel;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(0);

        protected override void SetRawDataCore(uint value)
        { }

        #endregion Protected Methods
    }
}