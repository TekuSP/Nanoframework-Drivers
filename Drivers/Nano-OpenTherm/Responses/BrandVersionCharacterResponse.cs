using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Single character of brand version string in low byte.</summary>
    public class BrandVersionCharacterResponse : Response
    {
        #region Public Constructors

        public BrandVersionCharacterResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public BrandVersionCharacterResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public char Character { get; set; }
        public override MessageID MessageID => MessageID.BrandVersion;

        public override MessageType MessageType { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessResponse(Character);

        protected override void SetRawDataCore(uint value) => Character = (char)Utilities.GetLowByte(value);

        #endregion Protected Methods
    }
}