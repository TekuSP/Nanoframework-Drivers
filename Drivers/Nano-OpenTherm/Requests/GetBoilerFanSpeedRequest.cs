using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the boiler fan speed setpoint and actual value.
    /// </summary>
    /// <remarks>
    /// Response payload: high/low byte mapping is device-specific (commonly setpoint in high byte and actual in low byte as RPM).
    /// </remarks>
    public class GetBoilerFanSpeedRequest : ReadRequest
    {
        public GetBoilerFanSpeedRequest() : base() { }
        public GetBoilerFanSpeedRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }
        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.BoilerFanSpeedSetpointAndActual;
    }
}
