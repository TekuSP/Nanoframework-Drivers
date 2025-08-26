using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of unsuccessful burner start attempts recorded by the slave.
    /// </summary>
    public class GetUnsuccessfulBurnerStartsRequest : ReadRequest
    {
        public GetUnsuccessfulBurnerStartsRequest() : base() { }
        public GetUnsuccessfulBurnerStartsRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Number of burner start attempts that failed (low 16 bits, unsigned).
        /// Units: count.
        /// </summary>
        public ushort Count { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Count);
        protected override void SetRawDataCore(uint value) { Count = Utilities.GetLowUShort(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.UnsuccessfulBurnerStarts;
    }
}
