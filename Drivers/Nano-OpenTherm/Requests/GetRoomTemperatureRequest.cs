using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

using MS = TekuSP.Drivers.Nano_OpenTherm.Enums.MasterStatus;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the room temperature as reported over OpenTherm.
    /// </summary>
    /// <remarks>
    /// Response payload: low 16 bits as 8.8 fixed-point degrees Celsius.
    /// </remarks>
    public class GetRoomTemperatureRequest : ReadRequest, IMasterStatus
    {
        #region Public Constructors

        public GetRoomTemperatureRequest() : base()
        {
        }

        public GetRoomTemperatureRequest(Request baseReq) : base(baseReq)
        {
        }

        /// <summary>
        /// Convenience constructor to initialize master status (LB) bits.
        /// </summary>
        public GetRoomTemperatureRequest(MS status)
            : base()
        {
            MasterStatus = status;
        }

        #endregion Public Constructors

        #region Public Properties

        // Convenience flags using new extension methods
        // IMasterStatus
        public bool MasterIsCentralHeating2Active { get => MasterStatus.IsSet(MS.CH2Enabled); set => MasterStatus = MasterStatus.SetFlag(MS.CH2Enabled, value); }

        public bool MasterIsCentralHeatingActive { get => MasterStatus.IsSet(MS.CHEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CHEnabled, value); }
        public bool MasterIsCoolingActive { get => MasterStatus.IsSet(MS.CoolingEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CoolingEnabled, value); }
        public bool MasterIsHotWaterActive { get => MasterStatus.IsSet(MS.DHWEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.DHWEnabled, value); }
        public bool MasterOTCActive { get => MasterStatus.IsSet(MS.OTCActive); set => MasterStatus = MasterStatus.SetFlag(MS.OTCActive, value); }
    public bool MasterSummerWinterMode { get => MasterStatus.IsSet(MS.SummerWinterMode); set => MasterStatus = MasterStatus.SetFlag(MS.SummerWinterMode, value); }
    public bool MasterDHWBlocking { get => MasterStatus.IsSet(MS.DHWBlocking); set => MasterStatus = MasterStatus.SetFlag(MS.DHWBlocking, value); }
        public bool MasterReserved7 { get => MasterStatus.IsSet(MS.Reserved7); set => MasterStatus = MasterStatus.SetFlag(MS.Reserved7, value); }
        public override MessageID MessageID => MessageID.Tr;
        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Properties

        protected MS MasterStatus { get; set; }

        #endregion Protected Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            byte low = Utilities.SetMasterStatus(MasterStatus);
            return ProcessRequest(low);
        }

        protected override void SetRawDataCore(uint value)
        {
            MasterStatus = Utilities.GetMasterStatus(value);
        }

        #endregion Protected Methods
    }
}