using System;
using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class SetDWHSetPointRequest : WriteRequest
    {
        protected override ulong GetRawDataCore() => ProcessRequest(Utilities.GetRawTemperature(Temperature));
        protected override void SetRawDataCore(ulong value) { /* allow raw override if ever needed */ }

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.TdhwSet;

        /// <summary>
        /// DWH Set Point Temperature
        /// </summary>
        public float Temperature { get; set; }
    }
}
