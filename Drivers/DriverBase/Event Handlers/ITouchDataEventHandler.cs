using DriverBase.Interfaces;

namespace DriverBase.Event_Handlers
{
    public static partial class EventHandlers
    {
        public delegate void ITouchDataHandler(object sender, ITouchData data);
    }
}
