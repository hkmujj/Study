using System.Windows.Input;

namespace Engine._6A.Interface.ViewModel
{
    public interface IMainViewModel : IClearData
    {
        ICommand Back { get; }
    }
}