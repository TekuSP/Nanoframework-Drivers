using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of burner starts during Domestic Hot Water (DHW) mode.
    /// </summary>
    public class GetDHWBurnerStartsRequest : ReadRequest
    {
        public GetDHWBurnerStartsRequest() : base() { }
        public GetDHWBurnerStartsRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Number of burner starts during domestic hot water (DHW) mode (encoded in the low 16 bits).
    /// Units: count.
    /// </summary>
    public ushort Count { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Count);
        protected override void SetRawDataCore(uint value) { Count = Utilities.GetLowUShort(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.DHWBurnerStarts;
    }
}
