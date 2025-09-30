using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the instantaneous electricity production (Watts) from the device.
    /// </summary>
    public class GetElectricityProductionRequest : ReadRequest
    {
        #region Public Constructors

        public GetElectricityProductionRequest() : base()
        {
        }

        public GetElectricityProductionRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.ElectricityProductionResponse);

        public override MessageID MessageID => MessageID.ElectricityProduction;

        public override MessageType MessageType => MessageType.READ_DATA;

        /// <summary>Instantaneous power production.</summary>
        public Power Power { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest((ushort)Power.Watts);

        protected override void SetRawDataCore(uint value)
        { Power = Power.FromWatts(Utilities.GetLowUShort(value)); }

        #endregion Protected Methods
    }
}