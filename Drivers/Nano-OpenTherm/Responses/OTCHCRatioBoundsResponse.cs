using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class OTCHCRatioBoundsResponse : Response
    {
        #region Public Constructors

        public OTCHCRatioBoundsResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public OTCHCRatioBoundsResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public sbyte LowerBound { get; set; }
        public override MessageID MessageID => MessageID.OTCHCRatioBounds;
        public override MessageType MessageType { get; set; }
        public sbyte UpperBound { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            ushort payload = Utilities.MakeUShort((byte)UpperBound, (byte)LowerBound);
            return ProcessResponse(payload);
        }

        protected override void SetRawDataCore(uint value)
        {
            UpperBound = (sbyte)Utilities.GetHighByte(value);
            LowerBound = (sbyte)Utilities.GetLowByte(value);
        }

        #endregion Protected Methods
    }
}