using System.Collections.Generic;
using System.Windows.Input;

namespace Subway.ShiJiaZhuangLine1.Interface.Model
{
    public interface IEnmergencyBorader : IViewModel
    {
        List<IBoradcast> DisplayBorder { get; set; }
        IBoradcast SelectBorder { get; set; }
        ICommand NextPage { get; }
        ICommand LastPage { get; }
        ICommand GetCurrent { get; }
        ICommand SentData { get; }
        ICommand ClearData { get; }
        ICommand ChangeCurrentPage { get; }


    }
}