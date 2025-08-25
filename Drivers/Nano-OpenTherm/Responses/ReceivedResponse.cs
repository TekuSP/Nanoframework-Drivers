using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class ReceivedResponse : Response
    {
        /// <summary>
        /// Initializes RawResponse
        /// </summary>
        public ReceivedResponse(uint rawData)
        {
            RawData = rawData;
            MessageType = (MessageType)(rawData >> 28 & 7);
            MessageID = (MessageID)(rawData >> 16 & 0xFF);
        }

        public override uint RawData
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
