using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads a specific fault-history buffer entry by index from the slave.
    /// </summary>
    public class GetFaultHistoryBufferEntryRequest : ReadRequest
    {
        public GetFaultHistoryBufferEntryRequest() : base() { }
        public GetFaultHistoryBufferEntryRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Index number of fault-history buffer entry to read (low byte).
        /// </summary>
        public byte Index { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Index);
        protected override void SetRawDataCore(uint value)
        {
            Index = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.FHBindexFHBvalue;
    }
}
