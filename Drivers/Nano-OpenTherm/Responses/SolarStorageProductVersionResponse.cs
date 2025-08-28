using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Solar Storage product version/type response.</summary>
    public class SolarStorageProductVersionResponse : ProductVersionTypeResponseBase
    {
        public SolarStorageProductVersionResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public SolarStorageProductVersionResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.SolarStorageVersion;
    }
}
