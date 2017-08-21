using System.Collections.ObjectModel;
using Urban.Phillippine.View.Config;

namespace Urban.Phillippine.View.Interface.ViewModel
{
    public interface IBrakeViewModel : IViewModelBase
    {
        ObservableCollection<BrakeViewUnit> Brake { get; set; }
    }
}