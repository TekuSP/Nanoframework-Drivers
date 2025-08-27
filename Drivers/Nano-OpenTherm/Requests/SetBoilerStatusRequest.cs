using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

using MS = TekuSP.Drivers.Nano_OpenTherm.Enums.MasterStatus;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets master status flags: CH/DHW/Cooling enable, OTC active, and CH2 enable.
    /// </summary>
    public class SetBoilerStatusRequest : WriteRequest, IMasterStatus
    {
        #region Public Constructors

        public SetBoilerStatusRequest() : base()
        {
        }

        public SetBoilerStatusRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        // IMasterStatus (boolean proxies)
        public bool MasterIsCentralHeating2Active { get => MasterStatus.IsSet(MS.CH2Enabled); set => MasterStatus = MasterStatus.SetFlag(MS.CH2Enabled, value); }

        public bool MasterIsCentralHeatingActive { get => MasterStatus.IsSet(MS.CHEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CHEnabled, value); }

        public bool MasterIsCoolingActive { get => MasterStatus.IsSet(MS.CoolingEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CoolingEnabled, value); }

        public bool MasterIsHotWaterActive { get => MasterStatus.IsSet(MS.DHWEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.DHWEnabled, value); }

        public bool MasterOTCActive { get => MasterStatus.IsSet(MS.OTCActive); set => MasterStatus = MasterStatus.SetFlag(MS.OTCActive, value); }

        public bool MasterReserved5 { get => MasterStatus.IsSet(MS.Reserved5); set => MasterStatus = MasterStatus.SetFlag(MS.Reserved5, value); }

        public bool MasterReserved6 { get => MasterStatus.IsSet(MS.Reserved6); set => MasterStatus = MasterStatus.SetFlag(MS.Reserved6, value); }

        public bool MasterReserved7 { get => MasterStatus.IsSet(MS.Reserved7); set => MasterStatus = MasterStatus.SetFlag(MS.Reserved7, value); }

        public override MessageID MessageID => MessageID.Status;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        #endregion Public Properties

        #region Protected Properties

        /// <summary>
        /// Master status flags to write (encoded in the high byte of payload).
        /// Single source of truth for IMasterStatus.
        /// </summary>
        protected MS MasterStatus { get; set; }

        #endregion Protected Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            // Place the master flags in the high byte
            ushort payload = Utilities.MakeUShort(Utilities.SetMasterStatus(MasterStatus), 0);
            return ProcessRequest(payload);
        }

        protected override void SetRawDataCore(uint value)
        {
            // Decode flags from the high byte into MasterStatus
            MasterStatus = (MS)Utilities.GetHighByte(value);
        }

        #endregion Protected Methods
    }
}