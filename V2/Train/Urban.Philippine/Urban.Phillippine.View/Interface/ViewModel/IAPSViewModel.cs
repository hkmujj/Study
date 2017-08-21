using System.Collections.ObjectModel;
using Urban.Phillippine.View.Config;

namespace Urban.Phillippine.View.Interface.ViewModel
{
    public interface IAPSViewModel
    {
        ObservableCollection<APSViewUnit> APS { get; set; }
    }
}