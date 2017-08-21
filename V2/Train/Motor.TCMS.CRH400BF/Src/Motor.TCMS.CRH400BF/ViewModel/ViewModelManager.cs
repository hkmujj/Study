using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Motor.TCMS.CRH400BF.Model;
using Motor.TCMS.CRH400BF.Services;

namespace Motor.TCMS.CRH400BF.ViewModel
{
    [Export]
    public class ViewModelManager
    {
        public ViewModelManager()
        {
            MainViewModel = ServiceLocator.Current.GetInstance<DomainViewModel>();
            MainViewModel.ViewLocation = ViewLocation.Main;
            MainViewModel.RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            ReserveViewModel = ServiceLocator.Current.GetInstance<DomainViewModel>();
            ReserveViewModel.ViewLocation = ViewLocation.Reserve;
            ReserveViewModel.RegionManager =
            ServiceLocator.Current.GetInstance<RegionManagerProvider>().ReserveRegionManager;
        }

        public DomainViewModel MainViewModel { get; private set; }

        public DomainViewModel ReserveViewModel { get; private set; }
    }
}