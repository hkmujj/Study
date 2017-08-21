using Motor.ATP.Infrasturcture.Interface.Service;

namespace Motor.ATP.Infrasturcture.Model.Service
{
    public interface INotifyableDriverInputEventService : IDriverInputEventService
    {
        void OnDriverInputed(DriverInputEventArgs eventArgs);
    }
}