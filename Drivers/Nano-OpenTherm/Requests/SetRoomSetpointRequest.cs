using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Room Setpoint (°C)
    /// </summary>
    public class SetRoomSetpointRequest : WriteRequest
    {
        #region Public Constructors

        public SetRoomSetpointRequest() : base()
        {
        }

        public SetRoomSetpointRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.RoomSetpointResponse);

        public override MessageID MessageID => MessageID.TrSet;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// Desired room setpoint in °C, encoded as 8.8 fixed‑point in the low 16 bits.
        /// </summary>
        public Temperature Temperature { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Utilities.GetRawF88((float)Temperature.DegreesCelsius, 0, 100));

        protected override void SetRawDataCore(uint value) => Temperature = Temperature.FromDegreesCelsius(Utilities.GetFloat(value));

        #endregion Protected Methods
    }
}