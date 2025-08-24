using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Nominal relative ventilation value (0-100%)
    /// </summary>
    public class SetNominalVentilationValueRequest : WriteRequest
    {
        public SetNominalVentilationValueRequest() : base() { }
        public SetNominalVentilationValueRequest(Request baseReq) : base(baseReq) { }

        public byte Percent { get; set; }

        protected override ulong GetRawDataCore()
        {
            // encode as 1.15 or 8.8? Use 8.8 fixed-point to be consistent with percent encodings
            uint raw = (uint)(Percent * 256);
            return ProcessRequest(raw);
        }
        protected override void SetRawDataCore(ulong value)
        {
            Percent = (byte)Utilities.GetFloat(value);
        }

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.NominalVentilationValue;
    }
}
