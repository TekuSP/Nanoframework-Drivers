using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetElectricityProducerHoursRequest : ReadRequest
    {
        public GetElectricityProducerHoursRequest() : base() { }
        public GetElectricityProducerHoursRequest(Request baseReq) : base(baseReq) { }

        public ushort Hours { get; set; }

        protected override ulong GetRawDataCore() => ProcessRequest(Hours);
        protected override void SetRawDataCore(ulong value) { Hours = Utilities.GetLowUShort(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.ElectricityProducerHours;
    }
}
