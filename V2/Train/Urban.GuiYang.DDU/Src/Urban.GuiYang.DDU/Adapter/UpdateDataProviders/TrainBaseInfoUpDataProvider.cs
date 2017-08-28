using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Urban.GuiYang.DDU.Extension;
using Urban.GuiYang.DDU.Model.Constant;
using Urban.GuiYang.DDU.Resources.Keys;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Adapter.UpdateDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    internal class TrainBaseInfoUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public TrainBaseInfoUpDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            args.ChangedBools.UpdateIfContains(InbKeys.Inb车辆屏亮屏, b => ViewModel.Model.Visible = b);

            args.ChangedFloats.UpdateIfContains(InfKeys.Inf列车速度, f => ViewModel.TrainViewModel.Model.Speed = f);
            //args.ChangedFloats.UpdateIfContains(InfKeys.Inf列车速度, f => ViewModel.TrainViewModel.Model.WorkState= (WorkState) f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf网压值, f => ViewModel.TrainViewModel.Model.TouchNetV = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf网流值, f => ViewModel.TrainViewModel.Model.TouchNetA = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf牵引力百分比, f => ViewModel.TrainViewModel.Model.TowPercent = f * 0.1f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf限速, f => ViewModel.TrainViewModel.Model.LimitedSpeed = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf制动力百分比, f => ViewModel.TrainViewModel.Model.BrakePercent = f * 0.1f);

            args.ChangedBools.UpdateIfContains(InbKeys.Inb旁路按钮旁路, b =>
            {
                ViewModel.Model.BypassState = b ? NavigationButtonState.Warning : NavigationButtonState.Normal;
            });
            args.ChangedBools.UpdateIfContains(InbKeys.Inb故障按钮故障, b =>
            {
                ViewModel.Model.FaultSate = b ? NavigationButtonState.Fault : NavigationButtonState.Normal;
            });
            var d = ViewModel.TrainViewModel.Model;
            d.WorkState = GetWorkState();
            d.DriveModel = GetDriveModel();
            d.RunningDirection = GetRunningDirection();
            d.CarCollection[0].Coupling.CouplingState = GetCouplingLeft();
            d.CarCollection[5].Coupling.CouplingState = GetCouplingRight();
            d.CarCollection[1].Pantograph.PantographState = GetPantographLeft();
            d.CarCollection[4].Pantograph.PantographState = GetPantographRight();
            d.CarCollection[1].Pantograph.VidioState = GetVidioStateLeft();
            d.CarCollection[4].Pantograph.VidioState = GetVidioStateRight();
            d.CarCollection[0].DriverRoom.DriverRoomState = GetDriverRoomStateLeft();
            d.CarCollection[5].DriverRoom.DriverRoomState = GetDriverRoomStateRight();

            d.CarCollection[0].Fire.FireAlert = GetFire1();
            d.CarCollection[1].Fire.FireAlert = GetFire2();
            d.CarCollection[2].Fire.FireAlert = GetFire3();
            d.CarCollection[3].Fire.FireAlert = GetFire4();
            d.CarCollection[4].Fire.FireAlert = GetFire5();
            d.CarCollection[5].Fire.FireAlert = GetFire6();

        }

        private CouplingState GetCouplingLeft()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb1车列车连挂))
            {
                return CouplingState.Active;
            }
            return CouplingState.Unactive;
        }
        private CouplingState GetCouplingRight()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb6车列车连挂))
            {
                return CouplingState.Active;
            }
            return CouplingState.Unactive;
        }




        private FireAlert GetFire6()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb6车检测到烟火))
            {
                return FireAlert.Alert;
            }
            return FireAlert.No;
        }

        private FireAlert GetFire5()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb5车检测到烟火))
            {
                return FireAlert.Alert;
            }
            return FireAlert.No;
        }

        private FireAlert GetFire4()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb4车检测到烟火))
            {
                return FireAlert.Alert;
            }
            return FireAlert.No;
        }

        private FireAlert GetFire3()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb3车检测到烟火))
            {
                return FireAlert.Alert;
            }
            return FireAlert.No;
        }

        private FireAlert GetFire2()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb2车检测到烟火))
            {
                return FireAlert.Alert;
            }
            return FireAlert.No;
        }

        private FireAlert GetFire1()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb1车检测到烟火))
            {
                return FireAlert.Alert;
            }
            return FireAlert.No;
        }

        private VidioState GetVidioStateLeft()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb2车受电弓视频故障))
            {
                return VidioState.Fault;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb2车受电弓视频正常))
            {
                return VidioState.Normal;
            }
            return VidioState.Unkown;
        }
        private VidioState GetVidioStateRight()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb5车受电弓视频故障))
            {
                return VidioState.Fault;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb5车受电弓视频正常))
            {
                return VidioState.Normal;
            }
            return VidioState.Unkown;
        }
        private RunningDirection GetRunningDirection()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb运行方向向左))
            {
                return RunningDirection.Left;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb运行方向向右))
            {
                return RunningDirection.Right;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb运行方向未选择))
            {
                return RunningDirection.None;
            }
            return RunningDirection.Unkown;
        }

        private DriverRoomState GetDriverRoomStateLeft()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb左司机室激活))
            {
                return DriverRoomState.Active;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb左司机室未知))
            {
                return DriverRoomState.Unknow;
            }
            return DriverRoomState.Unactive;
        }
        private DriverRoomState GetDriverRoomStateRight()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb右司机室激活))
            {
                return DriverRoomState.Active;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb右司机室未知))
            {
                return DriverRoomState.Unknow;
            }
            return DriverRoomState.Unactive;
        }
        private PantographState GetPantographLeft()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb2车受电弓升弓到位))
            {
                return PantographState.Uped;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb2车受电弓升弓故障))
            {
                return PantographState.UpFault;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb2车受电弓降弓到位))
            {
                return PantographState.Downed;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb2车受电弓降弓故障))
            {
                return PantographState.DownFault;
            }
            return PantographState.Unkown;
        }

        private PantographState GetPantographRight()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb5车受电弓升弓到位))
            {
                return PantographState.Uped;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb5车受电弓升弓故障))
            {
                return PantographState.UpFault;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb5车受电弓降弓到位))
            {
                return PantographState.Downed;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb5车受电弓降弓故障))
            {
                return PantographState.DownFault;
            }
            return PantographState.Unkown;
        }
        private DriveModel GetDriveModel()
        {

            if (DataService.GetInBoolOf(InbKeys.Inb驾驶模式CM))
            {
                return DriveModel.CM;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb驾驶模式RM))
            {
                return DriveModel.RM;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb驾驶模式URM))
            {
                return DriveModel.URM;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb驾驶模式WM))
            {
                return DriveModel.WM;
            }
            return DriveModel.AM;

        }

        private WorkState GetWorkState()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb工况停车))
            {
                return WorkState.Stop;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb工况常用制动))
            {
                return WorkState.NormalBrake;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb工况快速制动))
            {
                return WorkState.FastBrake;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb工况惰行))
            {
                return WorkState.Coasting;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb工况牵引))
            {
                return WorkState.Tow;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb工况紧急制动))
            {
                return WorkState.EmergBrake;
            }
            return WorkState.Unknow;
        }
    }
}


