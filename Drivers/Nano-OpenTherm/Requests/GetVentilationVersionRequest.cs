using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Ventilation / heat-recovery product version number and type
    /// </summary>
    public class GetVentilationVersionRequest : ReadRequest
    {
        public GetVentilationVersionRequest() : base() { }
        public GetVentilationVersionRequest(Request baseReq) : base(baseReq) { }

        public byte Version { get; set; }
        public byte Type { get; set; }

        protected override ulong GetRawDataCore()
        {
            uint raw = (uint)((Version << 8) | Type);
            return ProcessRequest(raw);
        }
        protected override void SetRawDataCore(ulong value)
        {
            Version = Utilities.GetHighByte(value);
            Type = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.VentilationHeatRecoveryVersion;
    }
}
