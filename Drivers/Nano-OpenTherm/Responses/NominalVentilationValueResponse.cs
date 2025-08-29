using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Nominal ventilation value as percent (0..100). Project decision: U8 in low byte (ID87).
    /// TODO: If an implementation needs OEM-specific units, restore UShortValueResponseBase.
    /// </summary>
    public class NominalVentilationValueResponse : Response
    {
        public NominalVentilationValueResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public NominalVentilationValueResponse(Response r) : base(r) { }

        public byte Percent { get; set; }

        protected override uint GetRawDataCore() => ProcessResponse(Utilities.MakeUShort(0, Percent));
        protected override void SetRawDataCore(uint value) => Percent = Utilities.GetLowByte(value);

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.NominalVentilationValue;
    }
}
