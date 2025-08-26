using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of transparent slave parameters supported by the device.
    /// </summary>
    public class GetTransparentSlaveParametersCountRequest : ReadRequest
    {
        public GetTransparentSlaveParametersCountRequest() : base() { }
        public GetTransparentSlaveParametersCountRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.TSP;
    }
}
