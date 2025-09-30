using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Remote Override Room Setpoint 2 (8.8 fixed-point temperature in °C).
    /// </summary>
    public class RemoteOverrideRoomSetPoint2Response : Response
    {
        #region Public Constructors

        public RemoteOverrideRoomSetPoint2Response(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public RemoteOverrideRoomSetPoint2Response(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.TrOverride2;

        public override MessageType MessageType { get; set; }

        /// <summary>Override room setpoint 2.</summary>
        public Temperature TrOverride2 { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawF88((float)TrOverride2.DegreesCelsius, 0, 100));

        protected override void SetRawDataCore(uint value) => TrOverride2 = Temperature.FromDegreesCelsius(Utilities.GetFloat(value));

        #endregion Protected Methods
    }
}