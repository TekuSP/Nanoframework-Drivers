using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the boiler maximum capacity and the minimum modulation level.
    /// </summary>
    /// <remarks>
    /// Response payload: two bytes where high byte is max capacity in kW (manufacturer specific scaling)
    /// and low byte is minimum relative modulation level in percent (0–100%).
    /// </remarks>
    public class GetBoilerCapacityAndMinModRequest : ReadRequest
    {
        public GetBoilerCapacityAndMinModRequest() : base() { }
        public GetBoilerCapacityAndMinModRequest(Request baseReq) : base(baseReq) { }

        protected override uint GetRawDataCore() => ProcessRequest(0);
        protected override void SetRawDataCore(uint value) { }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.MaxCapacityMinModLevel;
    }
}
