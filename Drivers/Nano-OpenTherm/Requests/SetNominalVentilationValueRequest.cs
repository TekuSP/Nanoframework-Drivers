using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets the nominal relative ventilation value (0–100%).
    /// Wire format: U8 in low byte (per project decision for ID87).
    /// </summary>
    public class SetNominalVentilationValueRequest : WriteRequest
    {
        #region Private Fields

    private byte _percent;

        #endregion Private Fields

        #region Public Constructors

        public SetNominalVentilationValueRequest() : base()
        {
        }

        public SetNominalVentilationValueRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.NominalVentilationValue;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// Nominal relative ventilation value in % (U8 in low byte). Value is clamped to 0–100.
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