using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads a byte of the device brand/serial number by index.
    /// </summary>
    /// <remarks>
    /// The <see cref="Index"/> is encoded in the low byte of the request. The response returns
    /// the selected byte in the low 8 bits of the payload (low 16 bits used by some devices).
    /// Iterate index to reconstruct the full identifier.
    /// </remarks>
    public class GetSerialRequest : ReadRequest
    {
        public GetSerialRequest() : base() { }
        public GetSerialRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Index of the serial/brand number byte to read from the device (0-based).
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
