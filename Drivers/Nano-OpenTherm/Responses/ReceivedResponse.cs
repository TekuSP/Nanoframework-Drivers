using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class ReceivedResponse : Response
    {
        private uint _raw;

        /// <summary>
        /// Initializes from a raw OpenTherm frame, decoding type and ID.
        /// </summary>
        public ReceivedResponse(uint rawData)
        {
            SetRawDataCore(rawData);
            MessageType = Utilities.GetMessageType(rawData);
            MessageID = Utilities.GetMessageID(rawData);
        }

        protected override uint GetRawDataCore() => _raw;
        protected override void SetRawDataCore(uint value) => _raw = value;

    public override MessageType MessageType { get; set; }
    public override MessageID MessageID { get; }
    }
}
