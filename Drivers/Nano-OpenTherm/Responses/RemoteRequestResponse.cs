using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

using MS = TekuSP.Drivers.Nano_OpenTherm.Enums.MasterStatus;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class RemoteRequestResponse : Response, IMasterStatus
    {
        #region Public Constructors

        public RemoteRequestResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        /// <summary>
        /// Convenience constructor to initialize master status flags (low byte).
        /// </summary>
        public RemoteRequestResponse(MS master, MessageType mt = MessageType.READ_ACK)
        {
            MessageType = mt;
            MasterStatus = master;
        }

        public RemoteRequestResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public bool MasterDHWBlocking { get => MasterStatus.IsSet(MasterStatus.DHWBlocking); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.DHWBlocking, value); }

        // IMasterStatus bits
        public bool MasterIsCentralHeating2Active { get => MasterStatus.IsSet(MS.CH2Enabled); set => MasterStatus = MasterStatus.SetFlag(MS.CH2Enabled, value); }

        public bool MasterIsCentralHeatingActive { get => MasterStatus.IsSet(MS.CHEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CHEnabled, value); }
        public bool MasterIsCoolingActive { get => MasterStatus.IsSet(MS.CoolingEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CoolingEnabled, value); }
        public bool MasterIsHotWaterActive { get => MasterStatus.IsSet(MS.DHWEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.DHWEnabled, value); }
        public bool MasterOTCActive { get => MasterStatus.IsSet(MS.OTCActive); set => MasterStatus = MasterStatus.SetFlag(MS.OTCActive, value); }
        public bool MasterReserved7 { get => MasterStatus.IsSet(MS.Reserved7); set => MasterStatus = MasterStatus.SetFlag(MS.Reserved7, value); }
        public bool MasterSummerWinterMode { get => MasterStatus.IsSet(MasterStatus.SummerWinterMode); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.SummerWinterMode, value); }
        public override MessageID MessageID => MessageID.RemoteRequest;
        public override MessageType MessageType { get; set; }

        #endregion Public Properties

        #region Protected Properties

        protected MS MasterStatus { get; set; }

        #endregion Protected Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessResponse(Utilities.SetMasterStatus(MasterStatus));

        protected override void SetRawDataCore(uint value) => MasterStatus = Utilities.GetMasterStatus(value);

        #endregion Protected Methods
    }
}