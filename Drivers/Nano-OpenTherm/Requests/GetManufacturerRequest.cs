using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetManufacturerRequest : ReadRequest
    {
        public GetManufacturerRequest() : base() { }
        public GetManufacturerRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Index of character to read from the brand text (0-based).
    /// This value is placed in the low byte of the request payload.
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
