using System;
using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class SetBoilerTemperatureRequest : WriteRequest
    {
        protected override ulong GetRawDataCore() => ProcessRequest(Utilities.GetRawTemperature(Temperature));
        protected override void SetRawDataCore(ulong value) { /* allow raw override if ever needed */ }

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.TSet;

        /// <summary>
        /// Temperature to set
        /// </summary>
        public float Temperature { get; set; }
    }
}
