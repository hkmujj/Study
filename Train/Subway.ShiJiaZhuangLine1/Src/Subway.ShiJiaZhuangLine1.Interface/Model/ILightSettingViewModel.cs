using System.Windows.Input;

namespace Subway.ShiJiaZhuangLine1.Interface.Model
{
    public interface ILightSettingViewModel : IViewModel
    {
        double Light { get; set; }
        ICommand Plus { get; }
        ICommand Subtract { get; }
    }
}