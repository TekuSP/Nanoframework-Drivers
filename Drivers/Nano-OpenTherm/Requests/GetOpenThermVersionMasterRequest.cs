using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// The implemented version of the OpenTherm Protocol Specification in the master
    /// </summary>
    public class GetOpenThermVersionMasterRequest : ReadRequest
    {
        public GetOpenThermVersionMasterRequest() : base() { }
        public GetOpenThermVersionMasterRequest(Request baseReq) : base(baseReq) { }

        public byte Major { get; set; }
        public byte Minor { get; set; }

        protected override uint GetRawDataCore()
        {
            uint raw = (uint)((Major << 8) | Minor);
            return ProcessRequest(raw);
        }
        protected override void SetRawDataCore(uint value)
        {
            Major = Utilities.GetHighByte(value);
            Minor = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.OpenThermVersionMaster;
    }
}
