using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetDWHSetPointRequest : Request
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

        public override MessageID MessageID => MessageID.Tdhw;
    }
}
