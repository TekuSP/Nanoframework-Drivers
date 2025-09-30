using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class YearResponse : Response
    {
        #region Public Constructors

        public YearResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public YearResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Year;
        public override MessageType MessageType { get; set; }
        public ushort Year { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessResponse(Year);

        protected override void SetRawDataCore(uint value) => Year = Utilities.GetLowUShort(value);

        #endregion Protected Methods
    }
}