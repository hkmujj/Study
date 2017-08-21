using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.ServiceLocation;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.Services;

namespace Motor.HMI.CRH380BG.ViewModel
{
    [Export]
    public class ViewModelManager
    {
        private readonly Lazy<CRH380BGViewModel> m_MainViewModel;
        private readonly Lazy<CRH380BGViewModel> m_ReserveViewModel;

        public ViewModelManager()
        {
            m_MainViewModel = new Lazy<CRH380BGViewModel>(() =>
            {
                var vm = ServiceLocator.Current.GetInstance<CRH380BGViewModel>();
                vm.ViewLocation = ViewLocation.Main;
                vm.RegionManager = ServiceLocator.Current.GetInstance<IRegionManagerProvider>().MainRegionManager;
                return vm;
            });
            m_ReserveViewModel = new Lazy<CRH380BGViewModel>(() =>
            {
                var vm = ServiceLocator.Current.GetInstance<CRH380BGViewModel>();
                vm.ViewLocation = ViewLocation.Reserve;
                vm.RegionManager = ServiceLocator.Current.GetInstance<IRegionManagerProvider>().ReserveRegionManager;
                return vm;
            });
        }

        public CRH380BGViewModel MainViewModel
        {
            get { return m_MainViewModel.Value; }
        }

        public CRH380BGViewModel ReserveViewModel
        {
            get { return m_ReserveViewModel.Value; }
        }
    }
}