using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// CO2 in exhaust measured value. Raw U16 value in parts-per-million per spec (ID79).
    /// </summary>
    public class CO2ExhaustResponse : UShortValueResponseBase
    {
        #region Public Constructors

        public CO2ExhaustResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public CO2ExhaustResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// CO₂ concentration in exhaust air (ID79) specified as U16 0..10000 ppm in v2.3b spec.
        /// Underlying raw value is direct ppm (no scaling); expose as UnitsNet.VolumeConcentration.
        /// </summary>
        public VolumeConcentration CO2
        {
            get => VolumeConcentration.FromPartsPerMillion(Value);
            set => Value = (ushort)System.Math.Clamp((int)System.Math.Round(value.PartsPerMillion), 0, 10000);
        }

        public override MessageID MessageID => MessageID.CO2exhaust;
        public override MessageType MessageType { get; set; }

        #endregion Public Properties
    }
}