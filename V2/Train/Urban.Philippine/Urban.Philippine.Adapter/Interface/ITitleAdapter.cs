using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Philippine.Adapter.Interface
{
    public interface ITitleAdapter : IModelAdapterBase
    {
        ITitleViewModel Title { get; }
    }
}