using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Number of hours that the slave is in Cooling Mode
    /// </summary>
    public class GetCoolingOperationHoursRequest : ReadRequest
    {
        public GetCoolingOperationHoursRequest() : base() { }
        public GetCoolingOperationHoursRequest(Request baseReq) : base(baseReq) { }

        public ushort Hours { get; private set; }

        protected override ulong GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(ulong value)
        {
            Hours = Utilities.GetLowUShort(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.CoolingOperationHours;
    }
}
