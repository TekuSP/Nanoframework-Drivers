using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the current domestic hot water (DHW) temperature (°C).
    /// </summary>
    public class GetDHWTemperatureRequest : ReadRequest
    {
        public GetDHWTemperatureRequest() : base() { }
        public GetDHWTemperatureRequest(Request baseReq) : base(baseReq) { }

        public override MessageID MessageID => MessageID.Tdhw;
        public override MessageType MessageType => MessageType.READ_DATA;

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }
    }
}
