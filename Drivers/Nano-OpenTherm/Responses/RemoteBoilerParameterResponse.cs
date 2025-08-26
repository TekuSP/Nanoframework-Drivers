using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class RemoteBoilerParameterResponse : Response
    {
        private RemoteParameterTransferEnable _enable;
        private RemoteParameterTransferReadWrite _rw;

        /// <summary>
        /// Initializes a new response for constructing a frame to send.
        /// </summary>
        /// <param name="messageType">Optional message type to use. Defaults to <see cref="TekuSP.Drivers.Nano_OpenTherm.Enums.MessageType.READ_ACK"/>.</param>
        public RemoteBoilerParameterResponse(MessageType messageType = MessageType.READ_ACK)
        {
            MessageType = messageType;
        }

        public RemoteBoilerParameterResponse(Response baseResponse)
        {
            MessageType = baseResponse.MessageType;
            SetRawDataCore(baseResponse.RawData);
        }

        // Build the payload from current properties: high byte = Read/Write flags, low byte = Enable flags
        protected override uint GetRawDataCore()
        {
            var low = Utilities.SetRemoteParameterTransferEnable(_enable);
            var high = Utilities.SetRemoteParameterTransferReadWrite(_rw);
            return ProcessResponse(Utilities.MakeUShort(high, low));
        }
        protected override void SetRawDataCore(uint value)
        {
            _enable = Utilities.GetRemoteParameterTransferEnable(value);
            _rw = Utilities.GetRemoteParameterTransferReadWrite(value);
        }

    public override MessageType MessageType { get; set; }
    public override MessageID MessageID => MessageID.RBPflags;
        /// <summary>
        /// DHW (Domestic Hot Water) setpoint.
        /// </summary>
    public bool DHWSetpointEnable
        {
            get => Utilities.IsSet(_enable, RemoteParameterTransferEnable.DHWSetpoint);
            set => Utilities.SetFlag(ref _enable, RemoteParameterTransferEnable.DHWSetpoint, value);
        }
        /// <summary>
        /// Maximum CH (Central Heating) setpoint.
        /// </summary>
    public bool MaxCHSetpointEnable
        {
            get => Utilities.IsSet(_enable, RemoteParameterTransferEnable.MaxCHSetpoint);
            set => Utilities.SetFlag(ref _enable, RemoteParameterTransferEnable.MaxCHSetpoint, value);
        }

        /// <summary>
        /// DHW (Domestic Hot Water) setpoint.
        /// </summary>
    public bool DHWSetpointReadWrite
        {
            get => Utilities.IsSet(_rw, RemoteParameterTransferReadWrite.DHWSetpoint);
            set => Utilities.SetFlag(ref _rw, RemoteParameterTransferReadWrite.DHWSetpoint, value);
        }
        /// <summary>
        /// Maximum CH (Central Heating) setpoint.
        /// </summary>
    public bool MaxCHSetpointReadWrite
        {
            get => Utilities.IsSet(_rw, RemoteParameterTransferReadWrite.MaxCHSetpoint);
            set => Utilities.SetFlag(ref _rw, RemoteParameterTransferReadWrite.MaxCHSetpoint, value);
        }
    }
}
