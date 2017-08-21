using System.ComponentModel;
using Engine._6A.Interface.ViewModel.SystemSeting.TrainInfo;

namespace Engine._6A.Interface.ViewModel.SystemSeting
{
    public interface ISystemSettingViewModel : IClearData, INotifyPropertyChanged
    {
        IWorkModelViewModel WorkModel { get; }
        ITrainSettingViewModel TrainSetting { get; }
        ITrainInfoViewModel TrainInfo { get; }
        IVersionInfoViewModel VersionInfo { get; }
        IPlateformInfoViewModel PlateformInfo { get; }
    }
}