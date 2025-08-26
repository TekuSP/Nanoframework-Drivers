using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the exhaust fan speed of the ventilation unit.
    /// </summary>
    /// <remarks>
    /// Response payload: low 16 bits as unsigned RPM.
    /// </remarks>
    public class GetExhaustFanSpeedRequest : ReadRequest
    {
        public GetExhaustFanSpeedRequest() : base() { }
        public GetExhaustFanSpeedRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.RPMexhaust;
    }
}
