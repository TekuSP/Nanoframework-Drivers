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

    /// <summary>
    /// OpenTherm major version number supported by the slave (high byte).
    /// </summary>
    public byte Major { get; set; }
    /// <summary>
    /// OpenTherm minor version number supported by the slave (low byte).
    /// </summary>
    public byte Minor { get; set; }

        protected override uint GetRawDataCore()
        {
            ushort payload = Utilities.MakeUShort(Major, Minor);
            return ProcessRequest(payload);
        }
        protected override void SetRawDataCore(uint value)
        {
            Major = Utilities.GetHighByte(value);
            Minor = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.OpenThermVersionSlave;
    }
}
