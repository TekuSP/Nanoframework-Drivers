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

        public float Temperature { get; set; }

        protected override ulong GetRawDataCore() => ProcessRequest(Utilities.GetRawTemperature(Temperature));
        protected override void SetRawDataCore(ulong value) => Temperature = Utilities.GetFloat(value);

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.TrSet;
    }
}
