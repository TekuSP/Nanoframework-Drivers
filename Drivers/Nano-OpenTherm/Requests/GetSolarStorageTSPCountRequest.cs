using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of Transparent Slave Parameters (TSP) supported by the Solar Storage.
    /// </summary>
    /// <remarks>
    /// The count is returned in the low 16 bits of the response payload.
    /// </remarks>
    public class GetSolarStorageTSPCountRequest : ReadRequest
    {
        public GetSolarStorageTSPCountRequest() : base() { }
        public GetSolarStorageTSPCountRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.TSPSolarStorage;
    }
}
