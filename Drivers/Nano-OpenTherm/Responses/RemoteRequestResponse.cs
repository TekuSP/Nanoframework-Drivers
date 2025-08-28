using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using MS = TekuSP.Drivers.Nano_OpenTherm.Enums.MasterStatus;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class RemoteRequestResponse : Response, IMasterStatus
    {
        public RemoteRequestResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        /// <summary>
        /// Convenience constructor to initialize master status flags (low byte).
        /// </summary>
        public RemoteRequestResponse(MS master, MessageType mt = MessageType.READ_ACK)
        {
            MessageType = mt;
            MasterStatus = master;
        }
        public RemoteRequestResponse(Response r) : base(r) { }

        // IMasterStatus bits
        public bool MasterIsCentralHeating2Active { get => MasterStatus.IsSet(MS.CH2Enabled); set => MasterStatus = MasterStatus.SetFlag(MS.CH2Enabled, value); }
        public bool MasterIsCentralHeatingActive { get => MasterStatus.IsSet(MS.CHEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CHEnabled, value); }
        public bool MasterIsCoolingActive { get => MasterStatus.IsSet(MS.CoolingEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CoolingEnabled, value); }
        public bool MasterIsHotWaterActive { get => MasterStatus.IsSet(MS.DHWEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.DHWEnabled, value); }
        public bool MasterOTCActive { get => MasterStatus.IsSet(MS.OTCActive); set => MasterStatus = MasterStatus.SetFlag(MS.OTCActive, value); }
        public bool MasterReserved5 { get => MasterStatus.IsSet(MS.Reserved5); set => MasterStatus = MasterStatus.SetFlag(MS.Reserved5, value); }
        public bool MasterReserved6 { get => MasterStatus.IsSet(MS.Reserved6); set => MasterStatus = MasterStatus.SetFlag(MS.Reserved6, value); }
        public bool MasterReserved7 { get => MasterStatus.IsSet(MS.Reserved7); set => MasterStatus = MasterStatus.SetFlag(MS.Reserved7, value); }

    protected MS MasterStatus { get; set; }

        protected override uint GetRawDataCore() => ProcessResponse(Utilities.SetMasterStatus(MasterStatus));
        protected override void SetRawDataCore(uint value) => MasterStatus = Utilities.GetMasterStatus(value);

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.RemoteRequest;
    }
}
