using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the size (entry count) of the ventilation fault-history buffer.
    /// </summary>
    /// <remarks>
    /// No selector is required; the size is returned in the low 16 bits of the response payload.
    /// </remarks>
    public class GetVentilationFHBSizeRequest : ReadRequest
    {
        public GetVentilationFHBSizeRequest() : base() { }
        public GetVentilationFHBSizeRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.FHBsizeVentilationHeatRecovery;
    }
}
