using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using MS = TekuSP.Drivers.Nano_OpenTherm.Enums.MasterStatus;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetDHWFlowRateRequest : ReadRequest
    {
        public GetDHWFlowRateRequest() : base() { }
        public GetDHWFlowRateRequest(Request baseReq) : base(baseReq) { }

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
        public override MessageID MessageID => MessageID.DHWFlowRate;

        public bool MasterIsCentralHeatingActive { get => (MasterStatus & MS.CHEnabled) != 0; set { if (value) MasterStatus |= MS.CHEnabled; else MasterStatus &= ~MS.CHEnabled; } }
        public bool MasterIsHotWaterActive { get => (MasterStatus & MS.DHWEnabled) != 0; set { if (value) MasterStatus |= MS.DHWEnabled; else MasterStatus &= ~MS.DHWEnabled; } }
        public bool MasterIsCoolingActive { get => (MasterStatus & MS.CoolingEnabled) != 0; set { if (value) MasterStatus |= MS.CoolingEnabled; else MasterStatus &= ~MS.CoolingEnabled; } }
        public bool MasterOTCActive { get => (MasterStatus & MS.OTCActive) != 0; set { if (value) MasterStatus |= MS.OTCActive; else MasterStatus &= ~MS.OTCActive; } }
        public bool MasterIsCentralHeating2Active { get => (MasterStatus & MS.CH2Enabled) != 0; set { if (value) MasterStatus |= MS.CH2Enabled; else MasterStatus &= ~MS.CH2Enabled; } }
    }
}
