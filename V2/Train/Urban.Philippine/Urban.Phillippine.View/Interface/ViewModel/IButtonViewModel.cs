using System.Windows.Input;

namespace Urban.Phillippine.View.Interface.ViewModel
{
    public interface IButtonViewModel
    {
        ICommand ChangedContent { get; }
        ICommand ChangedMain { get; }
        ICommand ChangedContentButton { get; }
    }
}