using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the cumulative electricity production total from the device.
    /// </summary>
    /// <remarks>
    /// Response payload: low 16 bits as an unsigned value representing kilowatt-hours (kWh).
    /// </remarks>
    public class GetCumulativeElectricityProductionRequest : ReadRequest
    {
        #region Public Constructors

        public GetCumulativeElectricityProductionRequest() : base()
        {
        }

        public GetCumulativeElectricityProductionRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>Cumulative energy production.</summary>
        public Energy Energy { get; set; }

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.CumulativeElectricityProductionResponse);
        public override MessageID MessageID => MessageID.CumulativElectricityProduction;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest((ushort)Energy.KilowattHours);

        protected override void SetRawDataCore(uint value)
        { Energy = Energy.FromKilowattHours(Utilities.GetLowUShort(value)); }

        #endregion Protected Methods
    }
}