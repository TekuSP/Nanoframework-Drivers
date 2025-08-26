using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetFlameSignalTooLowNumberRequest : ReadRequest
    {
        public GetFlameSignalTooLowNumberRequest() : base() { }
        public GetFlameSignalTooLowNumberRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Number of times the flame signal was too low (low 16 bits, unsigned).
    /// Units: count.
    /// </summary>
    public ushort Count { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Count);
        protected override void SetRawDataCore(uint value) { Count = Utilities.GetLowUShort(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.FlameSignalTooLowNumber;
    }
}
