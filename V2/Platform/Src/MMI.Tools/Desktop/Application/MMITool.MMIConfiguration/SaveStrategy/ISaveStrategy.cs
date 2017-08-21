using Microsoft.Practices.Prism.Regions;
using MMITool.Addin.MMIConfiguration.Interface;

namespace MMITool.Addin.MMIConfiguration.SaveStrategy
{
    public interface ISaveStrategy
    {
        bool CanSaveExcute { get; }

        bool CanCancelExcute { get; }

        void SaveExcute();

        void CancelExcute();

        void OnNavigatedTo(IConfigureContentEditerViewModel viewModel, NavigationContext navigationContext);

        void UpdateSaveItem(IConfigureContentEditerViewModel viewModel);
    }
}