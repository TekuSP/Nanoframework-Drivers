using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// The implemented version of the OpenTherm Protocol Specification in the slave
    /// </summary>
    public class GetOpenThermVersionSlaveRequest : ReadRequest
    {
        #region Public Constructors

        public GetOpenThermVersionSlaveRequest() : base()
        {
        }

        public GetOpenThermVersionSlaveRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// OpenTherm major version number supported by the slave (high byte).
        /// </summary>
        public byte Major { get; set; }

        public override MessageID MessageID => MessageID.OpenThermVersionSlave;

        public override MessageType MessageType => MessageType.READ_DATA;

        /// <summary>
        /// OpenTherm minor version number supported by the slave (low byte).
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