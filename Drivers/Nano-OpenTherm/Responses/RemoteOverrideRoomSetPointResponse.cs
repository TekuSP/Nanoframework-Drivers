using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class RemoteOverrideRoomSetPointResponse : Response
    {
        #region Private Fields

        private ushort _raw16;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new response for constructing a frame to send.
        /// </summary>
        /// <param name="messageType">Optional message type to use. Defaults to <see cref="TekuSP.Drivers.Nano_OpenTherm.Enums.MessageType.READ_ACK"/>.</param>
        public RemoteOverrideRoomSetPointResponse(MessageType messageType = MessageType.READ_ACK)
        {
            MessageType = messageType;
        }

        public RemoteOverrideRoomSetPointResponse(Response baseResponse) : base(baseResponse)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.TrOverride;

        public override MessageType MessageType { get; set; }

        /// <summary>
        /// Remote override room setpoint
        /// </summary>
        /// <summary>Remote room override setpoint temperature.</summary>
        public Temperature TrOverride
        {
            get => Temperature.FromDegreesCelsius(Utilities.GetFloat(_raw16));
            set => _raw16 = (ushort)Utilities.GetRawF88((float)value.DegreesCelsius, 0, 100);
        }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessResponse(_raw16);

        protected override void SetRawDataCore(uint value) => _raw16 = Utilities.GetLowUShort(value);

        #endregion Protected Methods
    }
}