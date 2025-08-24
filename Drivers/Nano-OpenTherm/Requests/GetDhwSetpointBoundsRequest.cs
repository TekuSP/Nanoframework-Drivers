using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// DHW Setpoint upper & lower bounds for adjustment (°C)
    /// </summary>
    public class GetDhwSetpointBoundsRequest : ReadRequest
    {
        public GetDhwSetpointBoundsRequest() : base() { }
        public GetDhwSetpointBoundsRequest(Request baseReq) : base(baseReq) { }

        public byte UpperBound { get; private set; }
        public byte LowerBound { get; private set; }

        protected override ulong GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(ulong value)
        {
            UpperBound = Utilities.GetHighByte(value);
            LowerBound = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.TdhwSetUBTdhwSetLB;
    }
}
