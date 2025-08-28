using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Remote Override Room Setpoint 2 (8.8 fixed-point temperature in °C).
    /// </summary>
    public class RemoteOverrideRoomSetPoint2Response : Response
    {
        public RemoteOverrideRoomSetPoint2Response(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public RemoteOverrideRoomSetPoint2Response(Response r) : base(r) { }

        /// <summary>Override room setpoint 2 in Celsius.</summary>
        public float TrOverride2 { get; set; }

        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawTemperature(TrOverride2));
        protected override void SetRawDataCore(uint value) => TrOverride2 = Utilities.GetFloat(value);

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.TrOverride2;
    }
}
