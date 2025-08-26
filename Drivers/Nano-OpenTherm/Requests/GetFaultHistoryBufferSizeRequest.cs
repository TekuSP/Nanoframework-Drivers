using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of entries available in the slave's fault-history buffer.
    /// </summary>
    public class GetFaultHistoryBufferSizeRequest : ReadRequest
    {
        public GetFaultHistoryBufferSizeRequest() : base() { }
        public GetFaultHistoryBufferSizeRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.FHBsize;
    }
}
