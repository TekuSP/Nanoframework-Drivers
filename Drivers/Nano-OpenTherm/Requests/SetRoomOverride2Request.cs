using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Remote Override Room Setpoint 2 (°C)
    /// </summary>
    public class SetRoomOverride2Request : WriteRequest
    {
        #region Public Constructors

        public SetRoomOverride2Request() : base()
        {
        }

        public SetRoomOverride2Request(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.TrOverride2;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// Remote override room temperature 2 in °C, encoded as 8.8 fixed‑point in the low 16 bits.
        /// </summary>
        public float Temperature { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Utilities.GetRawTemperature(Temperature));

        protected override void SetRawDataCore(uint value) => Temperature = Utilities.GetFloat(value);

        #endregion Protected Methods
    }
}