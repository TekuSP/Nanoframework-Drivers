using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Control Setpoint for 2nd CH circuit (°C)
    /// </summary>
    public class SetCH2SetpointRequest : WriteRequest
    {
        public SetCH2SetpointRequest() : base() { }
        public SetCH2SetpointRequest(Request baseReq) : base(baseReq) { }

        public float Temperature { get; set; }

        protected override ulong GetRawDataCore() => ProcessRequest(Utilities.GetRawTemperature(Temperature));
        protected override void SetRawDataCore(ulong value) => Temperature = Utilities.GetFloat(value);

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.TsetCH2;
    }
}
