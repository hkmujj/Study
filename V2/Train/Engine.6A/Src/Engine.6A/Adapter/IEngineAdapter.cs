using Engine._6A.Adapter.ConfigInfo;
using Engine._6A.Interface;
using Engine._6A.Interface.ViewModel;
using MMI.Facility.Interface.Service;

namespace Engine._6A.Adapter
{
    public interface IEngineAdapter : IClearData, IDataChanged, IDataListener
    {
        IEngine6AViewModel Model { get; }
        IFaultManage FaultManage { get; }
    }
}