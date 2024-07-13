using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetBoilerTemperatureRequest : Request
    {
        public override ulong RawData
        {
            get
            {
                return ProcessRequest(0);
            }
            set
            {
            
            }
        }

        public override MessageType MessageType => MessageType.READ_DATA;

        public override MessageID MessageID => MessageID.Tboiler; 
    }
}
