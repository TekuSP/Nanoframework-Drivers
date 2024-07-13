using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class SetBoilerTemperatureRequest : Request
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

        public override MessageID MessageID => MessageID.TSet;

        /// <summary>
        /// Temperature to set
        /// </summary>
        public float Temperature { get; set; }
    }
}
