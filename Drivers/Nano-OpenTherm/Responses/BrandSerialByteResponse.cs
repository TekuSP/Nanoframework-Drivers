using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Single byte of brand/serial number in low byte.</summary>
    public class BrandSerialByteResponse : Response
    {
        #region Public Constructors

        public BrandSerialByteResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public BrandSerialByteResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public byte Data { get; set; }
        public override MessageID MessageID => MessageID.BrandSerialNumber;

        public override MessageType MessageType { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessResponse(Data);

        protected override void SetRawDataCore(uint value) => Data = Utilities.GetLowByte(value);

        #endregion Protected Methods
    }
}