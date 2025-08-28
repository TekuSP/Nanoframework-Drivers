using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the remote override function flags. v2.2: ID 100 is read-only.
    /// </summary>
    public class GetRemoteOverrideFunctionRequest : ReadRequest, IRemoteOverrideFunction
    {
        #region Public Constructors

    public GetRemoteOverrideFunctionRequest() : base()
        {
        }

    public GetRemoteOverrideFunctionRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        // Backing via protected interface property only

        #region Public Properties

        public bool ManualChangePriority { get => RemoteOverrideFunction.IsSet(TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteOverrideFunction.ManualChangePriority); set => RemoteOverrideFunction = RemoteOverrideFunction.SetFlag(TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteOverrideFunction.ManualChangePriority, value); }

    public override MessageID MessageID => MessageID.RemoteOverrideFunction;

    public override MessageType MessageType => MessageType.READ_DATA;

        public bool ProgramChangePriority { get => RemoteOverrideFunction.IsSet(TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteOverrideFunction.ProgramChangePriority); set => RemoteOverrideFunction = RemoteOverrideFunction.SetFlag(TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteOverrideFunction.ProgramChangePriority, value); }

        public bool RemoteOverrideReserved2 { get => RemoteOverrideFunction.IsSet(TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteOverrideFunction.Reserved2); set => RemoteOverrideFunction = RemoteOverrideFunction.SetFlag(TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteOverrideFunction.Reserved2, value); }

        public bool RemoteOverrideReserved3 { get => RemoteOverrideFunction.IsSet(TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteOverrideFunction.Reserved3); set => RemoteOverrideFunction = RemoteOverrideFunction.SetFlag(TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteOverrideFunction.Reserved3, value); }

        public bool RemoteOverrideReserved4 { get => RemoteOverrideFunction.IsSet(TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteOverrideFunction.Reserved4); set => RemoteOverrideFunction = RemoteOverrideFunction.SetFlag(TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteOverrideFunction.Reserved4, value); }

        public bool RemoteOverrideReserved5 { get => RemoteOverrideFunction.IsSet(TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteOverrideFunction.Reserved5); set => RemoteOverrideFunction = RemoteOverrideFunction.SetFlag(TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteOverrideFunction.Reserved5, value); }

        public bool RemoteOverrideReserved6 { get => RemoteOverrideFunction.IsSet(TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteOverrideFunction.Reserved6); set => RemoteOverrideFunction = RemoteOverrideFunction.SetFlag(TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteOverrideFunction.Reserved6, value); }

        public bool RemoteOverrideReserved7 { get => RemoteOverrideFunction.IsSet(TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteOverrideFunction.Reserved7); set => RemoteOverrideFunction = RemoteOverrideFunction.SetFlag(TekuSP.Drivers.Nano_OpenTherm.Enums.RemoteOverrideFunction.Reserved7, value); }

        #endregion Public Properties

        #region Protected Properties

        // IRemoteOverrideFunction
        protected RemoteOverrideFunction RemoteOverrideFunction { get; set; }

        #endregion Protected Properties

        #region Protected Methods

    protected override uint GetRawDataCore() => ProcessRequest(0);

    protected override void SetRawDataCore(uint value) { }

        #endregion Protected Methods
    }
}