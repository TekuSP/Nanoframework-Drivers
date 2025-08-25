using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetRemoteVentilationParameterFlagsRequest : ReadRequest
    {
        public GetRemoteVentilationParameterFlagsRequest() : base() { }
        public GetRemoteVentilationParameterFlagsRequest(Request baseReq) : base(baseReq) { }

        public Enums.RemoteParameterTransferEnable TransferEnable { get; set; }
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
        public bool EnableDhwSetpoint { get => (TransferEnable & Enums.RemoteParameterTransferEnable.DHWSetpoint) != 0; set { if (value) TransferEnable |= Enums.RemoteParameterTransferEnable.DHWSetpoint; else TransferEnable &= ~Enums.RemoteParameterTransferEnable.DHWSetpoint; } }
        public bool EnableMaxChSetpoint { get => (TransferEnable & Enums.RemoteParameterTransferEnable.MaxCHSetpoint) != 0; set { if (value) TransferEnable |= Enums.RemoteParameterTransferEnable.MaxCHSetpoint; else TransferEnable &= ~Enums.RemoteParameterTransferEnable.MaxCHSetpoint; } }
        public bool RWDhwSetpoint { get => (TransferReadWrite & Enums.RemoteParameterTransferReadWrite.DHWSetpoint) != 0; set { if (value) TransferReadWrite |= Enums.RemoteParameterTransferReadWrite.DHWSetpoint; else TransferReadWrite &= ~Enums.RemoteParameterTransferReadWrite.DHWSetpoint; } }
        public bool RWMaxChSetpoint { get => (TransferReadWrite & Enums.RemoteParameterTransferReadWrite.MaxCHSetpoint) != 0; set { if (value) TransferReadWrite |= Enums.RemoteParameterTransferReadWrite.MaxCHSetpoint; else TransferReadWrite &= ~Enums.RemoteParameterTransferReadWrite.MaxCHSetpoint; } }
    }
}
