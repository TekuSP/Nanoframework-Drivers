using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class SolarStorageTSPCountResponse : UShortValueResponseBase
    {
        public SolarStorageTSPCountResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public SolarStorageTSPCountResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.TSPSolarStorage;
    }
}
