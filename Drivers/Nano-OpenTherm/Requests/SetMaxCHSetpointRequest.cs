using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets the maximum CH water setpoint (°C). Remote parameter 2.
    /// </summary>
    public class SetMaxCHSetpointRequest : WriteRequest
    {
        #region Private Fields

        private Temperature _temperature;

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

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.MaxTSetResponse);

        public override MessageID MessageID => MessageID.MaxTSet;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// Maximum allowed CH water temperature in °C (encoded as 8.8 fixed-point in low 16 bits). Value is clamped to 0–100.
        /// </summary>
        public Temperature Temperature
        {
            get => _temperature;
            set => _temperature = Temperature.FromDegreesCelsius(((float)value.DegreesCelsius).Normalize());
        }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Utilities.GetRawF88((float)Temperature.DegreesCelsius, 0, 100));

        protected override void SetRawDataCore(uint value) => Temperature = Temperature.FromDegreesCelsius(Utilities.GetFloat(value));

        #endregion Protected Methods
    }
}