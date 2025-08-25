using System;
using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class SetDWHSetPointRequest : WriteRequest
    {
        public SetDWHSetPointRequest() : base() { }
        public SetDWHSetPointRequest(Request baseReq) : base(baseReq) { }

        private float _temperature;
        public float Temperature
        {
            get => _temperature;
            set => _temperature = value.Normalize();
        }

        protected override uint GetRawDataCore()
        {
            return ProcessRequest(Utilities.GetRawTemperature(Temperature));
        }
        protected override void SetRawDataCore(uint value)
        {
            Temperature = Utilities.GetFloat(value);
        }

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.TdhwSet;
    }
}
