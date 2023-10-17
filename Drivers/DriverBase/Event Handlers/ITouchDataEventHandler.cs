using DriverBase.Interfaces;

namespace DriverBase.Event_Handlers
{
    public static class EventHandlers
    {
        public delegate void ITouchDataHandler(object sender, ITouchData data);
    }
}
