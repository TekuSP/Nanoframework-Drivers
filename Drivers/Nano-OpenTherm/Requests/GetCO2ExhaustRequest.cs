using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the CO₂ concentration in the exhaust air.
    /// </summary>
    /// <remarks>
    /// Response payload: low 16 bits as 8.8 fixed-point percent or ppm depending on device; consult vendor docs.
    /// </remarks>
    public class GetCO2ExhaustRequest : ReadRequest
    {
        public GetCO2ExhaustRequest() : base() { }
        public GetCO2ExhaustRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.CO2exhaust;
    }
}
