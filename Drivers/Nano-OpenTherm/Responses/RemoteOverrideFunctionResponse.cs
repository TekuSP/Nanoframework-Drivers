using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Remote override function flags (low byte).
    /// </summary>
    public class RemoteOverrideFunctionResponse : Response, IRemoteOverrideFunction
    {
        public RemoteOverrideFunctionResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public RemoteOverrideFunctionResponse(Response r) : base(r) { }

        protected RemoteOverrideFunction RemoteOverrideFunction { get; set; }

        protected override uint GetRawDataCore() => ProcessResponse((byte)RemoteOverrideFunction);
        protected override void SetRawDataCore(uint value) => RemoteOverrideFunction = Utilities.GetRemoteOverrideFunction(value);

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.RemoteOverrideFunction;

        // IRemoteOverrideFunction
        public bool ManualChangePriority { get => RemoteOverrideFunction.IsSet(RemoteOverrideFunction.ManualChangePriority); set => RemoteOverrideFunction = RemoteOverrideFunction.SetFlag(RemoteOverrideFunction.ManualChangePriority, value); }
        public bool ProgramChangePriority { get => RemoteOverrideFunction.IsSet(RemoteOverrideFunction.ProgramChangePriority); set => RemoteOverrideFunction = RemoteOverrideFunction.SetFlag(RemoteOverrideFunction.ProgramChangePriority, value); }
        public bool RemoteOverrideReserved2 { get => RemoteOverrideFunction.IsSet(RemoteOverrideFunction.Reserved2); set => RemoteOverrideFunction = RemoteOverrideFunction.SetFlag(RemoteOverrideFunction.Reserved2, value); }
        public bool RemoteOverrideReserved3 { get => RemoteOverrideFunction.IsSet(RemoteOverrideFunction.Reserved3); set => RemoteOverrideFunction = RemoteOverrideFunction.SetFlag(RemoteOverrideFunction.Reserved3, value); }
        public bool RemoteOverrideReserved4 { get => RemoteOverrideFunction.IsSet(RemoteOverrideFunction.Reserved4); set => RemoteOverrideFunction = RemoteOverrideFunction.SetFlag(RemoteOverrideFunction.Reserved4, value); }
        public bool RemoteOverrideReserved5 { get => RemoteOverrideFunction.IsSet(RemoteOverrideFunction.Reserved5); set => RemoteOverrideFunction = RemoteOverrideFunction.SetFlag(RemoteOverrideFunction.Reserved5, value); }
        public bool RemoteOverrideReserved6 { get => RemoteOverrideFunction.IsSet(RemoteOverrideFunction.Reserved6); set => RemoteOverrideFunction = RemoteOverrideFunction.SetFlag(RemoteOverrideFunction.Reserved6, value); }
        public bool RemoteOverrideReserved7 { get => RemoteOverrideFunction.IsSet(RemoteOverrideFunction.Reserved7); set => RemoteOverrideFunction = RemoteOverrideFunction.SetFlag(RemoteOverrideFunction.Reserved7, value); }
    }
}
