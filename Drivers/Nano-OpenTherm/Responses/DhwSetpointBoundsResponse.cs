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

    public sbyte UpperBoundC { get; set; }
    public sbyte LowerBoundC { get; set; }

        protected override uint GetRawDataCore()
        {
            ushort payload = Utilities.MakeUShort((byte)UpperBoundC, (byte)LowerBoundC);
            return ProcessResponse(payload);
        }
        protected override void SetRawDataCore(uint value)
        {
            UpperBoundC = (sbyte)Utilities.GetHighByte(value);
            LowerBoundC = (sbyte)Utilities.GetLowByte(value);
        }
        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.TdhwSetUBTdhwSetLB;
    }
}
