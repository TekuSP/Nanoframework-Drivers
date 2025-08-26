using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads a character from the manufacturer/brand name by index.
    /// </summary>
    public class GetManufacturerRequest : ReadRequest
    {
        public GetManufacturerRequest() : base() { }
        public GetManufacturerRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Index of the character to read from the brand text (0-based, low byte).
    /// </summary>
    public byte Index { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Index);
        protected override void SetRawDataCore(uint value)
        {
            // Populate Index from incoming raw payload (low byte)
            Index = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.Brand;
    }
}
