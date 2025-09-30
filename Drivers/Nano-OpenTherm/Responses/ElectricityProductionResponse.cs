using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class ElectricityProductionResponse : UShortValueResponseBase
    {
        #region Public Constructors

        public ElectricityProductionResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public ElectricityProductionResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.ElectricityProduction;

        /// <summary>Instantaneous electrical power production. Raw encoding: low 16 bits unsigned Watts.</summary>
        public Power Power
        {
            get => Power.FromWatts(Value);
            set => Value = (ushort)value.Watts; // truncation acceptable within 0-65535 W spec range
        }

        #endregion Public Properties
    }
}