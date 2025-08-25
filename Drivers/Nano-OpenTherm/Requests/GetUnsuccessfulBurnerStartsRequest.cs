using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetUnsuccessfulBurnerStartsRequest : ReadRequest
    {
        public GetUnsuccessfulBurnerStartsRequest() : base() { }
        public GetUnsuccessfulBurnerStartsRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Number of burner start attempts that failed. Encoded in low 16 bits.
        /// </summary>
        public ushort Count { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Count);
        protected override void SetRawDataCore(uint value) { Count = Utilities.GetLowUShort(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.UnsuccessfulBurnerStarts;
    }
}
