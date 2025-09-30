using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses
{
    /// <summary>
    /// Small helper base for responses with a single 8.8 fixed-point temperature payload.
    /// </summary>
    public abstract class FloatTemperatureResponseBase : Response
    {
        #region Protected Constructors

        protected FloatTemperatureResponseBase()
        { }

        protected FloatTemperatureResponseBase(Response baseResponse) : base(baseResponse)
        {
        }

        #endregion Protected Constructors

        #region Public Properties

        public override MessageType MessageType { get; set; }

        /// <summary>Temperature value with full unit context.</summary>
        public Temperature Temperature { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawF88((float)Temperature.DegreesCelsius, 0, 100));

        protected override void SetRawDataCore(uint value) => Temperature = Temperature.FromDegreesCelsius(Utilities.GetFloat(value));

        #endregion Protected Methods
    }
}