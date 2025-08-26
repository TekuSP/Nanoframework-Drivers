using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the ventilation/heat-recovery product type and version bytes.
    /// </summary>
    public class GetVentilationVersionRequest : ReadRequest
    {
        public GetVentilationVersionRequest() : base() { }
        public GetVentilationVersionRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Product firmware/hardware version (high byte).
    /// </summary>
        public byte Version { get; set; }
    /// <summary>
    /// Product type identifier (low byte). OEM-specific mapping.
    /// </summary>
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
        public override MessageID MessageID => MessageID.VentilationHeatRecoveryVersion;

    // Convenience properties for Type
    /// <summary>Product is a Boiler.</summary>
    public bool IsBoiler { get => Type == VersionProductType.Boiler; set { if (value) Type = VersionProductType.Boiler; } }
    /// <summary>Product is a Heat Pump.</summary>
    public bool IsHeatPump { get => Type == VersionProductType.HeatPump; set { if (value) Type = VersionProductType.HeatPump; } }
    /// <summary>Product is a Ventilation unit.</summary>
    public bool IsVentilation { get => Type == VersionProductType.Ventilation; set { if (value) Type = VersionProductType.Ventilation; } }
    /// <summary>Product is a Controller.</summary>
    public bool IsController { get => Type == VersionProductType.Controller; set { if (value) Type = VersionProductType.Controller; } }
    /// <summary>Product is a Sensor.</summary>
    public bool IsSensor { get => Type == VersionProductType.Sensor; set { if (value) Type = VersionProductType.Sensor; } }
    }
}
