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
            args.ChangedBools.UpdateIfContains(InbKeys.Inb����������, b => ViewModel.Model.Visible = b);

            args.ChangedFloats.UpdateIfContains(InfKeys.Inf�г��ٶ�, f => ViewModel.TrainViewModel.Model.Speed = f);
            //args.ChangedFloats.UpdateIfContains(InfKeys.Inf�г��ٶ�, f => ViewModel.TrainViewModel.Model.WorkState= (WorkState) f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf��ѹֵ, f => ViewModel.TrainViewModel.Model.TouchNetV = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf����ֵ, f => ViewModel.TrainViewModel.Model.TouchNetA = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Infǣ�����ٷֱ�, f => ViewModel.TrainViewModel.Model.TowPercent = f * 0.1f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf����, f => ViewModel.TrainViewModel.Model.LimitedSpeed = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf�ƶ����ٷֱ�, f => ViewModel.TrainViewModel.Model.BrakePercent = f * 0.1f);

            args.ChangedBools.UpdateIfContains(InbKeys.Inb��·��ť��·, b =>
            {
                ViewModel.Model.BypassState = b ? NavigationButtonState.Warning : NavigationButtonState.Normal;
            });
            args.ChangedBools.UpdateIfContains(InbKeys.Inb���ϰ�ť����, b =>
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
            if (DataService.GetInBoolOf(InbKeys.Inb1���г�����))
            {
                return CouplingState.Active;
            }
            return CouplingState.Unactive;
        }
        private CouplingState GetCouplingRight()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb6���г�����))
            {
                return CouplingState.Active;
            }
            return CouplingState.Unactive;
        }




        private FireAlert GetFire6()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb6����⵽�̻�))
            {
                return FireAlert.Alert;
            }
            return FireAlert.No;
        }

        private FireAlert GetFire5()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb5����⵽�̻�))
            {
                return FireAlert.Alert;
            }
            return FireAlert.No;
        }

        private FireAlert GetFire4()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb4����⵽�̻�))
            {
                return FireAlert.Alert;
            }
            return FireAlert.No;
        }

        private FireAlert GetFire3()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb3����⵽�̻�))
            {
                return FireAlert.Alert;
            }
            return FireAlert.No;
        }

        private FireAlert GetFire2()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb2����⵽�̻�))
            {
                return FireAlert.Alert;
            }
            return FireAlert.No;
        }

        private FireAlert GetFire1()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb1����⵽�̻�))
            {
                return FireAlert.Alert;
            }
            return FireAlert.No;
        }

        private VidioState GetVidioStateLeft()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb2���ܵ繭��Ƶ����))
            {
                return VidioState.Fault;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb2���ܵ繭��Ƶ����))
            {
                return VidioState.Normal;
            }
            return VidioState.Unkown;
        }
        private VidioState GetVidioStateRight()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb5���ܵ繭��Ƶ����))
            {
                return VidioState.Fault;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb5���ܵ繭��Ƶ����))
            {
                return VidioState.Normal;
            }
            return VidioState.Unkown;
        }
        private RunningDirection GetRunningDirection()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb���з�������))
            {
                return RunningDirection.Left;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb���з�������))
            {
                return RunningDirection.Right;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb���з���δѡ��))
            {
                return RunningDirection.None;
            }
            return RunningDirection.Unkown;
        }

        private DriverRoomState GetDriverRoomStateLeft()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb��˾���Ҽ���))
            {
                return DriverRoomState.Active;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb��˾����δ֪))
            {
                return DriverRoomState.Unknow;
            }
            return DriverRoomState.Unactive;
        }
        private DriverRoomState GetDriverRoomStateRight()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb��˾���Ҽ���))
            {
                return DriverRoomState.Active;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb��˾����δ֪))
            {
                return DriverRoomState.Unknow;
            }
            return DriverRoomState.Unactive;
        }
        private PantographState GetPantographLeft()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb2���ܵ繭������λ))
            {
                return PantographState.Uped;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb2���ܵ繭��������))
            {
                return PantographState.UpFault;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb2���ܵ繭������λ))
            {
                return PantographState.Downed;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb2���ܵ繭��������))
            {
                return PantographState.DownFault;
            }
            return PantographState.Unkown;
        }

        private PantographState GetPantographRight()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb5���ܵ繭������λ))
            {
                return PantographState.Uped;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb5���ܵ繭��������))
            {
                return PantographState.UpFault;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb5���ܵ繭������λ))
            {
                return PantographState.Downed;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb5���ܵ繭��������))
            {
                return PantographState.DownFault;
            }
            return PantographState.Unkown;
        }
        private DriveModel GetDriveModel()
        {

            if (DataService.GetInBoolOf(InbKeys.Inb��ʻģʽCM))
            {
                return DriveModel.CM;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb��ʻģʽRM))
            {
                return DriveModel.RM;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb��ʻģʽURM))
            {
                return DriveModel.URM;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb��ʻģʽWM))
            {
                return DriveModel.WM;
            }
            return DriveModel.AM;

        }

        private WorkState GetWorkState()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb����ͣ��))
            {
                return WorkState.Stop;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb���������ƶ�))
            {
                return WorkState.NormalBrake;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb���������ƶ�))
            {
                return WorkState.FastBrake;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb��������))
            {
                return WorkState.Coasting;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb����ǣ��))
            {
                return WorkState.Tow;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb���������ƶ�))
            {
                return WorkState.EmergBrake;
            }
            return WorkState.Unknow;
        }
    }
}


