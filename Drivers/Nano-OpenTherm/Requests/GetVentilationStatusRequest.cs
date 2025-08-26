using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using MS = TekuSP.Drivers.Nano_OpenTherm.Enums.MasterStatus;
using SS = TekuSP.Drivers.Nano_OpenTherm.Enums.SlaveStatus;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Ventilation/Heat-recovery status flags
    /// </summary>
    public class GetVentilationStatusRequest : ReadRequest
    {
        public GetVentilationStatusRequest() : base() { }
        public GetVentilationStatusRequest(Request baseReq) : base(baseReq) { }

    // Expose enums as writable so callers can compose flags and have them encoded
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

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.StatusVentilationHeatRecovery;

    // Convenience flags for Master
    /// <summary>Central heating enabled on Master (bit 0).</summary>
    public bool MasterIsCentralHeatingActive { get => Utilities.IsSet(MasterStatus, MS.CHEnabled); set => Utilities.SetFlag(ref MasterStatus, MS.CHEnabled, value); }
    /// <summary>Domestic hot water enabled on Master (bit 1).</summary>
    public bool MasterIsHotWaterActive { get => Utilities.IsSet(MasterStatus, MS.DHWEnabled); set => Utilities.SetFlag(ref MasterStatus, MS.DHWEnabled, value); }
    /// <summary>Cooling enabled on Master (bit 2).</summary>
    public bool MasterIsCoolingActive { get => Utilities.IsSet(MasterStatus, MS.CoolingEnabled); set => Utilities.SetFlag(ref MasterStatus, MS.CoolingEnabled, value); }
    /// <summary>Outside temperature compensation (OTC) active on Master (bit 3).</summary>
    public bool MasterOTCActive { get => Utilities.IsSet(MasterStatus, MS.OTCActive); set => Utilities.SetFlag(ref MasterStatus, MS.OTCActive, value); }
    /// <summary>Second heating circuit enabled on Master (bit 4).</summary>
    public bool MasterIsCentralHeating2Active { get => Utilities.IsSet(MasterStatus, MS.CH2Enabled); set => Utilities.SetFlag(ref MasterStatus, MS.CH2Enabled, value); }

    // Convenience flags for Slave
    /// <summary>Fault indication present on Slave (bit 0).</summary>
    public bool SlaveIsFault { get => Utilities.IsSet(SlaveStatus, SS.FaultIndication); set => Utilities.SetFlag(ref SlaveStatus, SS.FaultIndication, value); }
    /// <summary>Central heating mode active on Slave (bit 1).</summary>
    public bool SlaveIsCentralHeatingActive { get => Utilities.IsSet(SlaveStatus, SS.CHMode); set => Utilities.SetFlag(ref SlaveStatus, SS.CHMode, value); }
    /// <summary>Domestic hot water mode active on Slave (bit 2).</summary>
    public bool SlaveIsHotWaterActive { get => Utilities.IsSet(SlaveStatus, SS.DHWMode); set => Utilities.SetFlag(ref SlaveStatus, SS.DHWMode, value); }
    /// <summary>Flame/burner on indicated on Slave (bit 3).</summary>
    public bool SlaveIsFlameOn { get => Utilities.IsSet(SlaveStatus, SS.FlameStatus); set => Utilities.SetFlag(ref SlaveStatus, SS.FlameStatus, value); }
    /// <summary>Cooling status active on Slave (bit 4).</summary>
    public bool SlaveIsCoolingActive { get => Utilities.IsSet(SlaveStatus, SS.CoolingStatus); set => Utilities.SetFlag(ref SlaveStatus, SS.CoolingStatus, value); }
    /// <summary>Second heating circuit mode active on Slave (bit 5).</summary>
    public bool SlaveIsCentralHeating2Active { get => Utilities.IsSet(SlaveStatus, SS.CH2Mode); set => Utilities.SetFlag(ref SlaveStatus, SS.CH2Mode, value); }
    /// <summary>Diagnostic indication on Slave (bit 6).</summary>
    public bool SlaveDiagnosticIndicationActive { get => Utilities.IsSet(SlaveStatus, SS.DiagnosticIndication); set => Utilities.SetFlag(ref SlaveStatus, SS.DiagnosticIndication, value); }
    }
}
