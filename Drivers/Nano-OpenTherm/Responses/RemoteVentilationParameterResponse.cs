using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Remote ventilation parameter enable/read-write flags.</summary>
    public class RemoteVentilationParameterResponse : Response, IRemoteParameterTransferEnable, IRemoteParameterTransferReadWrite
    {
        protected RemoteParameterTransferEnable RemoteParameterTransferEnable { get; set; }
        protected RemoteParameterTransferReadWrite RemoteParameterTransferReadWrite { get; set; }

        public RemoteVentilationParameterResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public RemoteVentilationParameterResponse(Response r) : base(r) { }

        protected override uint GetRawDataCore()
        {
            byte low = Utilities.SetRemoteParameterTransferEnable(RemoteParameterTransferEnable);
            byte high = Utilities.SetRemoteParameterTransferReadWrite(RemoteParameterTransferReadWrite);
            return ProcessResponse(Utilities.MakeUShort(high, low));
        }

        protected override void SetRawDataCore(uint value)
        {
            RemoteParameterTransferEnable = Utilities.GetRemoteParameterTransferEnable(value);
            RemoteParameterTransferReadWrite = Utilities.GetRemoteParameterTransferReadWrite(value);
        }

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.RBPflagsVentilationHeatRecovery;

        // IRemoteParameterTransferEnable
        public bool EnableDHWSetpoint { get => RemoteParameterTransferEnable.IsSet(RemoteParameterTransferEnable.DHWSetpoint); set => RemoteParameterTransferEnable = RemoteParameterTransferEnable.SetFlag(RemoteParameterTransferEnable.DHWSetpoint, value); }
        public bool EnableMaxCHSetpoint { get => RemoteParameterTransferEnable.IsSet(RemoteParameterTransferEnable.MaxCHSetpoint); set => RemoteParameterTransferEnable = RemoteParameterTransferEnable.SetFlag(RemoteParameterTransferEnable.MaxCHSetpoint, value); }
        public bool EnableReserved2 { get => RemoteParameterTransferEnable.IsSet(RemoteParameterTransferEnable.Reserved2); set => RemoteParameterTransferEnable = RemoteParameterTransferEnable.SetFlag(RemoteParameterTransferEnable.Reserved2, value); }
        public bool EnableReserved3 { get => RemoteParameterTransferEnable.IsSet(RemoteParameterTransferEnable.Reserved3); set => RemoteParameterTransferEnable = RemoteParameterTransferEnable.SetFlag(RemoteParameterTransferEnable.Reserved3, value); }
        public bool EnableReserved4 { get => RemoteParameterTransferEnable.IsSet(RemoteParameterTransferEnable.Reserved4); set => RemoteParameterTransferEnable = RemoteParameterTransferEnable.SetFlag(RemoteParameterTransferEnable.Reserved4, value); }
        public bool EnableReserved5 { get => RemoteParameterTransferEnable.IsSet(RemoteParameterTransferEnable.Reserved5); set => RemoteParameterTransferEnable = RemoteParameterTransferEnable.SetFlag(RemoteParameterTransferEnable.Reserved5, value); }
        public bool EnableReserved6 { get => RemoteParameterTransferEnable.IsSet(RemoteParameterTransferEnable.Reserved6); set => RemoteParameterTransferEnable = RemoteParameterTransferEnable.SetFlag(RemoteParameterTransferEnable.Reserved6, value); }
        public bool EnableReserved7 { get => RemoteParameterTransferEnable.IsSet(RemoteParameterTransferEnable.Reserved7); set => RemoteParameterTransferEnable = RemoteParameterTransferEnable.SetFlag(RemoteParameterTransferEnable.Reserved7, value); }

        // IRemoteParameterTransferReadWrite
        public bool RWDHWSetpoint { get => RemoteParameterTransferReadWrite.IsSet(RemoteParameterTransferReadWrite.DHWSetpoint); set => RemoteParameterTransferReadWrite = RemoteParameterTransferReadWrite.SetFlag(RemoteParameterTransferReadWrite.DHWSetpoint, value); }
        public bool RWMaxCHSetpoint { get => RemoteParameterTransferReadWrite.IsSet(RemoteParameterTransferReadWrite.MaxCHSetpoint); set => RemoteParameterTransferReadWrite = RemoteParameterTransferReadWrite.SetFlag(RemoteParameterTransferReadWrite.MaxCHSetpoint, value); }
        public bool RWReserved2 { get => RemoteParameterTransferReadWrite.IsSet(RemoteParameterTransferReadWrite.Reserved2); set => RemoteParameterTransferReadWrite = RemoteParameterTransferReadWrite.SetFlag(RemoteParameterTransferReadWrite.Reserved2, value); }
        public bool RWReserved3 { get => RemoteParameterTransferReadWrite.IsSet(RemoteParameterTransferReadWrite.Reserved3); set => RemoteParameterTransferReadWrite = RemoteParameterTransferReadWrite.SetFlag(RemoteParameterTransferReadWrite.Reserved3, value); }
        public bool RWReserved4 { get => RemoteParameterTransferReadWrite.IsSet(RemoteParameterTransferReadWrite.Reserved4); set => RemoteParameterTransferReadWrite = RemoteParameterTransferReadWrite.SetFlag(RemoteParameterTransferReadWrite.Reserved4, value); }
        public bool RWReserved5 { get => RemoteParameterTransferReadWrite.IsSet(RemoteParameterTransferReadWrite.Reserved5); set => RemoteParameterTransferReadWrite = RemoteParameterTransferReadWrite.SetFlag(RemoteParameterTransferReadWrite.Reserved5, value); }
        public bool RWReserved6 { get => RemoteParameterTransferReadWrite.IsSet(RemoteParameterTransferReadWrite.Reserved6); set => RemoteParameterTransferReadWrite = RemoteParameterTransferReadWrite.SetFlag(RemoteParameterTransferReadWrite.Reserved6, value); }
        public bool RWReserved7 { get => RemoteParameterTransferReadWrite.IsSet(RemoteParameterTransferReadWrite.Reserved7); set => RemoteParameterTransferReadWrite = RemoteParameterTransferReadWrite.SetFlag(RemoteParameterTransferReadWrite.Reserved7, value); }
    }
}
