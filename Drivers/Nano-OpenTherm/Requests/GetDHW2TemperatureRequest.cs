using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the secondary domestic hot water (DHW2) temperature.
    /// </summary>
    /// <remarks>
    /// Response payload: low 16 bits as 8.8 fixed-point degrees Celsius.
    /// </remarks>
    public class GetDHW2TemperatureRequest : ReadRequest
    {
        public GetDHW2TemperatureRequest() : base() { }
        public GetDHW2TemperatureRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }
        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.Tdhw2;
    }
}
