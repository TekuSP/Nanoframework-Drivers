using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using MS = TekuSP.Drivers.Nano_OpenTherm.Enums.MasterStatus;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetRoomTemperatureCH2Request : ReadRequest
    {
        public GetRoomTemperatureCH2Request() : base() { }
        public GetRoomTemperatureCH2Request(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Master status flags (low byte). Use convenience properties to read/write individual bits.
    /// </summary>
    public MS MasterStatus { get; set; }

        protected override uint GetRawDataCore()
        {
            uint data = (uint)(byte)MasterStatus;
            return ProcessRequest(data);
        }
        protected override void SetRawDataCore(uint value)
        {
            MasterStatus = Utilities.GetMasterStatus(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.TrCH2;

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
    }
}
