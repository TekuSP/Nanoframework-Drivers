using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets the nominal relative ventilation value (0–100%).
    /// </summary>
    public class SetNominalVentilationValueRequest : WriteRequest
    {
        #region Private Fields

        private float _percent;

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
        /// Nominal relative ventilation value in % (encoded as 8.8 fixed-point in low 16 bits). Value is clamped to 0–100.
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