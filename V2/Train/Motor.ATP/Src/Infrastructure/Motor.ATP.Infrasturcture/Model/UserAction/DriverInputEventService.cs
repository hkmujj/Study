using System;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Model.Service;

namespace Motor.ATP.Infrasturcture.Model.UserAction
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