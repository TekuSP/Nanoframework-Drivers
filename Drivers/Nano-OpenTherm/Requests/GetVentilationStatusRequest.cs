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
            uint data = (uint)(((byte)SlaveStatus << 8) | (byte)MasterStatus);
            return ProcessRequest(data);
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
        public bool MasterIsCentralHeatingActive { get => (MasterStatus & MS.CHEnabled) != 0; set { if (value) MasterStatus |= MS.CHEnabled; else MasterStatus &= ~MS.CHEnabled; } }
    /// <summary>Domestic hot water enabled on Master (bit 1).</summary>
        public bool MasterIsHotWaterActive { get => (MasterStatus & MS.DHWEnabled) != 0; set { if (value) MasterStatus |= MS.DHWEnabled; else MasterStatus &= ~MS.DHWEnabled; } }
    /// <summary>Cooling enabled on Master (bit 2).</summary>
        public bool MasterIsCoolingActive { get => (MasterStatus & MS.CoolingEnabled) != 0; set { if (value) MasterStatus |= MS.CoolingEnabled; else MasterStatus &= ~MS.CoolingEnabled; } }
    /// <summary>Outside temperature compensation (OTC) active on Master (bit 3).</summary>
        public bool MasterOTCActive { get => (MasterStatus & MS.OTCActive) != 0; set { if (value) MasterStatus |= MS.OTCActive; else MasterStatus &= ~MS.OTCActive; } }
    /// <summary>Second heating circuit enabled on Master (bit 4).</summary>
        public bool MasterIsCentralHeating2Active { get => (MasterStatus & MS.CH2Enabled) != 0; set { if (value) MasterStatus |= MS.CH2Enabled; else MasterStatus &= ~MS.CH2Enabled; } }

    // Convenience flags for Slave
    /// <summary>Fault indication present on Slave (bit 0).</summary>
        public bool SlaveIsFault { get => (SlaveStatus & SS.FaultIndication) != 0; set { if (value) SlaveStatus |= SS.FaultIndication; else SlaveStatus &= ~SS.FaultIndication; } }
    /// <summary>Central heating mode active on Slave (bit 1).</summary>
        public bool SlaveIsCentralHeatingActive { get => (SlaveStatus & SS.CHMode) != 0; set { if (value) SlaveStatus |= SS.CHMode; else SlaveStatus &= ~SS.CHMode; } }
    /// <summary>Domestic hot water mode active on Slave (bit 2).</summary>
        public bool SlaveIsHotWaterActive { get => (SlaveStatus & SS.DHWMode) != 0; set { if (value) SlaveStatus |= SS.DHWMode; else SlaveStatus &= ~SS.DHWMode; } }
    /// <summary>Flame/burner on indicated on Slave (bit 3).</summary>
        public bool SlaveIsFlameOn { get => (SlaveStatus & SS.FlameStatus) != 0; set { if (value) SlaveStatus |= SS.FlameStatus; else SlaveStatus &= ~SS.FlameStatus; } }
    /// <summary>Cooling status active on Slave (bit 4).</summary>
        public bool SlaveIsCoolingActive { get => (SlaveStatus & SS.CoolingStatus) != 0; set { if (value) SlaveStatus |= SS.CoolingStatus; else SlaveStatus &= ~SS.CoolingStatus; } }
    /// <summary>Second heating circuit mode active on Slave (bit 5).</summary>
        public bool SlaveIsCentralHeating2Active { get => (SlaveStatus & SS.CH2Mode) != 0; set { if (value) SlaveStatus |= SS.CH2Mode; else SlaveStatus &= ~SS.CH2Mode; } }
    /// <summary>Diagnostic indication on Slave (bit 6).</summary>
        public bool SlaveDiagnosticIndicationActive { get => (SlaveStatus & SS.DiagnosticIndication) != 0; set { if (value) SlaveStatus |= SS.DiagnosticIndication; else SlaveStatus &= ~SS.DiagnosticIndication; } }
    }
}
