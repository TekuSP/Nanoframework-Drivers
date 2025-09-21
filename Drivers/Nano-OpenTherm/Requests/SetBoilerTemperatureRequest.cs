using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets the boiler water setpoint temperature (°C).
    /// </summary>
    public class SetBoilerTemperatureRequest : WriteRequest
    {
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.ControlSetpointResponse);
        #region Private Fields

        private float _temperature;

        #endregion Private Fields

        #region Public Constructors

        public SetBoilerTemperatureRequest() : base()
        {
        }

        public SetBoilerTemperatureRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.TSet;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// Boiler water setpoint in °C.
        /// Encoded as 8.8 fixed-point in the low 16 bits of the frame.
        /// Values are normalized/clamped to the valid OpenTherm range.
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