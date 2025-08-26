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
            uint data = (uint)(((byte)TransferReadWrite << 8) | (byte)TransferEnable);
            return ProcessRequest(data);
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
        public bool EnableDhwSetpoint { get => (TransferEnable & Enums.RemoteParameterTransferEnable.DHWSetpoint) != 0; set { if (value) TransferEnable |= Enums.RemoteParameterTransferEnable.DHWSetpoint; else TransferEnable &= ~Enums.RemoteParameterTransferEnable.DHWSetpoint; } }
        /// <summary>Enable transfer of Max CH Setpoint parameter.</summary>
        public bool EnableMaxChSetpoint { get => (TransferEnable & Enums.RemoteParameterTransferEnable.MaxCHSetpoint) != 0; set { if (value) TransferEnable |= Enums.RemoteParameterTransferEnable.MaxCHSetpoint; else TransferEnable &= ~Enums.RemoteParameterTransferEnable.MaxCHSetpoint; } }
        /// <summary>DHW Setpoint is writable (otherwise read-only).</summary>
        public bool RWDhwSetpoint { get => (TransferReadWrite & Enums.RemoteParameterTransferReadWrite.DHWSetpoint) != 0; set { if (value) TransferReadWrite |= Enums.RemoteParameterTransferReadWrite.DHWSetpoint; else TransferReadWrite &= ~Enums.RemoteParameterTransferReadWrite.DHWSetpoint; } }
        /// <summary>Max CH Setpoint is writable (otherwise read-only).</summary>
        public bool RWMaxChSetpoint { get => (TransferReadWrite & Enums.RemoteParameterTransferReadWrite.MaxCHSetpoint) != 0; set { if (value) TransferReadWrite |= Enums.RemoteParameterTransferReadWrite.MaxCHSetpoint; else TransferReadWrite &= ~Enums.RemoteParameterTransferReadWrite.MaxCHSetpoint; } }
    }
}
