using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Solar storage Fault History Buffer entry response (index/value packed in low 16 bits).
    /// </summary>
    public class SolarStorageFHBEntryResponse : UShortValueResponseBase
    {
        public SolarStorageFHBEntryResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public SolarStorageFHBEntryResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.FHBindexFHBvalueSolarStorage;
    }
}
