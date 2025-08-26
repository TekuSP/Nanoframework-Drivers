using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using MS = TekuSP.Drivers.Nano_OpenTherm.Enums.MasterStatus;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the remote request/status bits from the master.
    /// </summary>
    public class GetRemoteRequestRequest : ReadRequest
    {
        public GetRemoteRequestRequest() : base() { }
        public GetRemoteRequestRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Master status flags (low byte). Use convenience properties to test individual bits.
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
        public override MessageID MessageID => MessageID.RemoteRequest;

        // Convenience flags
        /// <summary>Central Heating enabled on Master.</summary>
        public bool MasterIsCentralHeatingActive { get => MasterStatus.IsSet(MS.CHEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CH2Enabled, value); }
        /// <summary>Hot Water (DHW) enabled on Master.</summary>
        public bool MasterIsHotWaterActive { get => MasterStatus.IsSet(MS.DHWEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.DHWEnabled, value); }
        /// <summary>Cooling enabled on Master.</summary>
        public bool MasterIsCoolingActive { get => MasterStatus.IsSet(MS.CoolingEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CoolingEnabled, value); }
        /// <summary>OpenTherm Continuous modulation/OTC active on Master.</summary>
        public bool MasterOTCActive { get => MasterStatus.IsSet(MS.OTCActive); set => MasterStatus = MasterStatus.SetFlag(MS.OTCActive, value); }
        /// <summary>Central Heating 2 enabled on Master.</summary>
        public bool MasterIsCentralHeating2Active { get => MasterStatus.IsSet(MS.CH2Enabled); set => MasterStatus = MasterStatus.SetFlag(MS.CH2Enabled, value); }
    }
}
