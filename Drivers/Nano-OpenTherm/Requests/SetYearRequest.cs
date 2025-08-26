using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Calendar year
    /// </summary>
    public class SetYearRequest : WriteRequest
    {
        public SetYearRequest() : base() { }
        public SetYearRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Calendar year encoded in the low 16 bits of the payload.
    /// Typical valid range: 0..4095 (implementation specific).
    /// </summary>
    public ushort Year { get; set; }

        protected override uint GetRawDataCore()
        {
            // year in low 16 bits (spec uses 0..4095 typically), keep raw low 16
            return ProcessRequest(Year);
        }
        protected override void SetRawDataCore(uint value)
        {
            Year = Utilities.GetLowUShort(value);
        }

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.Year;
    }
}
