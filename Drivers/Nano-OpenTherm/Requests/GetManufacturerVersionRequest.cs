using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetManufacturerVersionRequest : ReadRequest
    {
        public GetManufacturerVersionRequest() : base() { }
        public GetManufacturerVersionRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Index of character to read from the brand version string (0-based).
    /// Encoded in the low byte of the request data.
    /// </summary>
    public byte Index { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Index);
        protected override void SetRawDataCore(uint value)
        {
            Index = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.BrandVersion;
    }
}
