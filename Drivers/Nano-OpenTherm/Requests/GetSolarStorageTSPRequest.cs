using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads a Transparent Slave Parameter (TSP) value by index from the Solar Storage.
    /// </summary>
    /// <remarks>
    /// The <see cref="Index"/> is encoded in the low byte of the request payload. The response
    /// carries the TSP value in the low 16 bits (encoding is parameter-specific).
    /// </remarks>
    public class GetSolarStorageTSPRequest : ReadRequest
    {
        public GetSolarStorageTSPRequest() : base() { }
        public GetSolarStorageTSPRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// TSP index to read (0-based). Encoded in the low byte.
        /// </summary>
        public byte Index { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Index);
        protected override void SetRawDataCore(uint value) { Index = Utilities.GetLowByte(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.TSPindexTSPvalueSolarStorage;
    }
}
