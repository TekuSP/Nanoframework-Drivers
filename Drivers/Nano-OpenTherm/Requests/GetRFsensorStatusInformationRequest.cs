using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetRFsensorStatusInformationRequest : ReadRequest
    {
        public GetRFsensorStatusInformationRequest() : base() { }
        public GetRFsensorStatusInformationRequest(Request baseReq) : base(baseReq) { }

        public byte SensorId { get; set; }

        protected override ulong GetRawDataCore() => ProcessRequest(SensorId);
        protected override void SetRawDataCore(ulong value) { SensorId = Utilities.GetLowByte(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.RFsensorStatusInformation;
    }
}
