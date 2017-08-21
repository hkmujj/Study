using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Service;

namespace Motor.ATP.Infrasturcture.Interface.Service
{
    /// <summary>
    /// EventAggregator
    /// </summary>
    public interface IEventAggregatorProvider : IService
    {
        /// <summary>
        /// EventAggregator
        /// </summary>
        IEventAggregator EventAggregator { get; }
    }
}