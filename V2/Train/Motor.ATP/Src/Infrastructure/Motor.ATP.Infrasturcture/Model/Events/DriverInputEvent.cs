using Microsoft.Practices.Prism.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;

namespace Motor.ATP.Infrasturcture.Model.Events
{
    public class DriverInputEvent<T> : CompositePresentationEvent<DriverInputEventArgs<T>>
    {

    }
}