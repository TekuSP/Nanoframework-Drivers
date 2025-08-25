using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class CustomRequest : ReadWriteRequest
    {
        private uint data;
        public CustomRequest(MessageType messageType, MessageID messageID, uint data = 0)
        {
            this.data = data;
            MessageType = messageType;
            MessageID = messageID;
        }

        protected override uint GetRawDataCore() => ProcessRequest(data);
        protected override void SetRawDataCore(uint value) { data = value; }

        public override MessageType MessageType { get; }
        public override MessageID MessageID { get; }
    }
}
