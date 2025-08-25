using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Remote override room Setpoint (°C)
    /// </summary>
    public class SetRoomOverrideRequest : WriteRequest
    {
        public SetRoomOverrideRequest() : base() { }
        public SetRoomOverrideRequest(Request baseReq) : base(baseReq) { }

        public float Temperature { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Utilities.GetRawTemperature(Temperature));
        protected override void SetRawDataCore(uint value) => Temperature = Utilities.GetFloat(value);

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.TrOverride;
    }
}
