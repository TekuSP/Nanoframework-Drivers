using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Room Setpoint (°C)
    /// </summary>
    public class SetRoomSetpointRequest : WriteRequest
    {
        public SetRoomSetpointRequest() : base() { }
        public SetRoomSetpointRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Desired room setpoint in °C, encoded as 8.8 fixed‑point in the low 16 bits.
    /// </summary>
        public float Temperature { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Utilities.GetRawTemperature(Temperature));
        protected override void SetRawDataCore(uint value) => Temperature = Utilities.GetFloat(value);

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.TrSet;
    }
}
