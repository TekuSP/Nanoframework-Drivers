using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Relative ventilation position (0-100%)
    /// </summary>
    public class SetVentilationPositionRequest : WriteRequest
    {
        public SetVentilationPositionRequest() : base() { }
        public SetVentilationPositionRequest(Request baseReq) : base(baseReq) { }

        private float _percent;
        public float Percent
        {
            get => _percent;
            set => _percent = value.Normalize();
        }

        protected override ulong GetRawDataCore()
        {
            uint raw = (uint)(Percent * 256f);
            return ProcessRequest(raw);
        }
        protected override void SetRawDataCore(ulong value)
        {
            Percent = Utilities.GetFloat(value);
        }

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.Vset;
    }
}
