using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets the maximum CH water setpoint (°C). Remote parameter 2.
    /// </summary>
    public class SetMaxCHSetpointRequest : WriteRequest
    {
        #region Private Fields

        private float _temperature;

        #endregion Private Fields

        #region Public Constructors

        public SetMaxCHSetpointRequest() : base()
        {
        }

        public SetMaxCHSetpointRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.MaxTSet;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// Maximum allowed CH water temperature in °C (encoded as 8.8 fixed-point in low 16 bits). Value is clamped to 0–100.
        /// </summary>
        public float Temperature
        {
            get => _temperature;
            set => _temperature = value.Normalize();
        }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Utilities.GetRawTemperature(Temperature));

        protected override void SetRawDataCore(uint value) => Temperature = Utilities.GetFloat(value);

        #endregion Protected Methods
    }
}