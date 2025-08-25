using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// The implemented version of the OpenTherm Protocol Specification in the slave
    /// </summary>
    public class GetOpenThermVersionSlaveRequest : ReadRequest
    {
        public GetOpenThermVersionSlaveRequest() : base() { }
        public GetOpenThermVersionSlaveRequest(Request baseReq) : base(baseReq) { }

        public byte Major { get; set; }
        public byte Minor { get; set; }

        protected override ulong GetRawDataCore()
        {
            uint raw = (uint)((Major << 8) | Minor);
            return ProcessRequest(raw);
        }
        protected override void SetRawDataCore(ulong value)
        {
            Major = Utilities.GetHighByte(value);
            Minor = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.OpenThermVersionSlave;
    }
}
