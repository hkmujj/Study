using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Subway.XiaMenLine1.Interface.Model
{
    public interface IEventPageModel : IViewModel
    {
        EventLevel TitleEventLevel { get; }
        string AllFaultNumber { get; }
        string CurrentPage { get; }
        string MaxPage { get; }
        IEventInfo EventInfo { get; set; }
        ObservableCollection<IEventInfo> EventsInfos { get; }
        Visibility VisibilityFalut { get; set; }

        ICommand GetCurentEvent { get; }
        ICommand Sort { get; }
        ICommand FirstPage { get; }
        ICommand NextPage { get; }
        ICommand LastPage { get; }
        ICommand GetCurrent { get; }
        ICommand Reset { get; }
        ICommand Cofirm { get; }
        ICommand NavigatorToEvenInfo { get; }
        IEventInfo ConfirmEvent { get; }
        string CarNumber { get; }
        string Datetime { get; }
        string SystemFaluet { get; }
        string CofirmLogic { get; }
         string NormalInfo { get; set; }

    }
}