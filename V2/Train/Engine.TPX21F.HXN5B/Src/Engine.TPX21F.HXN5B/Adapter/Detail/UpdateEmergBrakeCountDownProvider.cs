using System;
using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Event;
using Engine.TPX21F.HXN5B.Extension;
using Engine.TPX21F.HXN5B.Model;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Engine.TPX21F.HXN5B.Resources.Keys;
using Engine.TPX21F.HXN5B.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;

namespace Engine.TPX21F.HXN5B.Adapter.Detail
{
    [Export(typeof(IUpdateDataProvider))]
    public class UpdateEmergBrakeCountDownProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public UpdateEmergBrakeCountDownProvider(IEventAggregator eventAggregator, HXN5BViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void Initalize(bool isDebugModel)
        {
            EventAggregator.GetEvent<EmergerBrakeCountDownEvent>().Subscribe(OnEmergerBrakeCountDown);
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            UpdateBrakeSysEvents(args);
        }


        private void UpdateBrakeSysEvents(CommunicationDataChangedArgs args)
        {
            var ev = ViewModel.Domain.BrakeSysViewModel.EventViewModel;

            args.ChangedBools.UpdateIfContains(InBKeys.制动系统_紧急制动解锁倒计时_60,
                b =>
                {
                    if (b)
                    {
                        ev.Controller.StartEmergerCountDown();
                    }
                    else
                    {
                        ev.Controller.StopEmergerCountDown();
                    }
                });

            args.ChangedBools.UpdateIfContains(InBKeys.制动系统_需将大闸从紧急位置运转位充风解锁, b => ev.Model.EmergerBrakeUnlockFlag = b);
        }

        private void OnEmergerBrakeCountDown(EmergerBrakeCountDownEvent.Args obj)
        {
            DataService.WriteService.ChangeBool(
                GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[OubKeys.制动系统_紧急制动解锁倒计时_60结束],
                obj.State == CountDownState.Stopped);

            if (obj.State == CountDownState.Stopped)
            {
                GlobalTimer.Instance.Tick1S += InstanceOnTick1S;
            }
        }

        private void InstanceOnTick1S(object sender, EventArgs e)
        {
            ResetCountDownState();
            GlobalTimer.Instance.Tick1S -= InstanceOnTick1S;
        }

        private void ResetCountDownState()
        {
            DataService.WriteService.ChangeBool(
                GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[OubKeys.制动系统_紧急制动解锁倒计时_60结束], false);
        }
    }
}