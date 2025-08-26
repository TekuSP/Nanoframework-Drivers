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
    public bool EnableDhwSetpoint { get => Utilities.IsSet(TransferEnable, RemoteParameterTransferEnable.DHWSetpoint); set => Utilities.SetFlag(ref TransferEnable, RemoteParameterTransferEnable.DHWSetpoint, value); }
        /// <summary>Enable transfer of Max CH Setpoint parameter.</summary>
    public bool EnableMaxChSetpoint { get => Utilities.IsSet(TransferEnable, RemoteParameterTransferEnable.MaxCHSetpoint); set => Utilities.SetFlag(ref TransferEnable, RemoteParameterTransferEnable.MaxCHSetpoint, value); }
        /// <summary>DHw Setpoint is writable (otherwise read-only).</summary>
    public bool RWDhwSetpoint { get => Utilities.IsSet(TransferReadWrite, RemoteParameterTransferReadWrite.DHWSetpoint); set => Utilities.SetFlag(ref TransferReadWrite, RemoteParameterTransferReadWrite.DHWSetpoint, value); }
        /// <summary>Max CH Setpoint is writable (otherwise read-only).</summary>
    public bool RWMaxChSetpoint { get => Utilities.IsSet(TransferReadWrite, RemoteParameterTransferReadWrite.MaxCHSetpoint); set => Utilities.SetFlag(ref TransferReadWrite, RemoteParameterTransferReadWrite.MaxCHSetpoint, value); }
    }
}
