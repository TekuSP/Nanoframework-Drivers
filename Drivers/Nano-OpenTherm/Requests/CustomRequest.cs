using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class CustomRequest : Request
    {
        private ulong data;
        public CustomRequest(MessageType messageType, MessageID messageID, ulong data = 0)
        {
            this.data = data;
            MessageType = messageType;
            MessageID = messageID;
        }
        public override ulong RawData
        {
            get
            {
                return ProcessRequest(data);
            }
            set
            {
            
            }
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
