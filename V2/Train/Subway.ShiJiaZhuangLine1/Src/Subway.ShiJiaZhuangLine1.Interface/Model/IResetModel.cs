using System.Windows;
using System.Windows.Input;

namespace Subway.ShiJiaZhuangLine1.Interface.Model
{
    public interface IResetModel : IViewModel
    {
        bool IsCar2Traction { get; set; }
        bool IsCar3Traction { get; set; }
        bool IsCar4Traction { get; set; }
        bool IsCar5Traction { get; set; }
        bool IsCar1Assist { get; set; }
        bool IsCar3Assist { get; set; }
        bool IsCar4Assist { get; set; }
        bool IsCar6Assist { get; set; }
        bool IsCar1Smoke { get; set; }
        string CurrentKey { get; }
        ICommand Click { get; }
        ICommand Confirm { get; }
        ICommand Back { get; }
        Visibility ConfirmVisibility { get; }
    }
}