using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class CHPressureResponse : Response
    {
        #region Public Constructors

        public CHPressureResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public CHPressureResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.CHPressure;

        public override MessageType MessageType { get; set; }

        /// <summary>CH water pressure.</summary>
        public Pressure Pressure { get; set; }

        #endregion Public Properties

        #region Protected Methods

        // Encoded as F8.8 (0..100 bar engineering clamp via legacy GetRawTemperature alias -> GetRawF88).
        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawF88((float)Pressure.Bars, 0, 100));

        protected override void SetRawDataCore(uint value) => Pressure = Pressure.FromBars(Utilities.GetFloat(value));

        #endregion Protected Methods
    }
}