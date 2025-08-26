using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetSuccessfulBurnerStartsRequest : ReadRequest
    {
        public GetSuccessfulBurnerStartsRequest() : base() { }
        public GetSuccessfulBurnerStartsRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Number of successful burner starts. Encoded in low 16 bits.
        /// </summary>
        public ushort Count { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Count);
        protected override void SetRawDataCore(uint value) { Count = Utilities.GetLowUShort(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.SuccessfulBurnerStarts;
    }
}
