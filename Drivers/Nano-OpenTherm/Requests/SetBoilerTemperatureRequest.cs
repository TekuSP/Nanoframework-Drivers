using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets the boiler water setpoint temperature (°C).
    /// </summary>
    public class SetBoilerTemperatureRequest : WriteRequest
    {
        #region Private Fields

        private Temperature _temperature;

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

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.ControlSetpointResponse);
        public override MessageID MessageID => MessageID.TSet;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// Boiler water setpoint in °C.
        /// Encoded as 8.8 fixed-point in the low 16 bits of the frame.
        /// Values are normalized/clamped to the valid OpenTherm range.
        /// </summary>
        public Temperature Temperature
        {
            get => _temperature;
            set => _temperature = Temperature.FromDegreesCelsius(((float)value.DegreesCelsius).Normalize());
        }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            return ProcessRequest(Utilities.GetRawF88((float)Temperature.DegreesCelsius, 0, 100));
        }

        protected override void SetRawDataCore(uint value)
        {
            Temperature = Temperature.FromDegreesCelsius(Utilities.GetFloat(value));
        }

        #endregion Protected Methods
    }
}