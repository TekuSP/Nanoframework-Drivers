using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Room Setpoint for 2nd CH circuit (°C)
    /// </summary>
    public class SetRoomSetpointCH2Request : WriteRequest
    {
        #region Public Constructors

        public SetRoomSetpointCH2Request() : base()
        {
        }

        public SetRoomSetpointCH2Request(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.TrSetCH2;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// Desired room setpoint for CH2 in °C, encoded as 8.8 fixed‑point in the low 16 bits.
        /// </summary>
        public float Temperature { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Utilities.GetRawTemperature(Temperature));

        protected override void SetRawDataCore(uint value) => Temperature = Utilities.GetFloat(value);

        #endregion Protected Methods
    }
}