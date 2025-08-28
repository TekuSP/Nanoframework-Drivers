using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Boiler fan speed setpoint and actual response, expressed in percent (0–100).
    /// High byte = Setpoint (%), Low byte = Actual (%). Each field maps 1:1 to a data byte.
    /// </summary>
    public class BoilerFanSpeedResponse : Response
    {
        public BoilerFanSpeedResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public BoilerFanSpeedResponse(Response r) : base(r) { }

    public float SetpointPercent { get; set; }
    public float ActualPercent { get; set; }

    protected override uint GetRawDataCore()
    {
        // Clamp to 0..100 and store as single-byte percent fields
        byte setpoint = (byte)SetpointPercent.Normalize();
        byte actual = (byte)ActualPercent.Normalize();
        return ProcessResponse(Utilities.MakeUShort(setpoint, actual));
    }
        protected override void SetRawDataCore(uint value)
        {
            // Interpret each payload byte directly as percent 0..100
            ActualPercent = Utilities.GetLowByte(value);
            SetpointPercent = Utilities.GetHighByte(value);
        }

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.BoilerFanSpeedSetpointAndActual;
    }
}
