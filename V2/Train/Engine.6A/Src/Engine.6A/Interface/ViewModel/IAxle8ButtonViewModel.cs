using System.Windows.Input;

namespace Engine._6A.Interface.ViewModel
{
    public interface IAxle8ButtonViewModel : IButtonViewModel
    {
        ICommand ChangedTrain { get; }

    }
}