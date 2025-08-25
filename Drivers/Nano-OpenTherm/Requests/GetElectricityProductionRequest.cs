using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetElectricityProductionRequest : ReadRequest
    {
        public GetElectricityProductionRequest() : base() { }
        public GetElectricityProductionRequest(Request baseReq) : base(baseReq) { }

        public ushort Watts { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Watts);
        protected override void SetRawDataCore(uint value) { Watts = Utilities.GetLowUShort(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.ElectricityProduction;
    }
}
