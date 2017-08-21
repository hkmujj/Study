using MMI.Facility.Interface.Service;
using Urban.Phillippine.View.Interface;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Philippine.Adapter.Interface
{
    public interface IModelAdapter : IDataChanged, IDataClear, IDataListener
    {
        IFaultManager FaultManager { get;  }
        IPhilippineViewModel Model { get; }
        IMainViewModelAdapter MainView { get; }
        ITitleAdapter TitleAdapter { get; }
    }
}