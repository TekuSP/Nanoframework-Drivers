using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class ReceivedRequest : Request
    {
        private ulong _raw;

        public ReceivedRequest(ulong rawData)
        {
            _raw = rawData;
            MessageType = (MessageType)((rawData >> 28) & 7);
            MessageID = (MessageID)((rawData >> 16) & 0xFF);
        }

        protected override ulong GetRawDataCore() => _raw;
        protected override void SetRawDataCore(ulong value) { _raw = value; }

        public override MessageType MessageType { get; }
        public override MessageID MessageID { get; }
    }
}
