using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

using MS = TekuSP.Drivers.Nano_OpenTherm.Enums.MasterStatus;
using SS = TekuSP.Drivers.Nano_OpenTherm.Enums.SlaveStatus;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Ventilation/Heat-recovery status flags
    /// </summary>
    public class GetVentilationStatusRequest : ReadRequest, IMasterStatus, ISlaveStatus
    {
        #region Public Constructors

        public GetVentilationStatusRequest() : base()
        {
        }

        public GetVentilationStatusRequest(Request baseReq) : base(baseReq)
        {
        }

        /// <summary>
        /// Convenience constructor to initialize master/slave status fields (used in some devices for echo/ack frames).
        /// </summary>
        public GetVentilationStatusRequest(MS master, SS slave)
        {
            MasterStatus = master;
            SlaveStatus = slave;
        }

        #endregion Public Constructors

        #region Public Properties

        // IMasterStatus
        public bool MasterIsCentralHeating2Active { get => MasterStatus.IsSet(MS.CH2Enabled); set => MasterStatus = MasterStatus.SetFlag(MS.CH2Enabled, value); }

        public bool MasterIsCentralHeatingActive { get => MasterStatus.IsSet(MS.CHEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CHEnabled, value); }

        public bool MasterIsCoolingActive { get => MasterStatus.IsSet(MS.CoolingEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CoolingEnabled, value); }

        public bool MasterIsHotWaterActive { get => MasterStatus.IsSet(MS.DHWEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.DHWEnabled, value); }

        public bool MasterOTCActive { get => MasterStatus.IsSet(MS.OTCActive); set => MasterStatus = MasterStatus.SetFlag(MS.OTCActive, value); }

    public bool MasterSummerWinterMode { get => MasterStatus.IsSet(MS.SummerWinterMode); set => MasterStatus = MasterStatus.SetFlag(MS.SummerWinterMode, value); }

    public bool MasterDHWBlocking { get => MasterStatus.IsSet(MS.DHWBlocking); set => MasterStatus = MasterStatus.SetFlag(MS.DHWBlocking, value); }


        public bool MasterReserved7 { get => MasterStatus.IsSet(MS.Reserved7); set => MasterStatus = MasterStatus.SetFlag(MS.Reserved7, value); }

        public override MessageID MessageID => MessageID.StatusVentilationHeatRecovery;

        public override MessageType MessageType => MessageType.READ_DATA;

        public bool SlaveCoolingStatus { get => SlaveStatus.IsSet(SS.CoolingStatus); set => SlaveStatus = SlaveStatus.SetFlag(SS.CoolingStatus, value); }

        public bool SlaveDHWMode { get => SlaveStatus.IsSet(SS.DHWMode); set => SlaveStatus = SlaveStatus.SetFlag(SS.DHWMode, value); }

        public bool SlaveDiagnosticIndication { get => SlaveStatus.IsSet(SS.DiagnosticIndication); set => SlaveStatus = SlaveStatus.SetFlag(SS.DiagnosticIndication, value); }

        public bool SlaveFaultIndication { get => SlaveStatus.IsSet(SS.FaultIndication); set => SlaveStatus = SlaveStatus.SetFlag(SS.FaultIndication, value); }

        public bool SlaveFlameStatus { get => SlaveStatus.IsSet(SS.FlameStatus); set => SlaveStatus = SlaveStatus.SetFlag(SS.FlameStatus, value); }

        // ISlaveStatus
        public bool SlaveCH2Mode { get => SlaveStatus.IsSet(SS.CH2Mode); set => SlaveStatus = SlaveStatus.SetFlag(SS.CH2Mode, value); }

        public bool SlaveCHMode { get => SlaveStatus.IsSet(SS.CHMode); set => SlaveStatus = SlaveStatus.SetFlag(SS.CHMode, value); }

        public bool SlaveReserved7 { get => SlaveStatus.IsSet(SS.Reserved); set => SlaveStatus = SlaveStatus.SetFlag(SS.Reserved, value); }

        #endregion Public Properties

        #region Protected Properties

        // Expose enums as writable so callers can compose flags and have them encoded
        protected MS MasterStatus { get; set; }

        protected SS SlaveStatus { get; set; }

        #endregion Protected Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            // Pack master in low byte and slave in high byte
            byte low = Utilities.SetMasterStatus(MasterStatus);
            byte high = Utilities.SetSlaveStatus(SlaveStatus);
            ushort payload = Utilities.MakeUShort(high, low);
            return ProcessRequest(payload);
        }

        protected override void SetRawDataCore(uint value)
        {
            MasterStatus = Utilities.GetMasterStatus(value);
            SlaveStatus = Utilities.GetSlaveStatus(value);
        }

        #endregion Protected Methods
    }
}