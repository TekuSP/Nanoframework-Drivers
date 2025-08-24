using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Max CH water Setpoint (°C) (Remote parameter 2)
    /// </summary>
    public class SetMaxCHSetpointRequest : WriteRequest
    {
        public SetMaxCHSetpointRequest() : base() { }
        public SetMaxCHSetpointRequest(Request baseReq) : base(baseReq) { }

        private float _temperature;
        public float Temperature
        {
            get => _temperature;
            set => _temperature = value.Normalize();
        }

        protected override ulong GetRawDataCore() => ProcessRequest(Utilities.GetRawTemperature(Temperature));
        protected override void SetRawDataCore(ulong value) => Temperature = Utilities.GetFloat(value);

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.MaxTSet;
    }
}
