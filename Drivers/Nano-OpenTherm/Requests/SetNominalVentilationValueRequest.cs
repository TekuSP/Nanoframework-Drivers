using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Nominal relative ventilation value (0-100%)
    /// </summary>
    public class SetNominalVentilationValueRequest : WriteRequest
    {
        public SetNominalVentilationValueRequest() : base() { }
        public SetNominalVentilationValueRequest(Request baseReq) : base(baseReq) { }

        private float _percent;
        public float Percent
        {
            get => _percent;
            set => _percent = value.Normalize();
        }

        protected override uint GetRawDataCore()
        {
            uint raw = (uint)(Percent * 256f);
            return ProcessRequest(raw);
        }
        protected override void SetRawDataCore(uint value)
        {
            Percent = Utilities.GetFloat(value);
        }

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.NominalVentilationValue;
    }
}
