using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetBurnerOperationHoursRequest : ReadRequest
    {
        public GetBurnerOperationHoursRequest() : base() { }
        public GetBurnerOperationHoursRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Total burner operation hours (encoded in the low 16 bits).
    /// Units: hours.
    /// </summary>
    /// <summary>
    /// Total burner operation hours, encoded as a 16-bit unsigned integer (OpenTherm ID 116, low 16 bits).
    /// Units: hours.
    /// </summary>
    public ushort Hours { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Hours);
        protected override void SetRawDataCore(uint value) { Hours = Utilities.GetLowUShort(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.BurnerOperationHours;
    }
}
