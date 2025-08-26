using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets the maximum CH water setpoint (°C). Remote parameter 2.
    /// </summary>
    public class SetMaxCHSetpointRequest : WriteRequest
    {
        public SetMaxCHSetpointRequest() : base() { }
        public SetMaxCHSetpointRequest(Request baseReq) : base(baseReq) { }

        private float _temperature;
        /// <summary>
        /// Maximum allowed CH water temperature in °C (encoded as 8.8 fixed-point in low 16 bits). Value is clamped to 0–100.
        /// </summary>
        public float Temperature
        {
            get => _temperature;
            set => _temperature = value.Normalize();
        }

        protected override uint GetRawDataCore() => ProcessRequest(Utilities.GetRawTemperature(Temperature));
        protected override void SetRawDataCore(uint value) => Temperature = Utilities.GetFloat(value);

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.MaxTSet;
    }
}
