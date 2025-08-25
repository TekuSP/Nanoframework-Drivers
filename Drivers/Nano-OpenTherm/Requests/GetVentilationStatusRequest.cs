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
        public MS MasterStatus { get; set; }
        public SS SlaveStatus { get; set; }

        protected override ulong GetRawDataCore()
        {
            // Pack master in low byte and slave in high byte
            uint data = (uint)(((byte)SlaveStatus << 8) | (byte)MasterStatus);
            return ProcessRequest(data);
        }
        protected override void SetRawDataCore(ulong value)
        {
            MasterStatus = Utilities.GetMasterStatus(value);
            SlaveStatus = Utilities.GetSlaveStatus(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.StatusVentilationHeatRecovery;

        // Convenience flags for Master
        public bool MasterIsCentralHeatingActive { get => (MasterStatus & MS.CHEnabled) != 0; set { if (value) MasterStatus |= MS.CHEnabled; else MasterStatus &= ~MS.CHEnabled; } }
        public bool MasterIsHotWaterActive { get => (MasterStatus & MS.DHWEnabled) != 0; set { if (value) MasterStatus |= MS.DHWEnabled; else MasterStatus &= ~MS.DHWEnabled; } }
        public bool MasterIsCoolingActive { get => (MasterStatus & MS.CoolingEnabled) != 0; set { if (value) MasterStatus |= MS.CoolingEnabled; else MasterStatus &= ~MS.CoolingEnabled; } }
        public bool MasterOTCActive { get => (MasterStatus & MS.OTCActive) != 0; set { if (value) MasterStatus |= MS.OTCActive; else MasterStatus &= ~MS.OTCActive; } }
        public bool MasterIsCentralHeating2Active { get => (MasterStatus & MS.CH2Enabled) != 0; set { if (value) MasterStatus |= MS.CH2Enabled; else MasterStatus &= ~MS.CH2Enabled; } }

        // Convenience flags for Slave
        public bool SlaveIsFault { get => (SlaveStatus & SS.FaultIndication) != 0; set { if (value) SlaveStatus |= SS.FaultIndication; else SlaveStatus &= ~SS.FaultIndication; } }
        public bool SlaveIsCentralHeatingActive { get => (SlaveStatus & SS.CHMode) != 0; set { if (value) SlaveStatus |= SS.CHMode; else SlaveStatus &= ~SS.CHMode; } }
        public bool SlaveIsHotWaterActive { get => (SlaveStatus & SS.DHWMode) != 0; set { if (value) SlaveStatus |= SS.DHWMode; else SlaveStatus &= ~SS.DHWMode; } }
        public bool SlaveIsFlameOn { get => (SlaveStatus & SS.FlameStatus) != 0; set { if (value) SlaveStatus |= SS.FlameStatus; else SlaveStatus &= ~SS.FlameStatus; } }
        public bool SlaveIsCoolingActive { get => (SlaveStatus & SS.CoolingStatus) != 0; set { if (value) SlaveStatus |= SS.CoolingStatus; else SlaveStatus &= ~SS.CoolingStatus; } }
        public bool SlaveIsCentralHeating2Active { get => (SlaveStatus & SS.CH2Mode) != 0; set { if (value) SlaveStatus |= SS.CH2Mode; else SlaveStatus &= ~SS.CH2Mode; } }
        public bool SlaveDiagnosticIndicationActive { get => (SlaveStatus & SS.DiagnosticIndication) != 0; set { if (value) SlaveStatus |= SS.DiagnosticIndication; else SlaveStatus &= ~SS.DiagnosticIndication; } }
    }
}
