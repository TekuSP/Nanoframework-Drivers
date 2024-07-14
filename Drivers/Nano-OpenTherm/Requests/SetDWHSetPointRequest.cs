using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class SetDWHSetPointRequest : Request
    {
        public override ulong RawData
        {
            get
            {
                return ProcessRequest(Utilities.GetRawTemperature(Temperature));
            }
            set
            {

            }
        }

        public override MessageType MessageType => MessageType.WRITE_DATA;

        public override MessageID MessageID => MessageID.TdhwSet;

        /// <summary>
        /// DWH Set Point Temperature
        /// </summary>
        public float Temperature { get; set; }
    }
}
