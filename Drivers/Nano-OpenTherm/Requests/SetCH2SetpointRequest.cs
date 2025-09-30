using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets the control setpoint for the second CH circuit (°C).
    /// </summary>
    public class SetCH2SetpointRequest : WriteRequest
    {
        #region Public Constructors

        public SetCH2SetpointRequest() : base()
        {
        }

        public SetCH2SetpointRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.ControlSetpointCH2Response);

        public override MessageID MessageID => MessageID.TsetCH2;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// Desired CH2 water temperature in °C (encoded as 8.8 fixed-point in low 16 bits).
        /// </summary>
        public Temperature Temperature { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Utilities.GetRawF88((float)Temperature.DegreesCelsius, 0, 100));

        protected override void SetRawDataCore(uint value) => Temperature = Temperature.FromDegreesCelsius(Utilities.GetFloat(value));

        #endregion Protected Methods
    }
}