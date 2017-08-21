using Microsoft.Practices.Prism.Events;
using Motor.ATP.Domain.Model.Events.DriverInputEvents;

namespace Motor.ATP.Domain.Model.Events
{
    public class DriverInputEvent<T> : CompositePresentationEvent<DriverInputEventArgs<T>>
    {

    }
}