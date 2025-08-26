using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using MS = TekuSP.Drivers.Nano_OpenTherm.Enums.MasterStatus;
using SS = TekuSP.Drivers.Nano_OpenTherm.Enums.SlaveStatus;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads Solar Storage master and slave status flags.
    /// </summary>
    /// <remarks>
    /// The low byte contains <see cref="MasterStatus"/> flags and the high byte contains
    /// <see cref="SlaveStatus"/> flags. Convenience boolean properties expose common bits.
    /// </remarks>
    public class GetSolarStorageStatusRequest : ReadRequest
    {
        public GetSolarStorageStatusRequest() : base() { }
        public GetSolarStorageStatusRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Master status flags (low byte). Use convenience properties for individual bits.
        /// </summary>
        public MS MasterStatus { get; set; }
        /// <summary>
        /// Slave status flags (high byte). Use convenience properties for individual bits.
        /// </summary>
        public SS SlaveStatus { get; set; }

        protected override uint GetRawDataCore()
        {
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

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.StatusSolarStorage;

        // Master convenience flags
        /// <summary>Central Heating enabled on Master.</summary>
        public bool MasterIsCentralHeatingActive { get => MasterStatus.IsSet(MS.CHEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CHEnabled, value); }
        /// <summary>Hot Water (DHW) enabled on Master.</summary>
        public bool MasterIsHotWaterActive { get => MasterStatus.IsSet(MS.DHWEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.DHWEnabled, value); }
        /// <summary>Cooling enabled on Master.</summary>
        public bool MasterIsCoolingActive { get => MasterStatus.IsSet(MS.CoolingEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CoolingEnabled, value); }
        /// <summary>OpenTherm Continuous modulation/OTC active on Master.</summary>
        public bool MasterOTCActive { get => MasterStatus.IsSet(MS.OTCActive); set => MasterStatus = MasterStatus.SetFlag(MS.OTCActive, value); }
        /// <summary>Central Heating 2 enabled on Master.</summary>
        public bool MasterIsCentralHeating2Active { get => MasterStatus.IsSet(MS.CH2Enabled); set => MasterStatus = MasterStatus.SetFlag(MS.CH2Enabled, value); }

        // Slave convenience flags
        /// <summary>Slave fault indicator.</summary>
        public bool SlaveIsFault { get => SlaveStatus.IsSet(SS.FaultIndication); set => SlaveStatus = SlaveStatus.SetFlag(SS.FaultIndication, value); }
        /// <summary>Central Heating mode active on Slave.</summary>
        public bool SlaveIsCentralHeatingActive { get => SlaveStatus.IsSet(SS.CHMode); set => SlaveStatus = SlaveStatus.SetFlag(SS.CHMode, value); }
        /// <summary>Hot Water mode active on Slave.</summary>
        public bool SlaveIsHotWaterActive { get => SlaveStatus.IsSet(SS.DHWMode); set => SlaveStatus = SlaveStatus.SetFlag(SS.DHWMode, value); }
        /// <summary>Flame status active on Slave.</summary>
        public bool SlaveIsFlameOn { get => SlaveStatus.IsSet(SS.FlameStatus); set => SlaveStatus = SlaveStatus.SetFlag(SS.FlameStatus, value); }
        /// <summary>Cooling active on Slave.</summary>
        public bool SlaveIsCoolingActive { get => SlaveStatus.IsSet(SS.CoolingStatus); set => SlaveStatus = SlaveStatus.SetFlag(SS.CoolingStatus, value); }
        /// <summary>Central Heating 2 mode active on Slave.</summary>
        public bool SlaveIsCentralHeating2Active { get => SlaveStatus.IsSet(SS.CH2Mode); set => SlaveStatus = SlaveStatus.SetFlag(SS.CH2Mode, value); }
        /// <summary>Diagnostic indication active on Slave.</summary>
        public bool SlaveDiagnosticIndicationActive { get => SlaveStatus.IsSet(SS.DiagnosticIndication); set => SlaveStatus = SlaveStatus.SetFlag(SS.DiagnosticIndication, value); }
    }
}
