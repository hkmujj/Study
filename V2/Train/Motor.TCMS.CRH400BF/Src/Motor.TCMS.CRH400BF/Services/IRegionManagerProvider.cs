using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Regions;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Motor.TCMS.CRH400BF.Services
{
    public interface IRegionManagerProvider
    {
        IRegionManager MainRegionManager { get; }

        IRegionManager ReserveRegionManager { get; }
    }

    [Export]
    [Export(typeof(IRegionManagerProvider))]
    class RegionManagerProvider : IRegionManagerProvider
    {
        public RegionManagerProvider()
        {
            MainRegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            ReserveRegionManager= new MefRegionManager();
        }

        public IRegionManager MainRegionManager { get; private set; }

        public IRegionManager ReserveRegionManager { get; private set; }
    }
}