using System.Collections.ObjectModel;
using System.Windows.Input;
using Urban.Phillippine.View.Config;

namespace Urban.Phillippine.View.Interface.ViewModel
{
    public interface ITMSViewModel
    {
        ObservableCollection<TMSViewUnit> TMS { get; set; }
        ObservableCollection<TMSViewUnit> DisplayTMS { get; set; }
        ICommand ChangedDisplay { get; }
    }
}