using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the remote boiler parameter transfer-enable and read/write capability flags.
    /// </summary>
    public class GetRemoteBoilerParameterFlagsRequest : ReadRequest
    {
        public GetRemoteBoilerParameterFlagsRequest() : base() { }
        public GetRemoteBoilerParameterFlagsRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Transfer-enable flags for remote boiler parameters (low byte).
        /// </summary>
        public RemoteParameterTransferEnable TransferEnable { get; set; }
        /// <summary>
        /// Read/Write capability flags for remote boiler parameters (high byte).
        /// </summary>
        public RemoteParameterTransferReadWrite TransferReadWrite { get; set; }

        protected override uint GetRawDataCore()
        {
            // Spec: low byte = enable flags, high byte = read/write flags
            byte low = Utilities.SetRemoteParameterTransferEnable(TransferEnable);
            byte high = Utilities.SetRemoteParameterTransferReadWrite(TransferReadWrite);
            ushort payload = Utilities.MakeUShort(high, low);
            return ProcessRequest(payload);
        }
        protected override void SetRawDataCore(uint value)
        {
            TransferEnable = Utilities.GetRemoteParameterTransferEnable(value);
            TransferReadWrite = Utilities.GetRemoteParameterTransferReadWrite(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.RBPflags;

        // Convenience properties
        /// <summary>Enable transfer of DHW Setpoint parameter.</summary>
        public bool EnableDhwSetpoint { get => TransferEnable.IsSet(RemoteParameterTransferEnable.DHWSetpoint); set => TransferEnable = TransferEnable.SetFlag(RemoteParameterTransferEnable.DHWSetpoint, value); }
        /// <summary>Enable transfer of Max CH Setpoint parameter.</summary>
        public bool EnableMaxChSetpoint { get => TransferEnable.IsSet(RemoteParameterTransferEnable.MaxCHSetpoint); set => TransferEnable = TransferEnable.SetFlag(RemoteParameterTransferEnable.MaxCHSetpoint, value); }
        /// <summary>DHw Setpoint is writable (otherwise read-only).</summary>
        public bool RWDhwSetpoint { get => TransferReadWrite.IsSet(RemoteParameterTransferReadWrite.DHWSetpoint); set => TransferReadWrite = TransferReadWrite.SetFlag(RemoteParameterTransferReadWrite.DHWSetpoint, value); }
        /// <summary>Max CH Setpoint is writable (otherwise read-only).</summary>
        public bool RWMaxChSetpoint { get => TransferReadWrite.IsSet(RemoteParameterTransferReadWrite.MaxCHSetpoint); set => TransferReadWrite = TransferReadWrite.SetFlag(RemoteParameterTransferReadWrite.MaxCHSetpoint, value); }
    }
}
