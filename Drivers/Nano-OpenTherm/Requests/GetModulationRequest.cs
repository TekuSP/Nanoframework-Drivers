using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the relative modulation level (%) of the slave.
    /// </summary>
    public class GetModulationRequest : ReadRequest
    {
        public GetModulationRequest() : base() { }
        public GetModulationRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }
        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.RelModLevel;
    }
}
