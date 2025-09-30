using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>OpenTherm protocol version in Ventilation/HR unit (BCD major.minor in bytes).</summary>
    public class VentilationOpenThermVersionResponse : Response
    {
        #region Public Constructors

        public VentilationOpenThermVersionResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public VentilationOpenThermVersionResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public byte Major { get; set; }
        public override MessageID MessageID => MessageID.OpenThermVersionVentilationHeatRecovery;
        public override MessageType MessageType { get; set; }
        public byte Minor { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
            => ProcessResponse(Utilities.MakeUShort(Major, Minor));

        protected override void SetRawDataCore(uint value)
        { Major = Utilities.GetHighByte(value); Minor = Utilities.GetLowByte(value); }

        #endregion Protected Methods
    }
}