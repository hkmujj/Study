using System.Collections.ObjectModel;
using Urban.Phillippine.View.Config;

namespace Urban.Phillippine.View.Interface.ViewModel
{
    public interface IIOStateViewModel
    {
        ObservableCollection<IOStateViewUnit> IOState { get; set; }
    }
}