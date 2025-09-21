using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets the relative ventilation position (0–100%).
    /// Wire format: U8 in low byte (per project decision for ID71).
    /// </summary>
    public class SetVentilationPositionRequest : WriteRequest
    {
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.VentilationPositionResponse);
        #region Private Fields

    private byte _percent;

        #endregion Private Fields

        #region Public Constructors

        public SetVentilationPositionRequest() : base()
        {
        }

        public SetVentilationPositionRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Vset;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// Relative ventilation position in % (U8 in low byte). Value is clamped to 0–100.
        /// </summary>
        public byte Percent
        {
            get => _percent;
            set => _percent = (byte)Utilities.Normalize(value);
        }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            return ProcessRequest(Utilities.MakeUShort(0, Percent));
        }

        protected override void SetRawDataCore(uint value)
        {
            Percent = Utilities.GetLowByte(value);
        }

        #endregion Protected Methods
    }
}