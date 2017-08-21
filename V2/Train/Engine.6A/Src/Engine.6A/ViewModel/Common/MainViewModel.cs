using System.ComponentModel.Composition;
using System.Windows.Input;
using Engine._6A.Constance;
using Engine._6A.Interface.ViewModel;
using Engine._6A.Views.Axle6;
using Engine._6A.Views.Axle8;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Engine._6A.ViewModel.Common
{
    [Export(typeof(IMainViewModel))]
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        public MainViewModel()
        {
            Back = new DelegateCommand(BackMethod);
        }

        private void BackMethod()
        {
            RegionManager.RequestNavigate(RegionNames.MainContentRegion, CoontrolNameBase.CurrentDialView);
            ServiceLocator.Current.GetInstance<AxleButtonView>().IsCurrent = false;
            ServiceLocator.Current.GetInstance<Axle8ButtonView>().IsCurrent = false;
        }

        public ICommand Back { get; private set; }


    }
}