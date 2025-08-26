using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the size (number of entries) of the Solar Storage Fault History Buffer (FHB).
    /// </summary>
    /// <remarks>
    /// No selector is required; the size is returned in the low 16 bits of the response payload.
    /// </remarks>
    public class GetSolarStorageFHBSizeRequest : ReadRequest
    {
        public GetSolarStorageFHBSizeRequest() : base() { }
        public GetSolarStorageFHBSizeRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.FHBsizeSolarStorage;
    }
}
