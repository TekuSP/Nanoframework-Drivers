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

        public byte Major { get; private set; }
        public byte Minor { get; private set; }

        protected override ulong GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(ulong value)
        {
            Major = Utilities.GetHighByte(value);
            Minor = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.OpenThermVersionMaster;
    }
}
