using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class CumulativeElectricityProductionResponse : UShortValueResponseBase
    {
        #region Public Constructors

        public CumulativeElectricityProductionResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public CumulativeElectricityProductionResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>Cumulative energy production total. Raw encoding: low 16 bits unsigned kilowatt-hours (kWh).</summary>
        public Energy Energy
        {
            get => Energy.FromKilowattHours(Value);
            set => Value = (ushort)value.KilowattHours; // truncation acceptable for 16-bit rolling counter
        }

        public override MessageID MessageID => MessageID.CumulativElectricityProduction;

        #endregion Public Properties
    }
}