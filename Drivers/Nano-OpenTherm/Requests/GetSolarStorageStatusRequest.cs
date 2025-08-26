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
    public bool MasterIsCentralHeatingActive { get => Utilities.IsSet(MasterStatus, MS.CHEnabled); set => Utilities.SetFlag(ref MasterStatus, MS.CHEnabled, value); }
    /// <summary>Hot Water (DHW) enabled on Master.</summary>
    public bool MasterIsHotWaterActive { get => Utilities.IsSet(MasterStatus, MS.DHWEnabled); set => Utilities.SetFlag(ref MasterStatus, MS.DHWEnabled, value); }
    /// <summary>Cooling enabled on Master.</summary>
    public bool MasterIsCoolingActive { get => Utilities.IsSet(MasterStatus, MS.CoolingEnabled); set => Utilities.SetFlag(ref MasterStatus, MS.CoolingEnabled, value); }
    /// <summary>OpenTherm Continuous modulation/OTC active on Master.</summary>
    public bool MasterOTCActive { get => Utilities.IsSet(MasterStatus, MS.OTCActive); set => Utilities.SetFlag(ref MasterStatus, MS.OTCActive, value); }
    /// <summary>Central Heating 2 enabled on Master.</summary>
    public bool MasterIsCentralHeating2Active { get => Utilities.IsSet(MasterStatus, MS.CH2Enabled); set => Utilities.SetFlag(ref MasterStatus, MS.CH2Enabled, value); }

    // Slave convenience flags
    /// <summary>Slave fault indicator.</summary>
    public bool SlaveIsFault { get => Utilities.IsSet(SlaveStatus, SS.FaultIndication); set => Utilities.SetFlag(ref SlaveStatus, SS.FaultIndication, value); }
    /// <summary>Central Heating mode active on Slave.</summary>
    public bool SlaveIsCentralHeatingActive { get => Utilities.IsSet(SlaveStatus, SS.CHMode); set => Utilities.SetFlag(ref SlaveStatus, SS.CHMode, value); }
    /// <summary>Hot Water mode active on Slave.</summary>
    public bool SlaveIsHotWaterActive { get => Utilities.IsSet(SlaveStatus, SS.DHWMode); set => Utilities.SetFlag(ref SlaveStatus, SS.DHWMode, value); }
    /// <summary>Flame status active on Slave.</summary>
    public bool SlaveIsFlameOn { get => Utilities.IsSet(SlaveStatus, SS.FlameStatus); set => Utilities.SetFlag(ref SlaveStatus, SS.FlameStatus, value); }
    /// <summary>Cooling active on Slave.</summary>
    public bool SlaveIsCoolingActive { get => Utilities.IsSet(SlaveStatus, SS.CoolingStatus); set => Utilities.SetFlag(ref SlaveStatus, SS.CoolingStatus, value); }
    /// <summary>Central Heating 2 mode active on Slave.</summary>
    public bool SlaveIsCentralHeating2Active { get => Utilities.IsSet(SlaveStatus, SS.CH2Mode); set => Utilities.SetFlag(ref SlaveStatus, SS.CH2Mode, value); }
    /// <summary>Diagnostic indication active on Slave.</summary>
    public bool SlaveDiagnosticIndicationActive { get => Utilities.IsSet(SlaveStatus, SS.DiagnosticIndication); set => Utilities.SetFlag(ref SlaveStatus, SS.DiagnosticIndication, value); }
    }
}
