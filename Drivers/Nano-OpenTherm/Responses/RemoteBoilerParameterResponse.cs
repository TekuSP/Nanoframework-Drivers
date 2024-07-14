using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class RemoteBoilerParameterResponse : Response
    {
        public RemoteBoilerParameterResponse(Response baseResponse)
        {
            RawData = baseResponse.RawData;
            MessageType = baseResponse.MessageType;
            MessageID = baseResponse.MessageID;
            RemoteParameterTransferEnable = Utilities.GetRemoteParameterTransferEnable(RawData);
            RemoteParameterTransferReadWrite = Utilities.GetRemoteParameterTransferReadWrite(RawData);
        }
        private RemoteParameterTransferEnable RemoteParameterTransferEnable
        {
            get;
        }
        private RemoteParameterTransferReadWrite RemoteParameterTransferReadWrite
        {
            get;
        }
        public override ulong RawData
        {
            get;
            set;
        }

        public override MessageType MessageType
        {
            get;
        }

        public override MessageID MessageID
        {
            get;
        }
        /// <summary>
        /// DHW (Domestic Hot Water) setpoint.
        /// </summary>
        public bool DHWSetpointEnable => (RemoteParameterTransferEnable & RemoteParameterTransferEnable.DHWSetpoint) == RemoteParameterTransferEnable.DHWSetpoint;
        /// <summary>
        /// Maximum CH (Central Heating) setpoint.
        /// </summary>
        public bool MaxCHSetpointEnable => (RemoteParameterTransferEnable & RemoteParameterTransferEnable.MaxCHSetpoint) == RemoteParameterTransferEnable.MaxCHSetpoint;

        /// <summary>
        /// DHW (Domestic Hot Water) setpoint.
        /// </summary>
        public bool DHWSetpointReadWrite => (RemoteParameterTransferReadWrite & RemoteParameterTransferReadWrite.DHWSetpoint) == RemoteParameterTransferReadWrite.DHWSetpoint;
        /// <summary>
        /// Maximum CH (Central Heating) setpoint.
        /// </summary>
        public bool MaxCHSetpointReadWrite => (RemoteParameterTransferReadWrite & RemoteParameterTransferReadWrite.MaxCHSetpoint) == RemoteParameterTransferReadWrite.MaxCHSetpoint;
    }
}
