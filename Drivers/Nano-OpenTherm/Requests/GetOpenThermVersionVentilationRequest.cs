using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// OT version implemented in the ventilation / heat recovery system
    /// </summary>
    public class GetOpenThermVersionVentilationRequest : ReadRequest
    {
        #region Public Constructors

        public GetOpenThermVersionVentilationRequest() : base()
        {
        }

        public GetOpenThermVersionVentilationRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

    public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.VentilationOpenThermVersionResponse);

        /// <summary>
        /// OpenTherm major version number (high byte).
        /// </summary>
        public byte Major { get; set; }

        public override MessageID MessageID => MessageID.OpenThermVersionVentilationHeatRecovery;

        public override MessageType MessageType => MessageType.READ_DATA;

        /// <summary>
        /// OpenTherm minor version number (low byte).
        /// </summary>
        public byte Minor { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            ushort payload = Utilities.MakeUShort(Major, Minor);
            return ProcessRequest(payload);
        }

        protected override void SetRawDataCore(uint value)
        {
            Major = Utilities.GetHighByte(value);
            Minor = Utilities.GetLowByte(value);
        }

        #endregion Protected Methods
    }
}