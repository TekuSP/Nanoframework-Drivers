using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets the domestic hot water (DHW) setpoint temperature (°C).
    /// </summary>
    public class SetDWHSetPointRequest : WriteRequest
    {
        #region Private Fields

        private float _temperature;

        #endregion Private Fields

        #region Public Constructors

        public SetDWHSetPointRequest() : base()
        {
        }

        public SetDWHSetPointRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.TdhwSet;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// DHW setpoint in °C (encoded as 8.8 fixed-point in the low 16 bits). Value is normalized/clamped to valid range.
        /// </summary>
        public float Temperature
        {
            get => _temperature;
            set => _temperature = value.Normalize();
        }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            return ProcessRequest(Utilities.GetRawTemperature(Temperature));
        }

        protected override void SetRawDataCore(uint value)
        {
            Temperature = Utilities.GetFloat(value);
        }

        #endregion Protected Methods
    }
}