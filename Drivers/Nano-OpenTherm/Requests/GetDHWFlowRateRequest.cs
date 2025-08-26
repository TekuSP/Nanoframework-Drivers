using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using MS = TekuSP.Drivers.Nano_OpenTherm.Enums.MasterStatus;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the domestic hot water (DHW) flow rate.
    /// </summary>
    /// <remarks>
    /// Response payload: device-specific encoding, commonly 8.8 fixed-point liters/min.
    /// </remarks>
    public class GetDHWFlowRateRequest : ReadRequest
    {
        public GetDHWFlowRateRequest() : base() { }
        public GetDHWFlowRateRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Master status flags (encoded in the low byte). Use convenience properties
    /// to test individual status bits like CH/DHW/Cooling.
    /// </summary>
    public MS MasterStatus { get; set; }

        protected override uint GetRawDataCore()
        {
            byte low = Utilities.SetMasterStatus(MasterStatus);
            return ProcessRequest(low);
        }
        protected override void SetRawDataCore(uint value)
        {
            MasterStatus = Utilities.GetMasterStatus(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.DHWFlowRate;

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
    }
}
