using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class DHWFlowRateResponse : Response
    {
        #region Public Constructors

        public DHWFlowRateResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public DHWFlowRateResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>DHW flow rate.</summary>
        public VolumeFlow Flow { get; set; }

        public override MessageID MessageID => MessageID.DHWFlowRate;

        public override MessageType MessageType { get; set; }

        #endregion Public Properties

        #region Protected Methods

        // Encoded as F8.8 L/min using legacy temperature encoder (maps now to generic GetRawF88).
        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawF88((float)Flow.LitersPerMinute, 0, 100));

        protected override void SetRawDataCore(uint value) => Flow = VolumeFlow.FromLitersPerMinute(Utilities.GetFloat(value));

        #endregion Protected Methods
    }
}