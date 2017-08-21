using Microsoft.Practices.Prism.Regions;

namespace Motor.HMI.CRH380BG.Services
{
    public interface IRegionManagerProvider
    {
        IRegionManager MainRegionManager { get; }
        IRegionManager ReserveRegionManager { get; }
    }
}