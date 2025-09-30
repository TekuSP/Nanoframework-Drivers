using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Supply fan speed in RPM (ushort).</summary>
    public class SupplyFanSpeedResponse : UShortValueResponseBase
    {
        #region Public Constructors

        public SupplyFanSpeedResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public SupplyFanSpeedResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.RPMsupply;

        /// <summary>Supply fan rotational speed.</summary>
        public RotationalSpeed Speed
        {
            get => RotationalSpeed.FromRevolutionsPerMinute(Value);
            set => Value = (ushort)value.RevolutionsPerMinute;
        }

        #endregion Public Properties
    }
}