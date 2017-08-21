using System.Collections.ObjectModel;
using Urban.Phillippine.View.Config;

namespace Urban.Phillippine.View.Interface.ViewModel
{
    public interface IVVVFViewModel
    {
        ObservableCollection<VVVFViewUnit> VVVFViewUnits { get; set; }
    }
}