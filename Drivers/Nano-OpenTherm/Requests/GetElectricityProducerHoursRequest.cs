using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetElectricityProducerHoursRequest : ReadRequest
    {
        public GetElectricityProducerHoursRequest() : base() { }
        public GetElectricityProducerHoursRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Total operation hours of the electricity producer (encoded in the low 16 bits).
    /// </summary>
    public ushort Hours { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Hours);
        protected override void SetRawDataCore(uint value) { Hours = Utilities.GetLowUShort(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.ElectricityProducerHours;
    }
}
