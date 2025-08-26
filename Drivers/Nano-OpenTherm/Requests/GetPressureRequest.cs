using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the central heating circuit pressure (bar) from the slave device.
    /// </summary>
    public class GetPressureRequest : ReadRequest
    {
        public GetPressureRequest() : base() { }
        public GetPressureRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }
        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.CHPressure;
    }
}
