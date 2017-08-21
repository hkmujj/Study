using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Service;

namespace Motor.ATP.Domain.Interface.Service
{
    public interface IEventAggregatorProvider : IService
    {
        IEventAggregator EventAggregator { get; }
    }
}