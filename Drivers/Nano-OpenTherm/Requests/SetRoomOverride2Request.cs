using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Remote Override Room Setpoint 2 (°C)
    /// </summary>
    public class SetRoomOverride2Request : WriteRequest
    {
        public SetRoomOverride2Request() : base() { }
        public SetRoomOverride2Request(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Remote override room temperature 2 in °C (encoded as 8.8 fixed-point in low 16 bits).
        /// </summary>
        public float Temperature { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Utilities.GetRawTemperature(Temperature));
        protected override void SetRawDataCore(uint value) => Temperature = Utilities.GetFloat(value);

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.TrOverride2;
    }
}
