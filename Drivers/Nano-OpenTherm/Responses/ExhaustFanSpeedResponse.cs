using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Exhaust fan speed in RPM (ushort).</summary>
    public class ExhaustFanSpeedResponse : UShortValueResponseBase
    {
        #region Public Constructors

        public ExhaustFanSpeedResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public ExhaustFanSpeedResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.RPMexhaust;

        /// <summary>Exhaust fan rotational speed.</summary>
        public RotationalSpeed Speed
        {
            get => RotationalSpeed.FromRevolutionsPerMinute(Value);
            set => Value = (ushort)value.RevolutionsPerMinute;
        }

        #endregion Public Properties
    }
}