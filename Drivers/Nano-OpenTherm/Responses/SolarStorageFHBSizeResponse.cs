using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class SolarStorageFHBSizeResponse : UShortValueResponseBase
    {
        public SolarStorageFHBSizeResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public SolarStorageFHBSizeResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.FHBsizeSolarStorage;
    }
}
