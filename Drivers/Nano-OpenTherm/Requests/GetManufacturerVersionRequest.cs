using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetManufacturerVersionRequest : ReadRequest
    {
        public GetManufacturerVersionRequest() : base() { }
        public GetManufacturerVersionRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Index of character to read from brand version string
        /// </summary>
        public byte Index { get; set; }

        protected override ulong GetRawDataCore() => ProcessRequest(Index);
        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.BrandVersion;
    }
}
