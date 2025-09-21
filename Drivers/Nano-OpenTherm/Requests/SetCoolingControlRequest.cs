using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Cooling control signal (%)
    /// </summary>
    public class SetCoolingControlRequest : WriteRequest
    {
        #region Private Fields

        private float _percent;

        #endregion Private Fields

        #region Public Constructors

        public SetCoolingControlRequest() : base()
        {
        }

        public SetCoolingControlRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

    public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.CoolingControlResponse);

        public override MessageID MessageID => MessageID.CoolingControl;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// Cooling control signal in % (encoded as 8.8 fixed-point in low 16 bits). Value is clamped 0..100.
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