using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Remote Override Operating Mode response (HC1/HC2/DHW modes in low byte).</summary>
    public class RemoteOverrideOperatingModeResponse : Response, IOperatingMode, IOperatingModeHC2, IOperatingModeDHW
    {
        protected OperatingMode HC1 { get; set; }
        protected OperatingMode HC2 { get; set; }
        protected OperatingMode DHW { get; set; }

        public RemoteOverrideOperatingModeResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public RemoteOverrideOperatingModeResponse(Response r) : base(r) { }

        protected override uint GetRawDataCore()
        {
            byte low = Utilities.SetOperatingModes(HC1, HC2, DHW);
            return ProcessResponse(Utilities.MakeUShort(0, low));
        }

        protected override void SetRawDataCore(uint value)
        {
            HC1 = Utilities.GetOperatingModeHC1(value);
            HC2 = Utilities.GetOperatingModeHC2(value);
            DHW = Utilities.GetOperatingModeDHW(value);
        }

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.RemoteOverrideOperatingModeHeatingDHW;

        // IOperatingMode (HC1)
        public bool ModeIsAuto { get => HC1 == OperatingMode.Auto; set { if (value) HC1 = OperatingMode.Auto; } }
        public bool ModeIsManual { get => HC1 == OperatingMode.Manual; set { if (value) HC1 = OperatingMode.Manual; } }
        public bool ModeIsOff { get => HC1 == OperatingMode.Off; set { if (value) HC1 = OperatingMode.Off; } }
        public bool ModeIsReserved { get => HC1 == OperatingMode.Reserved; set { if (value) HC1 = OperatingMode.Reserved; } }

        // IOperatingModeHC2
        public bool HC2ModeIsAuto { get => HC2 == OperatingMode.Auto; set { if (value) HC2 = OperatingMode.Auto; } }
        public bool HC2ModeIsManual { get => HC2 == OperatingMode.Manual; set { if (value) HC2 = OperatingMode.Manual; } }
        public bool HC2ModeIsOff { get => HC2 == OperatingMode.Off; set { if (value) HC2 = OperatingMode.Off; } }
        public bool HC2ModeIsReserved { get => HC2 == OperatingMode.Reserved; set { if (value) HC2 = OperatingMode.Reserved; } }

        // IOperatingModeDHW
        public bool DHWModeIsAuto { get => DHW == OperatingMode.Auto; set { if (value) DHW = OperatingMode.Auto; } }
        public bool DHWModeIsManual { get => DHW == OperatingMode.Manual; set { if (value) DHW = OperatingMode.Manual; } }
        public bool DHWModeIsOff { get => DHW == OperatingMode.Off; set { if (value) DHW = OperatingMode.Off; } }
        public bool DHWModeIsReserved { get => DHW == OperatingMode.Reserved; set { if (value) DHW = OperatingMode.Reserved; } }
    }
}
