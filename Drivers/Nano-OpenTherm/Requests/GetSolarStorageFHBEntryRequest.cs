using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads an entry from the Solar Storage Fault History Buffer (FHB).
    /// </summary>
    /// <remarks>
    /// The <see cref="Index"/> is sent in the low byte of the payload to select
    /// which FHB entry to read. The response encodes the entry value in the low 16 bits
    /// (OEM-specific format).
    /// </remarks>
    public class GetSolarStorageFHBEntryRequest : ReadRequest
    {
        public GetSolarStorageFHBEntryRequest() : base() { }
        public GetSolarStorageFHBEntryRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Fault history buffer index to read (0-based). Encoded in the low byte.
        /// </summary>
        public byte Index { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Index);
        protected override void SetRawDataCore(uint value) { Index = Utilities.GetLowByte(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.FHBindexFHBvalueSolarStorage;
    }
}
