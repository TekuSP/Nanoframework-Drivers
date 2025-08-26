using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads a ventilation fault-history buffer entry by index.
    /// </summary>
    /// <remarks>
    /// The <see cref="Index"/> is encoded in the low byte. The response payload contains the entry value
    /// in the low 16 bits (OEM-specific encoding).
    /// </remarks>
    public class GetVentilationFHBEntryRequest : ReadRequest
    {
        public GetVentilationFHBEntryRequest() : base() { }
        public GetVentilationFHBEntryRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Index of ventilation fault-history entry to read (0-based). Encoded in the low byte.
        /// </summary>
        public byte Index { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Index);
        protected override void SetRawDataCore(uint value) { Index = Utilities.GetLowByte(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.FHBindexFHBvalueVentilationHeatRecovery;
    }
}
