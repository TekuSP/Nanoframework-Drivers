using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetFaultHistoryBufferEntryRequest : ReadRequest
    {
        public GetFaultHistoryBufferEntryRequest() : base() { }
        public GetFaultHistoryBufferEntryRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Index number of fault-history buffer entry to read
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
