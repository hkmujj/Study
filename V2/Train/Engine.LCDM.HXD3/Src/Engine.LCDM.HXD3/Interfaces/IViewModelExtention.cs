using Engine.LCDM.HXD3.ViewModels;

namespace Engine.LCDM.HXD3.Interfaces
{
    public interface IViewModelExtention<T> where T : ViewModelBase
    {
        T ViewModel { get; set; }
    }
}