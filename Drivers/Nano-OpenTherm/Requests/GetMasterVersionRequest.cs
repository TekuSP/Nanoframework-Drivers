using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Master product version number and type
    /// </summary>
    public class GetMasterVersionRequest : ReadRequest
    {
        public GetMasterVersionRequest() : base() { }
        public GetMasterVersionRequest(Request baseReq) : base(baseReq) { }

        public byte Version { get; set; }
        public byte Type { get; set; }

        protected override uint GetRawDataCore()
        {
            uint raw = (uint)((Version << 8) | Type);
            return ProcessRequest(raw);
        }
        protected override void SetRawDataCore(uint value)
        {
            Version = Utilities.GetHighByte(value);
            Type = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.MasterVersion;
    }
}
