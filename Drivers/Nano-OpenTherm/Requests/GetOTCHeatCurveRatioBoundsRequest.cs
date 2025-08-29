using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetOTCHeatCurveRatioBoundsRequest : ReadRequest
    {
        public GetOTCHeatCurveRatioBoundsRequest() : base() { }
        public GetOTCHeatCurveRatioBoundsRequest(Request baseReq) : base(baseReq) { }

        public override MessageID MessageID => MessageID.OTCHCRatioBounds;
        public override MessageType MessageType => MessageType.READ_DATA;

        public byte UpperBound { get; protected set; }
        public byte LowerBound { get; protected set; }

        protected override uint GetRawDataCore()
        {
            ushort payload = Utilities.MakeUShort(UpperBound, LowerBound);
            return ProcessRequest(payload);
        }

        protected override void SetRawDataCore(uint value)
        {
            UpperBound = Utilities.GetHighByte(value);
            LowerBound = Utilities.GetLowByte(value);
        }
    }
}
