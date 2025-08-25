using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
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
            uint data = (uint)(((byte)TransferReadWrite << 8) | (byte)TransferEnable);
            return ProcessRequest(data);
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
        public bool EnableDhwSetpoint { get => (TransferEnable & RemoteParameterTransferEnable.DHWSetpoint) != 0; set { if (value) TransferEnable |= RemoteParameterTransferEnable.DHWSetpoint; else TransferEnable &= ~RemoteParameterTransferEnable.DHWSetpoint; } }
        /// <summary>Enable transfer of Max CH Setpoint parameter.</summary>
        public bool EnableMaxChSetpoint { get => (TransferEnable & RemoteParameterTransferEnable.MaxCHSetpoint) != 0; set { if (value) TransferEnable |= RemoteParameterTransferEnable.MaxCHSetpoint; else TransferEnable &= ~RemoteParameterTransferEnable.MaxCHSetpoint; } }
        /// <summary>DHw Setpoint is writable (otherwise read-only).</summary>
        public bool RWDhwSetpoint { get => (TransferReadWrite & RemoteParameterTransferReadWrite.DHWSetpoint) != 0; set { if (value) TransferReadWrite |= RemoteParameterTransferReadWrite.DHWSetpoint; else TransferReadWrite &= ~RemoteParameterTransferReadWrite.DHWSetpoint; } }
        /// <summary>Max CH Setpoint is writable (otherwise read-only).</summary>
        public bool RWMaxChSetpoint { get => (TransferReadWrite & RemoteParameterTransferReadWrite.MaxCHSetpoint) != 0; set { if (value) TransferReadWrite |= RemoteParameterTransferReadWrite.MaxCHSetpoint; else TransferReadWrite &= ~RemoteParameterTransferReadWrite.MaxCHSetpoint; } }
    }
}
