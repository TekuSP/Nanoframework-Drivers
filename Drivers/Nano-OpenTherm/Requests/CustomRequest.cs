using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Generic read/write request for custom OpenTherm message types and IDs.
    /// </summary>
    public class CustomRequest : ReadWriteRequest
    {
        #region Private Fields

        private uint data;

        #endregion Private Fields

        #region Public Constructors

        public CustomRequest(MessageType messageType, MessageID messageID, uint data = 0)
        {
            this.data = data;
            MessageType = messageType;
            MessageID = messageID;
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID { get; }

        public override MessageType MessageType { get; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(data);

        protected override void SetRawDataCore(uint value)
        { data = value; }

        #endregion Protected Methods
    }
}