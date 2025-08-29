using System;
using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Boiler fan speed setpoint (HB) and current (LB) as percentages (0..100).
    /// </summary>
    public class BoilerFanSpeedResponse : Response
    {
        public BoilerFanSpeedResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public BoilerFanSpeedResponse(Response r) : base(r) { }

        public float SetpointPercent { get; set; }
        public float ActualPercent { get; set; }

        protected override uint GetRawDataCore()
        {
            // Store integer 0..100 in bytes using Utilities helpers
            ushort payload = 0;
            payload = Utilities.SetHighByte(payload, (byte)Utilities.Normalize(SetpointPercent));
            payload = Utilities.SetLowByte(payload, (byte)Utilities.Normalize(ActualPercent));
            return ProcessResponse(payload);
        }

        protected override void SetRawDataCore(uint value)
        {
            SetpointPercent = Utilities.GetHighByte(value);
            ActualPercent = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.BoilerFanSpeedSetpointAndActual;
    }
}
// Note: OpenTherm 2.3b Data-ID 35 uses HB/LB bytes, not f8.8; percent is represented as integer 0..100.
