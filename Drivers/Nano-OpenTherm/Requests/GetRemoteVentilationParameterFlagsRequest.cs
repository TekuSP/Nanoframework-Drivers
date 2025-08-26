using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads remote ventilation parameter enable and read/write capability flags.
    /// </summary>
    /// <remarks>
    /// Low byte contains <see cref="TransferEnable"/> flags; high byte contains <see cref="TransferReadWrite"/> flags.
    /// </remarks>
    public class GetRemoteVentilationParameterFlagsRequest : ReadRequest
    {
        public GetRemoteVentilationParameterFlagsRequest() : base() { }
        public GetRemoteVentilationParameterFlagsRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Transfer-enable flags for remote ventilation parameters (low byte).
        /// </summary>
        public Enums.RemoteParameterTransferEnable TransferEnable { get; set; }
        /// <summary>
        /// Read/Write capability flags for remote ventilation parameters (high byte).
        /// </summary>
        public Enums.RemoteParameterTransferReadWrite TransferReadWrite { get; set; }

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
        public override MessageID MessageID => MessageID.RBPflagsVentilationHeatRecovery;

        // Convenience flag properties
        /// <summary>Enable transfer of DHW Setpoint parameter.</summary>
    public bool EnableDhwSetpoint { get => Utilities.IsSet(TransferEnable, Enums.RemoteParameterTransferEnable.DHWSetpoint); set => Utilities.SetFlag(ref TransferEnable, Enums.RemoteParameterTransferEnable.DHWSetpoint, value); }
        /// <summary>Enable transfer of Max CH Setpoint parameter.</summary>
    public bool EnableMaxChSetpoint { get => Utilities.IsSet(TransferEnable, Enums.RemoteParameterTransferEnable.MaxCHSetpoint); set => Utilities.SetFlag(ref TransferEnable, Enums.RemoteParameterTransferEnable.MaxCHSetpoint, value); }
        /// <summary>DHW Setpoint is writable (otherwise read-only).</summary>
    public bool RWDhwSetpoint { get => Utilities.IsSet(TransferReadWrite, Enums.RemoteParameterTransferReadWrite.DHWSetpoint); set => Utilities.SetFlag(ref TransferReadWrite, Enums.RemoteParameterTransferReadWrite.DHWSetpoint, value); }
        /// <summary>Max CH Setpoint is writable (otherwise read-only).</summary>
    public bool RWMaxChSetpoint { get => Utilities.IsSet(TransferReadWrite, Enums.RemoteParameterTransferReadWrite.MaxCHSetpoint); set => Utilities.SetFlag(ref TransferReadWrite, Enums.RemoteParameterTransferReadWrite.MaxCHSetpoint, value); }
    }
}
