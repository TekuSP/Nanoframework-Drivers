using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetSerialRequest : ReadRequest
    {
        public GetSerialRequest() : base() { }
        public GetSerialRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Index of character to read from serial number string
        /// </summary>
        public byte Index { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Index);
        protected override void SetRawDataCore(uint value)
        {
            Index = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.BrandSerialNumber;
    }
}
