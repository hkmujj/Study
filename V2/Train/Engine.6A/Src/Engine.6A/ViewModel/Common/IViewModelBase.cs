using System.ComponentModel;
using Engine._6A.Interface;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

namespace Engine._6A.ViewModel.Common
{
    public interface IViewModelBase : INotifyPropertyChanged, IClearData, INavigationAware
    {
        IRegionManager RegionManager { get; }
        IEventAggregator EventAggregator { get; }
    }
}