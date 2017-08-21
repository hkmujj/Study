using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using CommonUtil.Annotations;

namespace Urban.Phillippine.View.Interface.ViewModel
{
    public interface IFaultRecordVIewModel
    {
        ICommand NexPage { get; }
        ICommand LastPage { get; }
        ICommand ChangedType { get; }
        IFaultManager Manager { get; }
        int MaxPage { get; set; }
        int CurrentPage { get; set; }
        string PageInfo { get; set; }
        IList<IFaultInfo> CurrentInfo { get; set; }
        IFaultInfo SelectInfo { get; set; }
        ICommand Select { get; }
        ICommand QuitComand { get; }
        Visibility Visibility { get; set; }
        Visibility FaultVisibility { get; set; }
        string StrPngType { get; set; }
        bool HasFault { get; set; }
    }
}