using System;
using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Base for Read-only requests. Exposes a public getter for RawData and no setter.
    /// </summary>
    public abstract class ReadRequest : Request
    {
        public ulong RawData => GetRawDataCore();
        protected override void SetRawDataCore(ulong value) => throw new NotSupportedException("This request is read-only.");
    }

    /// <summary>
    /// Base for Write-only requests. Exposes a public setter for RawData and no getter.
    /// </summary>
    public abstract class WriteRequest : Request
    {
        public ulong RawData { set => SetRawDataCore(value); }
        // Derived classes must implement GetRawDataCore to let the driver read the frame via the explicit interface getter
    }

    /// <summary>
    /// Base for Read/Write requests. Exposes both accessors.
    /// </summary>
    public abstract class ReadWriteRequest : Request
    {
        public ulong RawData
        {
            get => GetRawDataCore();
            set => SetRawDataCore(value);
        }
    }
}
