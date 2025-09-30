using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

using RPE = TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteParameterTransferEnable;
using RPRW = TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteParameterTransferReadWrite;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the remote boiler parameter transfer-enable and read/write capability flags.
    /// </summary>
    public class GetRemoteBoilerParameterFlagsRequest : ReadRequest, IRemoteParameterTransferEnable, IRemoteParameterTransferReadWrite
    {
        #region Public Constructors

        public GetRemoteBoilerParameterFlagsRequest() : base()
        {
        }

        public GetRemoteBoilerParameterFlagsRequest(Request baseReq) : base(baseReq)
        {
        }

        /// <summary>
        /// Convenience constructor to initialize enable (LB) and read/write (HB) flags.
        /// </summary>
        public GetRemoteBoilerParameterFlagsRequest(RemoteParameterTransferEnable enable, RemoteParameterTransferReadWrite rw)
            : base()
        {
            RemoteParameterTransferEnable = enable;
            RemoteParameterTransferReadWrite = rw;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool EnableDHWSetpoint { get => RemoteParameterTransferEnable.IsSet(RPE.DHWSetpoint); set => RemoteParameterTransferEnable = RemoteParameterTransferEnable.SetFlag(RPE.DHWSetpoint, value); }

        // Backing via protected auto-properties only
        public bool EnableMaxCHSetpoint { get => RemoteParameterTransferEnable.IsSet(RPE.MaxCHSetpoint); set => RemoteParameterTransferEnable = RemoteParameterTransferEnable.SetFlag(RPE.MaxCHSetpoint, value); }

        public bool EnableReserved2 { get => RemoteParameterTransferEnable.IsSet(RPE.Reserved2); set => RemoteParameterTransferEnable = RemoteParameterTransferEnable.SetFlag(RPE.Reserved2, value); }
        public bool EnableReserved3 { get => RemoteParameterTransferEnable.IsSet(RPE.Reserved3); set => RemoteParameterTransferEnable = RemoteParameterTransferEnable.SetFlag(RPE.Reserved3, value); }
        public bool EnableReserved4 { get => RemoteParameterTransferEnable.IsSet(RPE.Reserved4); set => RemoteParameterTransferEnable = RemoteParameterTransferEnable.SetFlag(RPE.Reserved4, value); }
        public bool EnableReserved5 { get => RemoteParameterTransferEnable.IsSet(RPE.Reserved5); set => RemoteParameterTransferEnable = RemoteParameterTransferEnable.SetFlag(RPE.Reserved5, value); }
        public bool EnableReserved6 { get => RemoteParameterTransferEnable.IsSet(RPE.Reserved6); set => RemoteParameterTransferEnable = RemoteParameterTransferEnable.SetFlag(RPE.Reserved6, value); }
        public bool EnableReserved7 { get => RemoteParameterTransferEnable.IsSet(RPE.Reserved7); set => RemoteParameterTransferEnable = RemoteParameterTransferEnable.SetFlag(RPE.Reserved7, value); }
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.RemoteBoilerParameterResponse);
        public override MessageID MessageID => MessageID.RBPflags;

        public override MessageType MessageType => MessageType.READ_DATA;

        public bool RWDHWSetpoint { get => RemoteParameterTransferReadWrite.IsSet(RPRW.DHWSetpoint); set => RemoteParameterTransferReadWrite = RemoteParameterTransferReadWrite.SetFlag(RPRW.DHWSetpoint, value); }

        public bool RWMaxCHSetpoint { get => RemoteParameterTransferReadWrite.IsSet(RPRW.MaxCHSetpoint); set => RemoteParameterTransferReadWrite = RemoteParameterTransferReadWrite.SetFlag(RPRW.MaxCHSetpoint, value); }

        public bool RWReserved2 { get => RemoteParameterTransferReadWrite.IsSet(RPRW.Reserved2); set => RemoteParameterTransferReadWrite = RemoteParameterTransferReadWrite.SetFlag(RPRW.Reserved2, value); }

        public bool RWReserved3 { get => RemoteParameterTransferReadWrite.IsSet(RPRW.Reserved3); set => RemoteParameterTransferReadWrite = RemoteParameterTransferReadWrite.SetFlag(RPRW.Reserved3, value); }

        public bool RWReserved4 { get => RemoteParameterTransferReadWrite.IsSet(RPRW.Reserved4); set => RemoteParameterTransferReadWrite = RemoteParameterTransferReadWrite.SetFlag(RPRW.Reserved4, value); }

        public bool RWReserved5 { get => RemoteParameterTransferReadWrite.IsSet(RPRW.Reserved5); set => RemoteParameterTransferReadWrite = RemoteParameterTransferReadWrite.SetFlag(RPRW.Reserved5, value); }

        public bool RWReserved6 { get => RemoteParameterTransferReadWrite.IsSet(RPRW.Reserved6); set => RemoteParameterTransferReadWrite = RemoteParameterTransferReadWrite.SetFlag(RPRW.Reserved6, value); }

        public bool RWReserved7 { get => RemoteParameterTransferReadWrite.IsSet(RPRW.Reserved7); set => RemoteParameterTransferReadWrite = RemoteParameterTransferReadWrite.SetFlag(RPRW.Reserved7, value); }

        #endregion Public Properties

        #region Protected Properties

        // IRemoteParameterTransferEnable
        protected RemoteParameterTransferEnable RemoteParameterTransferEnable { get; set; }

        // IRemoteParameterTransferReadWrite
        protected RemoteParameterTransferReadWrite RemoteParameterTransferReadWrite { get; set; }

        #endregion Protected Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            // Spec: low byte = enable flags, high byte = read/write flags
            byte low = Utilities.SetRemoteParameterTransferEnable(RemoteParameterTransferEnable);
            byte high = Utilities.SetRemoteParameterTransferReadWrite(RemoteParameterTransferReadWrite);
            ushort payload = Utilities.MakeUShort(high, low);
            return ProcessRequest(payload);
        }

        protected override void SetRawDataCore(uint value)
        {
            RemoteParameterTransferEnable = Utilities.GetRemoteParameterTransferEnable(value);
            RemoteParameterTransferReadWrite = Utilities.GetRemoteParameterTransferReadWrite(value);
        }

        #endregion Protected Methods
    }
}