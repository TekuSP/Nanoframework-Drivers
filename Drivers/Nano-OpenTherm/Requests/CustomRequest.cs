using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class CustomRequest : ReadWriteRequest
    {
        private ulong data;
        public CustomRequest(MessageType messageType, MessageID messageID, ulong data = 0)
        {
            this.data = data;
            MessageType = messageType;
            MessageID = messageID;
        }

        protected override ulong GetRawDataCore() => ProcessRequest(data);
        protected override void SetRawDataCore(ulong value) { data = value; }

        public override MessageType MessageType { get; }
        public override MessageID MessageID { get; }
    }
}
