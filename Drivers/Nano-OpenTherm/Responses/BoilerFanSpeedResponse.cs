using System;
using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Boiler fan speed setpoint (HB) and actual (LB) as raw u8 bytes.
    /// OpenTherm 2.3b Data-ID 35 defines HB/LB bytes; OTGW labels this as fan speed/setpoint. Some boilers map to rpm elsewhere.
    /// </summary>
    public class BoilerFanSpeedResponse : Response
    {
        public BoilerFanSpeedResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public BoilerFanSpeedResponse(Response r) : base(r) { }

    public byte SetpointByte { get; set; }
    public byte ActualByte { get; set; }

        protected override uint GetRawDataCore()
        {
            ushort payload = Utilities.MakeUShort(SetpointByte, ActualByte);
            return ProcessResponse(payload);
        }

        protected override void SetRawDataCore(uint value)
        {
            SetpointByte = Utilities.GetHighByte(value);
            ActualByte = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.BoilerFanSpeedSetpointAndActual;
    }
}
// Note: OpenTherm 2.3b Data-ID 35 uses HB/LB bytes, not f8.8.
