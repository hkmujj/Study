using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Engine._6A.Interface.ViewModel
{
    public interface IFaultViewModel : IClearData, INotifyPropertyChanged
    {
        string PageInfo { get; set; }
        IList<IFaultInfo> CurrentFalutInfos { get; set; }
        ICommand NextPage { get; }
        ICommand LastPage { get; }
        ICommand Reset { get; }
        IFaultManage FaultManage { get; set; }
        Visibility ButtonVisibility { get; set; }
    }
}