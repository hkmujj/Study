using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Regions;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Motor.HMI.CRH380BG.Services
{
    [Export]
    [Export(typeof(IRegionManagerProvider))]
    internal class RegionManagerProvider : IRegionManagerProvider
    {
        public RegionManagerProvider()
        {
            MainRegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            ReserveRegionManager = new MefRegionManager();
        }

        public IRegionManager MainRegionManager { get; private set; }
        public IRegionManager ReserveRegionManager { get; private set; }
    }
}