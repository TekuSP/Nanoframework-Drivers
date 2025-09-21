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
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.StatusResponse);
        #region Public Constructors

        public SetBoilerStatusRequest() : base()
        {
        }

        public SetBoilerStatusRequest(Request baseReq) : base(baseReq)
        {
        }

        /// <summary>
        /// Convenience constructor to set master status flags.
        /// </summary>
        public SetBoilerStatusRequest(MS status)
        {
            MasterStatus = status;
        }

        #endregion Public Constructors

        #region Public Properties

        // IMasterStatus (boolean proxies)
        public bool MasterIsCentralHeating2Active { get => MasterStatus.IsSet(MS.CH2Enabled); set => MasterStatus = MasterStatus.SetFlag(MS.CH2Enabled, value); }

        public bool MasterIsCentralHeatingActive { get => MasterStatus.IsSet(MS.CHEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CHEnabled, value); }

        public bool MasterIsCoolingActive { get => MasterStatus.IsSet(MS.CoolingEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CoolingEnabled, value); }

        public bool MasterIsHotWaterActive { get => MasterStatus.IsSet(MS.DHWEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.DHWEnabled, value); }

        public bool MasterOTCActive { get => MasterStatus.IsSet(MS.OTCActive); set => MasterStatus = MasterStatus.SetFlag(MS.OTCActive, value); }

    public bool MasterSummerWinterMode { get => MasterStatus.IsSet(MS.SummerWinterMode); set => MasterStatus = MasterStatus.SetFlag(MS.SummerWinterMode, value); }

    public bool MasterDHWBlocking { get => MasterStatus.IsSet(MS.DHWBlocking); set => MasterStatus = MasterStatus.SetFlag(MS.DHWBlocking, value); }


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
            // v2.2 status exchange (ID 0): HB = MasterStatus, LB = SlaveStatus (0 for request)
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