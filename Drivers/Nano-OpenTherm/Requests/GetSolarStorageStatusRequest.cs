using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using MS = TekuSP.Drivers.Nano_OpenTherm.Enums.MasterStatus;
using SS = TekuSP.Drivers.Nano_OpenTherm.Enums.SlaveStatus;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
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
            uint data = (uint)(((byte)SlaveStatus << 8) | (byte)MasterStatus);
            return ProcessRequest(data);
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
    public bool MasterIsCentralHeatingActive { get => (MasterStatus & MS.CHEnabled) != 0; set { if (value) MasterStatus |= MS.CHEnabled; else MasterStatus &= ~MS.CHEnabled; } }
    /// <summary>Hot Water (DHW) enabled on Master.</summary>
    public bool MasterIsHotWaterActive { get => (MasterStatus & MS.DHWEnabled) != 0; set { if (value) MasterStatus |= MS.DHWEnabled; else MasterStatus &= ~MS.DHWEnabled; } }
    /// <summary>Cooling enabled on Master.</summary>
    public bool MasterIsCoolingActive { get => (MasterStatus & MS.CoolingEnabled) != 0; set { if (value) MasterStatus |= MS.CoolingEnabled; else MasterStatus &= ~MS.CoolingEnabled; } }
    /// <summary>OpenTherm Continuous modulation/OTC active on Master.</summary>
    public bool MasterOTCActive { get => (MasterStatus & MS.OTCActive) != 0; set { if (value) MasterStatus |= MS.OTCActive; else MasterStatus &= ~MS.OTCActive; } }
    /// <summary>Central Heating 2 enabled on Master.</summary>
    public bool MasterIsCentralHeating2Active { get => (MasterStatus & MS.CH2Enabled) != 0; set { if (value) MasterStatus |= MS.CH2Enabled; else MasterStatus &= ~MS.CH2Enabled; } }

    // Slave convenience flags
    /// <summary>Slave fault indicator.</summary>
    public bool SlaveIsFault { get => (SlaveStatus & SS.FaultIndication) != 0; set { if (value) SlaveStatus |= SS.FaultIndication; else SlaveStatus &= ~SS.FaultIndication; } }
    /// <summary>Central Heating mode active on Slave.</summary>
    public bool SlaveIsCentralHeatingActive { get => (SlaveStatus & SS.CHMode) != 0; set { if (value) SlaveStatus |= SS.CHMode; else SlaveStatus &= ~SS.CHMode; } }
    /// <summary>Hot Water mode active on Slave.</summary>
    public bool SlaveIsHotWaterActive { get => (SlaveStatus & SS.DHWMode) != 0; set { if (value) SlaveStatus |= SS.DHWMode; else SlaveStatus &= ~SS.DHWMode; } }
    /// <summary>Flame status active on Slave.</summary>
    public bool SlaveIsFlameOn { get => (SlaveStatus & SS.FlameStatus) != 0; set { if (value) SlaveStatus |= SS.FlameStatus; else SlaveStatus &= ~SS.FlameStatus; } }
    /// <summary>Cooling active on Slave.</summary>
    public bool SlaveIsCoolingActive { get => (SlaveStatus & SS.CoolingStatus) != 0; set { if (value) SlaveStatus |= SS.CoolingStatus; else SlaveStatus &= ~SS.CoolingStatus; } }
    /// <summary>Central Heating 2 mode active on Slave.</summary>
    public bool SlaveIsCentralHeating2Active { get => (SlaveStatus & SS.CH2Mode) != 0; set { if (value) SlaveStatus |= SS.CH2Mode; else SlaveStatus &= ~SS.CH2Mode; } }
    /// <summary>Diagnostic indication active on Slave.</summary>
    public bool SlaveDiagnosticIndicationActive { get => (SlaveStatus & SS.DiagnosticIndication) != 0; set { if (value) SlaveStatus |= SS.DiagnosticIndication; else SlaveStatus &= ~SS.DiagnosticIndication; } }
    }
}
