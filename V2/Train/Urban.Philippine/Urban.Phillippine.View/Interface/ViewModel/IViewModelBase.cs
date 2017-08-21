using System.ComponentModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

namespace Urban.Phillippine.View.Interface.ViewModel
{
    public interface IViewModelBase : INotifyPropertyChanged, IDataClear
    {
        IRegionManager RegionManager { get; }
        IEventAggregator EventAggregator { get; }
    }
}