using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class StatusResponse : Response
    {
        public StatusResponse(Response baseResponse)
        {
            RawData = baseResponse.RawData;
            MessageType = baseResponse.MessageType;
            MessageID = baseResponse.MessageID;
            MasterStatus = Utilities.GetMasterStatus(RawData);
            SlaveStatus = Utilities.GetSlaveStatus(RawData);
        }
        private MasterStatus MasterStatus
        {
            get; 
        }
        private SlaveStatus SlaveStatus
        {
            get;
        }
        public override ulong RawData
        {
            get;
            set;
        }

        public override MessageType MessageType
        {
            get;
        }

        public override MessageID MessageID
        {
            get;
        }

        /// <summary>
        /// Is central heating active
        /// </summary>
        public bool MasterIsCentralHeatingActive => (MasterStatus & MasterStatus.CHEnabled) == MasterStatus.CHEnabled;
        /// <summary>
        /// Is hot water active
        /// </summary>
        public bool MasterIsHotWaterActive => (MasterStatus & MasterStatus.DHWEnabled) == MasterStatus.DHWEnabled;
        /// <summary>
        /// Is cooling active
        /// </summary>
        public bool MasterIsCoolingActive => (MasterStatus & MasterStatus.CoolingEnabled) == MasterStatus.CoolingEnabled;
        /// <summary>
        /// Is OTC Active
        /// </summary>
        public bool MasterOTCActive => (MasterStatus & MasterStatus.OTCActive) == MasterStatus.OTCActive;
        /// <summary>
        /// Is central heating 2 active
        /// </summary>
        public bool MasterIsCentralHeating2Active => (MasterStatus & MasterStatus.CH2Enabled) == MasterStatus.CH2Enabled;

        /// <summary>
        /// Is fault recorded
        /// </summary>
        public bool SlaveIsFault => (SlaveStatus & SlaveStatus.FaultIndication) == SlaveStatus.FaultIndication;
        /// <summary>
        /// Is central heating active
        /// </summary>
        public bool SlaveIsCentralHeatingActive => (SlaveStatus & SlaveStatus.CHMode) == SlaveStatus.CHMode;
        /// <summary>
        /// Is hot water active
        /// </summary>
        public bool SlaveIsHotWaterActive => (SlaveStatus & SlaveStatus.DHWMode) == SlaveStatus.DHWMode;
        /// <summary>
        /// Is flame on
        /// </summary>
        public bool SlaveIsFlameOn => (SlaveStatus & SlaveStatus.FlameStatus) == SlaveStatus.FlameStatus;
        /// <summary>
        /// Is cooling active
        /// </summary>
        public bool SlaveIsCoolingActive => (SlaveStatus & SlaveStatus.CoolingStatus) == SlaveStatus.CoolingStatus;
        /// <summary>
        /// Is central heating 2 active
        /// </summary>
        public bool SlaveIsCentralHeating2Active => (SlaveStatus & SlaveStatus.CH2Mode) == SlaveStatus.CH2Mode;
        /// <summary>
        /// Is there diagnostic event
        /// </summary>
        public bool SlaveDiagnosticIndicationActive => (SlaveStatus & SlaveStatus.DiagnosticIndication) == SlaveStatus.DiagnosticIndication;

    }
}
