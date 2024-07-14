using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    internal class RemoteOverrideRoomSetPointResponse : Response
    {
        public RemoteOverrideRoomSetPointResponse(Response baseResponse)
        {
            RawData = baseResponse.RawData;
            MessageType = baseResponse.MessageType;
            MessageID = baseResponse.MessageID;
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
        /// Remote override room setpoint
        /// </summary>
        public float TrOverride => Utilities.GetFloat(RawData);
    }
}
