using System.ComponentModel.Composition;
using Engine._6A.Interface.ViewModel.SystemSeting;
using Engine._6A.Interface.ViewModel.SystemSeting.TrainInfo;

namespace Engine._6A.ViewModel.Common.SystemSetting
{
   [Export(typeof(ISystemSettingViewModel))]
    public class SystemSettingViewModel : ViewModelBase, ISystemSettingViewModel
    {
        [Import]
        public IWorkModelViewModel WorkModel { get; private set; }
        [Import]
        public ITrainSettingViewModel TrainSetting { get; private set; }
        [Import]
        public ITrainInfoViewModel TrainInfo { get; private set; }
        [Import]
        public IVersionInfoViewModel VersionInfo { get; private set; }
        [Import]
        public IPlateformInfoViewModel PlateformInfo { get; private set; }
    }
}