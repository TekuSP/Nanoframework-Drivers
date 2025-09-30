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
        #region Public Constructors

        public GetPowerCyclesRequest() : base()
        {
        }

        public GetPowerCyclesRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.PowerCyclesResponse);

        public override MessageID MessageID => MessageID.PowerCycles;

        public override MessageType MessageType => MessageType.READ_DATA;

        /// <summary>
        /// Number of power on/off cycles (low 16 bits, unsigned).
        /// Units: count.
        /// </summary>
        public ushort PowerCycles { get; private set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(0);

        protected override void SetRawDataCore(uint value)
        {
            PowerCycles = Utilities.GetLowUShort(value);
        }

        #endregion Protected Methods
    }
}