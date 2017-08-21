using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Service;

namespace Urban.Domain.TrainState.Interface.Common
{
    public interface IStateUpdatable
    {
        void Update(IUpdateParam updateParam);
    }
}