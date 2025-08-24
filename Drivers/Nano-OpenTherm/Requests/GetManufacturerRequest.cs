using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetManufacturerRequest : ReadRequest
    {
        public GetManufacturerRequest() : base() { }
        public GetManufacturerRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Index of character to read from brand text (per OpenTherm spec)
        /// </summary>
        public byte Index { get; set; }

        protected override ulong GetRawDataCore() => ProcessRequest(Index);
        protected override void SetRawDataCore(ulong value)
        {
            // Populate Index from incoming raw payload (low byte)
            Index = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.Brand;
    }
}
