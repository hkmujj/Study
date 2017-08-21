using System;
using Engine.TAX2.SS7C.Constant;
using Engine.TAX2.SS7C.Events;
using Engine.TAX2.SS7C.Extension;
using Engine.TAX2.SS7C.Model.Domain.Constant;
using Engine.TAX2.SS7C.Model.Domain.TrainState.TAX;
using Engine.TAX2.SS7C.View.Contents.TrainState.TAX2Info;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    public abstract class ChangeToTAX2ActionResponserBase : BtnActionResponserBase
    {
        protected string StateKey { set; get; }

        protected Lazy<TAXModelBase> TAXModelBase { set; get; }

        protected ChangeToTAX2ActionResponserBase()
        {
            ServiceLocator.Current.GetInstance<IEventAggregator>()
                .GetEvent<CurrentTAXStateChangedEvent>()
                .Subscribe(OnCurrentTAXStateChangedEvent, ThreadOption.UIThread, false,
                    args => args.Source == TAXModelBase.Value);
        }

        private void OnCurrentTAXStateChangedEvent(CurrentTAXStateChangedEvent.Args obj)
        {
            ResponseClick();
        }

        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            NavigateTo(StateKey);
            NavigateByTAX2CommunicationState(TAXModelBase.Value);
        }

        protected void NavigateByTAX2CommunicationState(TAXModelBase model)
        {
            TAX2CommunicationState state = model.TAX2CommunicationState;
            if (state.IsCommunicationFail())
            {
                RegionManager.RequestNavigate(RegionNames.ContentDownContent, typeof(CommunicationFailView).FullName);
            }
            ViewModel.Value.TrainStateViewModel.TAX2ViewModel.Model.CurrentSelectedModel = model;
        }
    }
}