using Motor.ATP.Domain.Interface.Service;

namespace Motor.ATP.Domain.Model.Service
{
    public interface INotifyableDriverInputEventService : IDriverInputEventService
    {
        void OnDriverInputed(DriverInputEventArgs eventArgs);
    }
}