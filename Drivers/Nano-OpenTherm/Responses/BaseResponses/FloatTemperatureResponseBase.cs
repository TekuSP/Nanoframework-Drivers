using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses
{
    /// <summary>
    /// Small helper base for responses with a single 8.8 fixed-point temperature payload.
    /// </summary>
    public abstract class FloatTemperatureResponseBase : Response
    {
    protected FloatTemperatureResponseBase() { }
    protected FloatTemperatureResponseBase(Response baseResponse) : base(baseResponse) { }
        /// <summary>Temperature value in Celsius.</summary>
        public float TemperatureC { get; set; }

        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawTemperature(TemperatureC));
        protected override void SetRawDataCore(uint value) => TemperatureC = Utilities.GetFloat(value);

        public override MessageType MessageType { get; set; }
    }
}
