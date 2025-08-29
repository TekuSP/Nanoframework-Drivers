using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Base for read-only requests. Exposes a public getter for <c>RawData</c> and a protected setter.
    /// Use this base when the payload is provided by the slave and the master does not write fields.
    /// </summary>
    public abstract class ReadRequest : Request
    {
        #region Protected Constructors

        protected ReadRequest() : base()
        {
        }

        protected ReadRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Protected Constructors

        #region Public Properties

        /// <summary>
        /// Encoded 32-bit OpenTherm frame. Public getter for consumers, protected setter for derived types.
        /// </summary>
        public uint RawData
        {
            get => GetRawDataCore();
            protected set => SetRawDataCore(value);
        }

        #endregion Public Properties
    }

    /// <summary>
    /// Base for read/write requests. Exposes both accessors to <c>RawData</c>.
    /// Use this when the master both sends and receives fields for the same MessageID.
    /// </summary>
    public abstract class ReadWriteRequest : Request
    {
        #region Protected Constructors

        protected ReadWriteRequest(MessageType messageType) : base()
        {
            MessageType = messageType;
        }

        protected ReadWriteRequest(Request baseReq, MessageType messageType) : base(baseReq)
        {
            MessageType = messageType;
        }

        public override MessageType MessageType { get; }

        #endregion Protected Constructors

        #region Public Properties

        /// <summary>
        /// Encoded 32-bit OpenTherm frame with both public getter and setter.
        /// </summary>
        public uint RawData
        {
            get => GetRawDataCore();
            set => SetRawDataCore(value);
        }

        #endregion Public Properties
    }

    /// <summary>
    /// Base for write-only requests. Exposes a public setter for <c>RawData</c> and a protected getter.
    /// Use this base when the payload is sent by the master and not read back directly from the device.
    /// </summary>
    public abstract class WriteRequest : Request
    {
        #region Protected Constructors

        protected WriteRequest() : base()
        {
        }

        protected WriteRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Protected Constructors

        #region Public Properties

        /// <summary>
        /// Encoded 32-bit OpenTherm frame. Public setter for consumers, protected getter for derived types.
        /// </summary>
        public uint RawData
        {
            protected get => GetRawDataCore();
            set => SetRawDataCore(value);
        }

        #endregion Public Properties
    }
}