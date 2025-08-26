using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of power on/off cycles recorded by the slave.
    /// </summary>
    /// <summary>
    /// Number of Power Cycles of a slave
    /// </summary>
    public class GetPowerCyclesRequest : ReadRequest
    {
        public GetPowerCyclesRequest() : base() { }
        public GetPowerCyclesRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Number of power on/off cycles (low 16 bits, unsigned).
    /// Units: count.
    /// </summary>
    public ushort PowerCycles { get; private set; }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value)
        {
            PowerCycles = Utilities.GetLowUShort(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.PowerCycles;
    }
}
