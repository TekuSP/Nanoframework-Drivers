using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the return water temperature from the boiler circuit.
    /// </summary>
    /// <remarks>
    /// Response payload: low 16 bits as 8.8 fixed-point degrees Celsius.
    /// </remarks>
    public class GetReturnTemperatureRequest : ReadRequest
    {
        public GetReturnTemperatureRequest() : base() { }
        public GetReturnTemperatureRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }
        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.Tret;
    }
}
