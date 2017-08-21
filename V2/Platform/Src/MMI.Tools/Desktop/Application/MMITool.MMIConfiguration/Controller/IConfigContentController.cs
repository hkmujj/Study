using System.ComponentModel;

namespace MMITool.Addin.MMIConfiguration.Controller
{
    public interface IConfigContentController : INotifyPropertyChanged
    {
        void SaveConfig();

        void ResetConfig();

        void NavigateToThis();

        bool IsModified { get; }
    }

    public interface IConfigContentController<out T> : IConfigContentController
    {
        T ViewModel { get; }
    }
}