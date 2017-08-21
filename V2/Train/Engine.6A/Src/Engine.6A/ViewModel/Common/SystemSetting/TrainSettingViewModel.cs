using System.ComponentModel.Composition;
using Engine._6A.Interface.ViewModel.SystemSeting;

namespace Engine._6A.ViewModel.Common.SystemSetting
{
     [Export(typeof(ITrainSettingViewModel))]
    public class TrainSettingViewModel : ViewModelBase, ITrainSettingViewModel
    {

    }
}