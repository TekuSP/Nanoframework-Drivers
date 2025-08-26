using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets the nominal relative ventilation value (0–100%).
    /// </summary>
    public class SetNominalVentilationValueRequest : WriteRequest
    {
        public SetNominalVentilationValueRequest() : base() { }
        public SetNominalVentilationValueRequest(Request baseReq) : base(baseReq) { }

        private float _percent;
        /// <summary>
        /// Nominal relative ventilation value in % (encoded as 8.8 fixed-point in low 16 bits). Value is clamped to 0–100.
        /// </summary>
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
