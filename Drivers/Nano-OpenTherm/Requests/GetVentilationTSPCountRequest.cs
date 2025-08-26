using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of transparent ventilation parameters supported.
    /// </summary>
    public class GetVentilationTSPCountRequest : ReadRequest
    {
        public GetVentilationTSPCountRequest() : base() { }
        public GetVentilationTSPCountRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.TSPventilationHeatRecovery;
    }
}
