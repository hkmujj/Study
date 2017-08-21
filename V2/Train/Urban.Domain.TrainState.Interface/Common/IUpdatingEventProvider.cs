using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Service;

namespace Urban.Domain.TrainState.Interface.Common
{
    public interface IUpdatingEventProvider<out T>
    {
        event UpdateEvent<T> Updating;
    }
}