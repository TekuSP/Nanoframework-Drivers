using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

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

        public override MessageID MessageID => MessageID.TsetCH2;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// Desired CH2 water temperature in °C (encoded as 8.8 fixed-point in low 16 bits).
        /// </summary>
        public float Temperature { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Utilities.GetRawTemperature(Temperature));

        protected override void SetRawDataCore(uint value) => Temperature = Utilities.GetFloat(value);

        #endregion Protected Methods
    }
}