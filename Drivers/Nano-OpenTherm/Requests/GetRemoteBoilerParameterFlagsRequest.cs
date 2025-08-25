using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetRemoteBoilerParameterFlagsRequest : ReadRequest
    {
        public GetRemoteBoilerParameterFlagsRequest() : base() { }
        public GetRemoteBoilerParameterFlagsRequest(Request baseReq) : base(baseReq) { }

        public RemoteParameterTransferEnable TransferEnable { get; set; }
        public RemoteParameterTransferReadWrite TransferReadWrite { get; set; }

        protected override ulong GetRawDataCore()
        {
            // Spec: low byte = enable flags, high byte = read/write flags
            uint data = (uint)(((byte)TransferReadWrite << 8) | (byte)TransferEnable);
            return ProcessRequest(data);
        }
        protected override void SetRawDataCore(ulong value)
        {
            TransferEnable = Utilities.GetRemoteParameterTransferEnable(value);
            TransferReadWrite = Utilities.GetRemoteParameterTransferReadWrite(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.RBPflags;

        // Convenience properties
        public bool EnableDhwSetpoint { get => (TransferEnable & RemoteParameterTransferEnable.DHWSetpoint) != 0; set { if (value) TransferEnable |= RemoteParameterTransferEnable.DHWSetpoint; else TransferEnable &= ~RemoteParameterTransferEnable.DHWSetpoint; } }
        public bool EnableMaxChSetpoint { get => (TransferEnable & RemoteParameterTransferEnable.MaxCHSetpoint) != 0; set { if (value) TransferEnable |= RemoteParameterTransferEnable.MaxCHSetpoint; else TransferEnable &= ~RemoteParameterTransferEnable.MaxCHSetpoint; } }
        public bool RWDhwSetpoint { get => (TransferReadWrite & RemoteParameterTransferReadWrite.DHWSetpoint) != 0; set { if (value) TransferReadWrite |= RemoteParameterTransferReadWrite.DHWSetpoint; else TransferReadWrite &= ~RemoteParameterTransferReadWrite.DHWSetpoint; } }
        public bool RWMaxChSetpoint { get => (TransferReadWrite & RemoteParameterTransferReadWrite.MaxCHSetpoint) != 0; set { if (value) TransferReadWrite |= RemoteParameterTransferReadWrite.MaxCHSetpoint; else TransferReadWrite &= ~RemoteParameterTransferReadWrite.MaxCHSetpoint; } }
    }
}
