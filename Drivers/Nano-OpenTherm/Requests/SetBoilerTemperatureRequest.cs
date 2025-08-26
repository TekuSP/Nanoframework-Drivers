using System;
using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets the boiler water setpoint temperature (°C).
    /// </summary>
    public class SetBoilerTemperatureRequest : WriteRequest
    {
        public SetBoilerTemperatureRequest() : base() { }
        public SetBoilerTemperatureRequest(Request baseReq) : base(baseReq) { }

        private float _temperature;
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

        protected override uint GetRawDataCore()
        {
            return ProcessRequest(Utilities.GetRawTemperature(Temperature));
        }
        protected override void SetRawDataCore(uint value)
        {
            Temperature = Utilities.GetFloat(value);
        }

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.TSet;
    }
}
