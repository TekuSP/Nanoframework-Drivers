using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class ReceivedRequest : Request
    {
        public ReceivedRequest(ulong rawData)
        {
            RawData = rawData;
            MessageType = (MessageType)((rawData >> 28) & 7);
            MessageID = (MessageID)((rawData >> 16) & 0xFF);
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
    }
}
