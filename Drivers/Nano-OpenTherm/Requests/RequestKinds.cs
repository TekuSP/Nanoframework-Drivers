using System;
using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Base for Read-only requests. Exposes a public getter for RawData and no setter.
    /// </summary>
    public abstract class ReadRequest : Request
    {
        protected ReadRequest() : base()
        {
            
        }
        protected ReadRequest(Request baseReq) : base(baseReq)
        {
        }
        public uint RawData
        {
            get => GetRawDataCore();
            protected set => SetRawDataCore(value);
        }
    }

    /// <summary>
    /// Base for Write-only requests. Exposes a public setter for RawData and no getter.
    /// </summary>
    public abstract class WriteRequest : Request
    {
        protected WriteRequest() : base()
        {       
        }
        protected WriteRequest(Request baseReq) : base(baseReq)
        {
        }

        public uint RawData
        {
            protected get => GetRawDataCore();
            set => SetRawDataCore(value);
        }
    }

    /// <summary>
    /// Base for Read/Write requests. Exposes both accessors.
    /// </summary>
    public abstract class ReadWriteRequest : Request
    {
        protected ReadWriteRequest() : base()
        { 
        }
        protected ReadWriteRequest(Request baseReq) : base(baseReq)
        {
        }

        public uint RawData
        {
            get => GetRawDataCore();
            set => SetRawDataCore(value);
        }
    }
}
