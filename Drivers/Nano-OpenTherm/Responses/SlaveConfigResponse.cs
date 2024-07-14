using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class SlaveConfigResponse : Response
    {
        public SlaveConfigResponse(Response baseResponse)
        {
            RawData = baseResponse.RawData;
            MessageType = baseResponse.MessageType;
            MessageID = baseResponse.MessageID;
            SlaveConfiguration = Utilities.GetSlaveConfiguration(RawData);
        }
        private SlaveConfiguration SlaveConfiguration
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
        /// DHW (Domestic Hot Water) present.
        /// </summary>
        public bool DHWPresent => (SlaveConfiguration & SlaveConfiguration.DHWPresent) == SlaveConfiguration.DHWPresent;
        /// <summary>
        /// Control type.
        /// </summary>
        public bool ControlType => (SlaveConfiguration & SlaveConfiguration.ControlType) == SlaveConfiguration.ControlType;
        /// <summary>
        /// Cooling configuration.
        /// </summary>
        public bool CoolingConfig => (SlaveConfiguration & SlaveConfiguration.CoolingConfig) == SlaveConfiguration.CoolingConfig;
        /// <summary>
        /// DHW configuration.
        /// </summary>
        public bool DHWConfig => (SlaveConfiguration & SlaveConfiguration.DHWConfig) == SlaveConfiguration.DHWConfig;
        /// <summary>
        /// Master low-off & pump control function.
        /// </summary>
        public bool MasterLowOffPumpControl => (SlaveConfiguration & SlaveConfiguration.MasterLowOffPumpControl) == SlaveConfiguration.MasterLowOffPumpControl;
        /// <summary>
        /// CH2 (Central Heating circuit 2) present.
        /// </summary>
        public bool CH2Present => (SlaveConfiguration & SlaveConfiguration.CH2Present) == SlaveConfiguration.CH2Present;
        /// <summary>
        /// Slave MemberID Code
        /// </summary>
        public byte MemberIDCode => Utilities.GetHighByte(RawData);
    }
}
