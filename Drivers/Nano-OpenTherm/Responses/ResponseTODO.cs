
using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    internal class ResponseTODO : Response
    {
        /// <summary>
        /// Is fault recorded
        /// </summary>
        public bool IsFault => (RawData & 0x1) == 1;
        /// <summary>
        /// Is central heating active
        /// </summary>
        public bool IsCentralHeatingActive => (RawData & 0x2) == 1;
        /// <summary>
        /// Is hot water active
        /// </summary>
        public bool IsHotWaterActive => (RawData & 0x4) == 1;
        /// <summary>
        /// Is flame on
        /// </summary>
        public bool IsFlameOn => (RawData & 0x8) == 1;
        /// <summary>
        /// Is cooling active
        /// </summary>
        public bool IsCoolingActive => (RawData & 0x10) == 1;
        /// <summary>
        /// Is in diagnostic mode
        /// </summary>
        public bool IsDiagnostic => (RawData & 0x40) == 1;

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
    }
}
