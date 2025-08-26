using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the relative ventilation level of the ventilation unit.
    /// </summary>
    /// <remarks>
    /// Response payload: low 16 bits as 8.8 fixed-point percent (0–100%).
    /// </remarks>
    public class GetRelativeVentilationLevelRequest : ReadRequest
    {
        public GetRelativeVentilationLevelRequest() : base() { }
        public GetRelativeVentilationLevelRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.RelVentLevel;
    }
}
