using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.ShenZhenLine7.Envets;
using Subway.ShenZhenLine7.Extention;
using Subway.ShenZhenLine7.Resource.Keys;
using Subway.ShenZhenLine7.ViewModels;

namespace Subway.ShenZhenLine7.Controller.ViewModelController
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class TractionLockController : ControllerBase<TractionLockViewModel>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TractionLockController()
        {
            ServiceLocator.Current.GetInstance<IEventAggregator>()
                .GetEvent<BoolDataChangedEvent>()
                .Subscribe((NetResponse));
        }

        private void NetResponse(DataChangedEventArgs<bool> dataChangedEventArgs)
        {
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车1右侧车门未关好, b => ViewModel.Car1RightUnClose = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车2右侧车门未关好, b => ViewModel.Car2RightUnClose = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车3右侧车门未关好, b => ViewModel.Car3RightUnClose = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车4右侧车门未关好, b => ViewModel.Car4RightUnClose = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车5右侧车门未关好, b => ViewModel.Car5RightUnClose = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车6右侧车门未关好, b => ViewModel.Car6RightUnClose = b);

            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车1左侧车门未关好, b => ViewModel.Car1LeftUnClose = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车2左侧车门未关好, b => ViewModel.Car2LeftUnClose = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车3左侧车门未关好, b => ViewModel.Car3LeftUnClose = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车4左侧车门未关好, b => ViewModel.Car4LeftUnClose = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车5左侧车门未关好, b => ViewModel.Car5LeftUnClose = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车6左侧车门未关好, b => ViewModel.Car6LeftUnClose = b);

            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车1停放制动未缓解, b => ViewModel.Car1ParkingBrakeUnRemit = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车2停放制动未缓解, b => ViewModel.Car2ParkingBrakeUnRemit = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车3停放制动未缓解, b => ViewModel.Car3ParkingBrakeUnRemit = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车4停放制动未缓解, b => ViewModel.Car4ParkingBrakeUnRemit = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车5停放制动未缓解, b => ViewModel.Car5ParkingBrakeUnRemit = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车6停放制动未缓解, b => ViewModel.Car6ParkingBrakeUnRemit = b);

            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车1主风管压力不足, b => ViewModel.Car1MasterFan = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车6主风管压力不足, b => ViewModel.Car6MasterFan = b);

            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车1紧急停车蘑菇拍1拍下, b => ViewModel.Car1EmergencyParking1 = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车1紧急停车蘑菇拍2拍下, b => ViewModel.Car1EmergencyParking2 = b);

            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车6紧急停车蘑菇拍1拍下, b => ViewModel.Car6EmergencyParking1 = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车6紧急停车蘑菇拍2拍下, b => ViewModel.Car6EmergencyParking2 = b);

            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车1紧急制动施加, b => ViewModel.Car1EmergencyBrakeInfiction = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车6紧急制动施加, b => ViewModel.Car6EmergencyBrakeInfiction = b);

            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车1警惕按钮松开, b => ViewModel.Car1VigilantButtonUp = b);
            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.车6警惕按钮松开, b => ViewModel.Car6VigilantButtonUp = b);

            dataChangedEventArgs.Data.UpdateIfContain(InBoolKeys.列车移动时制动不缓解, b => ViewModel.TrainRemove = b);
        }
    }
}