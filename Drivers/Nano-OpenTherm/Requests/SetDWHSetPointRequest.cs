using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets the domestic hot water (DHW) setpoint temperature (°C).
    /// </summary>
    public class SetDWHSetPointRequest : WriteRequest
    {
        #region Private Fields

        private Temperature _temperature;

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

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.DhwSetpointResponse);

        public override MessageID MessageID => MessageID.TdhwSet;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// DHW setpoint in °C (encoded as 8.8 fixed-point in the low 16 bits). Value is normalized/clamped to valid range.
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