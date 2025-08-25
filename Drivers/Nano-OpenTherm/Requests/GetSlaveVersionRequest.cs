using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Slave product version number and type
    /// </summary>
    public class GetSlaveVersionRequest : ReadRequest
    {
        public GetSlaveVersionRequest() : base() { }
        public GetSlaveVersionRequest(Request baseReq) : base(baseReq) { }

        public byte Version { get; set; }
        public VersionProductType Type { get; set; }

        protected override uint GetRawDataCore()
        {
            uint raw = (uint)((Version << 8) | (byte)Type);
            return ProcessRequest(raw);
        }
        protected override void SetRawDataCore(uint value)
        {
            Version = Utilities.GetHighByte(value);
            Type = (VersionProductType)Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.SlaveVersion;

        // Convenience properties for Type
        public bool IsBoiler { get => Type == VersionProductType.Boiler; set { if (value) Type = VersionProductType.Boiler; } }
        public bool IsHeatPump { get => Type == VersionProductType.HeatPump; set { if (value) Type = VersionProductType.HeatPump; } }
        public bool IsVentilation { get => Type == VersionProductType.Ventilation; set { if (value) Type = VersionProductType.Ventilation; } }
        public bool IsController { get => Type == VersionProductType.Controller; set { if (value) Type = VersionProductType.Controller; } }
        public bool IsSensor { get => Type == VersionProductType.Sensor; set { if (value) Type = VersionProductType.Sensor; } }
    }
}
