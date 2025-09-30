using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// The implemented version of the OpenTherm Protocol Specification in the master
    /// </summary>
    public class GetOpenThermVersionMasterRequest : ReadRequest
    {
        #region Public Constructors

        public GetOpenThermVersionMasterRequest() : base()
        {
        }

        public GetOpenThermVersionMasterRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.OpenThermVersionMasterResponse);

        /// <summary>
        /// OpenTherm major version number supported by the master (high byte).
        /// </summary>
        public byte Major { get; set; }

        public override MessageID MessageID => MessageID.OpenThermVersionMaster;

        public override MessageType MessageType => MessageType.READ_DATA;

        /// <summary>
        /// OpenTherm minor version number supported by the master (low byte).
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