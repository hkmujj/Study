using System.ComponentModel.Composition;
using System.Windows.Input;
using Engine._6A.Constance;
using Engine._6A.Interface.ViewModel;
using Engine._6A.ViewModel.Common;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;

namespace Engine._6A.ViewModel.Axle6
{
    [Export(ContractName.Axle6, typeof(IButtonViewModel))]
    public class Axle6ButtonViewModel : ViewModelBase, IAxle6ButtonViewModel
    {
        public Axle6ButtonViewModel()
        {
            Navigator = new DelegateCommand<string>((args) =>
            {
                if (string.IsNullOrEmpty(args))
                {
                    return;
                }

                RegionManager.RequestNavigate(RegionNames.ContentRegion, args);

            });
        }

        public ICommand Navigator { get; private set; }
    }
}