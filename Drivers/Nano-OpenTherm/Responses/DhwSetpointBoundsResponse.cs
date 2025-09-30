using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// DHW Setpoint Upper/Lower Bounds (°C) in a single payload.
    /// </summary>
    public class DhwSetpointBoundsResponse : Response
    {
        #region Public Constructors

        public DhwSetpointBoundsResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public DhwSetpointBoundsResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public sbyte LowerBoundC { get; set; }
        public override MessageID MessageID => MessageID.TdhwSetUBTdhwSetLB;
        public override MessageType MessageType { get; set; }
        public sbyte UpperBoundC { get; set; }

        #endregion Public Properties

        #region Protected Methods

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

        #endregion Protected Methods
    }
}