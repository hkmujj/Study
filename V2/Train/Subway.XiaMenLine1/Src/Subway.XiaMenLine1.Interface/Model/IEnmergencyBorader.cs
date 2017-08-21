using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace Subway.XiaMenLine1.Interface.Model
{
    public interface IEnmergencyBorader : IViewModel
    {
        ICommand SendBoradType { get; set; }
        ICommand SenBoradID { get; }
        ICommand GetCurrent { get; }
        ObservableCollection<IBoradcast> LoveBoradcast { get; }
        ObservableCollection<IBoradcast> EmergentBoradcast { get; }
        ObservableCollection<IBoradcast> CustBoradcast { get; }

    }
}