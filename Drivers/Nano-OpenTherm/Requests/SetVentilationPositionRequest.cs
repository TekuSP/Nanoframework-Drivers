using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets the relative ventilation position (0–100%).
    /// </summary>
    public class SetVentilationPositionRequest : WriteRequest
    {
        #region Private Fields

        private float _percent;

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
        /// Relative ventilation position in % (encoded as 8.8 fixed-point in low 16 bits). Value is clamped to 0–100.
        /// </summary>
        public float Percent
        {
            get => _percent;
            set => _percent = value.Normalize();
        }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            return ProcessRequest(Utilities.GetRawPercentage(Percent));
        }

        protected override void SetRawDataCore(uint value)
        {
            Percent = Utilities.GetPercentage(value);
        }

        #endregion Protected Methods
    }
}