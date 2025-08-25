using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Boiler fan speed Setpoint and actual value
    /// </summary>
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
