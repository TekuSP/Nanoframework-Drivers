using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class RemoteOverrideRoomSetPointResponse : Response
    {
        private ushort _raw16;

        /// <summary>
        /// Initializes a new response for constructing a frame to send.
        /// </summary>
        /// <param name="messageType">Optional message type to use. Defaults to <see cref="TekuSP.Drivers.Nano_OpenTherm.Enums.MessageType.READ_ACK"/>.</param>
        public RemoteOverrideRoomSetPointResponse(MessageType messageType = MessageType.READ_ACK)
        {
            MessageType = messageType;
        }

        public RemoteOverrideRoomSetPointResponse(Response baseResponse)
        {
            MessageType = baseResponse.MessageType;
            SetRawDataCore(baseResponse.RawData);
        }

        protected override uint GetRawDataCore() => ProcessResponse(_raw16);
        protected override void SetRawDataCore(uint value) => _raw16 = (ushort)(value & 0xFFFF);

    public override MessageType MessageType { get; set; }
    public override MessageID MessageID => MessageID.TrOverride;
        /// <summary>
        /// Remote override room setpoint
        /// </summary>
    public float TrOverride { get => Utilities.GetFloat(_raw16); set => _raw16 = (ushort)Utilities.GetRawTemperature(value); }
    }
}
