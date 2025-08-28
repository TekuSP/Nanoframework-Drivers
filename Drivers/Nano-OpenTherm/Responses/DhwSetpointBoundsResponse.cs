using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// DHW Setpoint Upper/Lower Bounds (°C) in a single payload.
    /// </summary>
    public class DhwSetpointBoundsResponse : Response
    {
        public DhwSetpointBoundsResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public DhwSetpointBoundsResponse(Response r) : base(r) { }

        public byte UpperBoundC { get; set; }
        public byte LowerBoundC { get; set; }

        protected override uint GetRawDataCore()
        {
            ushort payload = Utilities.MakeUShort(UpperBoundC, LowerBoundC);
            return ProcessResponse(payload);
        }
        protected override void SetRawDataCore(uint value)
        {
            UpperBoundC = Utilities.GetHighByte(value);
            LowerBoundC = Utilities.GetLowByte(value);
        }
        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.TdhwSetUBTdhwSetLB;
    }
}
