using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class SolarStorageTSPValueResponse : UShortValueResponseBase
    {
        public SolarStorageTSPValueResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public SolarStorageTSPValueResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.TSPindexTSPvalueSolarStorage;
    }
}
