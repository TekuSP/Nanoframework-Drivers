using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Request to get the solar storage version.
    /// </summary>
    public class GetSolarStorageVersionRequest : ReadRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSolarStorageVersionRequest"/> class.
        /// </summary>
        public GetSolarStorageVersionRequest() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSolarStorageVersionRequest"/> class.
        /// </summary>
        /// <param name="baseReq">The base request.</param>
        public GetSolarStorageVersionRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Product firmware/hardware version (high byte).
        /// </summary>
        public byte Version { get; set; }

        /// <summary>
        /// Product type identifier (low byte). OEM-specific mapping.
        /// </summary>
        public VersionProductType Type { get; set; }

        /// <summary>
        /// Retrieves the raw data for the request.
        /// </summary>
        /// <returns>The raw data as an unsigned integer.</returns>
        protected override uint GetRawDataCore()
        {
            uint raw = (uint)((Version << 8) | (byte)Type);
            return ProcessRequest(raw);
        }

        /// <summary>
        /// Sets the raw data for the request.
        /// </summary>
        /// <param name="value">The raw data as an unsigned integer.</param>
        protected override void SetRawDataCore(uint value)
        {
            Version = Utilities.GetHighByte(value);
            Type = (VersionProductType)Utilities.GetLowByte(value);
        }

        /// <summary>
        /// Gets the message type for the request.
        /// </summary>
        public override MessageType MessageType => MessageType.READ_DATA;

        /// <summary>
        /// Gets the message ID for the request.
        /// </summary>
        public override MessageID MessageID => MessageID.SolarStorageVersion;
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
