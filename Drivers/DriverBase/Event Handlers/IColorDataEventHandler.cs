using DriverBase.Interfaces;

namespace DriverBase.Event_Handlers
{
    public static partial class EventHandlers
    {
        public delegate void IColorDataEventHandler(object sender, IColorData data);
    }
}
