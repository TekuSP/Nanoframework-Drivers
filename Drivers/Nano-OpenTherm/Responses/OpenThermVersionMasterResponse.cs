using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// OpenTherm protocol version in Master. Packed as BCD major.minor.
    /// </summary>
    public class OpenThermVersionMasterResponse : Response
    {
        #region Public Constructors

        public OpenThermVersionMasterResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public OpenThermVersionMasterResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public byte Major { get; set; }
        public override MessageID MessageID => MessageID.OpenThermVersionMaster;
        public override MessageType MessageType { get; set; }
        public byte Minor { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            ushort payload = Utilities.MakeUShort(Major, Minor);
            return ProcessResponse(payload);
        }

        protected override void SetRawDataCore(uint value)
        {
            Major = Utilities.GetHighByte(value);
            Minor = Utilities.GetLowByte(value);
        }

        #endregion Protected Methods
    }
}