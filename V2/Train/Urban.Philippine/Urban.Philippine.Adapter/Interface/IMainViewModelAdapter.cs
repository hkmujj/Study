using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Philippine.Adapter.Interface
{
    public interface IMainViewModelAdapter : IModelAdapterBase
    {
        IMainViewModel MainViewModel { get; }
    }
}