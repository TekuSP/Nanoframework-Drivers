using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads a character from the manufacturer/brand version string by index.
    /// </summary>
    public class GetManufacturerVersionRequest : ReadRequest
    {
        public GetManufacturerVersionRequest() : base() { }
        public GetManufacturerVersionRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Index of the character to read (0-based, low byte of request payload).
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
