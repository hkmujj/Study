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
    public class UpdateDomainNotifyProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public UpdateDomainNotifyProvider(IEventAggregator eventAggregator, HXN5BViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }
        
        public override void Initalize(bool isDebugModel)
        {
            EventAggregator.GetEvent<VigilantCountDownEvent>().Subscribe(OnVigilantCountDown);
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            args.ChangedBools.UpdateIfContains(InBKeys.主界面_警惕计时10S, b =>
            {
                if (b)
                {
                    ViewModel.Domain.Controller.StartVigilantCountDown();
                }
                else
                {
                    ViewModel.Domain.Controller.StopVigilantCountDown();
                }
            });
        }

        private void OnVigilantCountDown(VigilantCountDownEvent.Args obj)
        {
            DataService.WriteService.ChangeBool(
                GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[OubKeys.主页面_警惕计时结束],
                obj.State == CountDownState.Stopped);

            if (obj.State == CountDownState.Stopped)
            {
                GlobalTimer.Instance.Tick1S += InstanceOnTick1S;
            }
        }

        private void InstanceOnTick1S(object sender, EventArgs eventArgs)
        {
            ResetCountDownState();
            GlobalTimer.Instance.Tick1S -= InstanceOnTick1S;
        }

        private void ResetCountDownState()
        {
            DataService.WriteService.ChangeBool(
                GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[OubKeys.主页面_警惕计时结束], false);
        }
    }
}