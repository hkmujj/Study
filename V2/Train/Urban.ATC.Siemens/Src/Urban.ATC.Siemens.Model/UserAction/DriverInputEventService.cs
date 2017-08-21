using System;
using Motor.ATP.Domain.Interface.Service;
using Motor.ATP.Domain.Model.Service;

namespace Motor.ATP.Domain.Model.UserAction
{
    public class DriverInputEventService : INotifyableDriverInputEventService
    {
        public event Action<DriverInputEventArgs> DriverInputed;

        public virtual void OnDriverInputed(DriverInputEventArgs eventArgs)
        {
            var handler = DriverInputed;
            if (handler != null)
            {
                handler(eventArgs);
            }
        }
    }
}