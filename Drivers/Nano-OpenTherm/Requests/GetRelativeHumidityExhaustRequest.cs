using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the relative humidity (%RH) of the exhaust air stream.
    /// </summary>
    public class GetRelativeHumidityExhaustRequest : ReadRequest
    {
        public GetRelativeHumidityExhaustRequest() : base() { }
        public GetRelativeHumidityExhaustRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.RHexhaust;
    }
}
