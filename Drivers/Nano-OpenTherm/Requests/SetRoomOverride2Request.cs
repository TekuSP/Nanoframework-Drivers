using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads Remote Override Room Setpoint 2 (°C). v2.2: treat as read-only.
    /// </summary>
    public class GetRoomOverride2Request : ReadRequest
    {
        #region Public Constructors

        public GetRoomOverride2Request() : base()
        {
        }

        public GetRoomOverride2Request(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.RemoteOverrideRoomSetPoint2Response);
        public override MessageID MessageID => MessageID.TrOverride2;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(0);

        protected override void SetRawDataCore(uint value)
        { }

        #endregion Protected Methods
    }
}