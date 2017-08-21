using LightRail.HMI.SZLHLF.ViewModel;

namespace LightRail.HMI.SZLHLF.Interface
{
    public interface IViewModelExtention<T> where T : ViewModelBase
    {
        T ViewModel { get; set; }
    }
}
