using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses
{
    /// <summary>
    /// Base for 16-bit value responses (payload is a single ushort).
    /// </summary>
    public abstract class UShortValueResponseBase : Response
    {
    protected UShortValueResponseBase() { }
    protected UShortValueResponseBase(Response baseResponse) : base(baseResponse) { }
        public ushort Value { get; set; }
        protected override uint GetRawDataCore() => ProcessResponse(Value);
        protected override void SetRawDataCore(uint value) => Value = Utilities.GetLowUShort(value);
        public override MessageType MessageType { get; set; }
    }
}
