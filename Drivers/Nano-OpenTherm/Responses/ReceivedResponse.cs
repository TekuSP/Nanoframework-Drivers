using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class ReceivedResponse : Response
    {
        #region Private Fields

        private uint _raw;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes from a raw OpenTherm frame, decoding type and ID.
        /// </summary>
        public ReceivedResponse(uint rawData)
        {
            SetRawDataCore(rawData);
            MessageType = Utilities.GetMessageType(rawData);
            MessageID = Utilities.GetMessageID(rawData);
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID { get; }

        public override MessageType MessageType { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => _raw;

        protected override void SetRawDataCore(uint value) => _raw = value;

        #endregion Protected Methods
    }
}