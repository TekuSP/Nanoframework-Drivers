using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetVentilationTSPRequest : ReadRequest
    {
        public GetVentilationTSPRequest() : base() { }
        public GetVentilationTSPRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Index number of the transparent ventilation parameter to read (low byte).
        /// </summary>
        public byte Index { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Index);
        protected override void SetRawDataCore(uint value) { Index = Utilities.GetLowByte(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.TSPindexTSPvalueVentilationHeatRecovery;
    }
}
