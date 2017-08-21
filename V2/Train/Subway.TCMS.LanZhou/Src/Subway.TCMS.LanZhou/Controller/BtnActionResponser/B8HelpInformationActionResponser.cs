using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using CommonUtil.Model;
using Microsoft.Practices.Prism.Events;
using Subway.TCMS.LanZhou.Event;
using Subway.TCMS.LanZhou.Model.BtnStragy;
using Subway.TCMS.LanZhou.View.Contents;
using Subway.TCMS.LanZhou.View.Contents.AirCondition;
using Subway.TCMS.LanZhou.View.Contents.BreakDownInformation;
using Subway.TCMS.LanZhou.View.Contents.ExamineAndRepair;
using Subway.TCMS.LanZhou.View.Contents.HelpInformation;

namespace Subway.TCMS.LanZhou.Controller.BtnActionResponser
{
    [Export]
    class B8HelpInformationActionResponser : OrdinaryActionResponser
    {
        protected Type m_CurentContentViewType;

        /// <summary>
        ///  key :  
        /// value :
        /// </summary>
        private IReadOnlyDictionary<Type, Type> m_ContentHelpviewDictionary;

        

        public B8HelpInformationActionResponser()
        {
            EventAggregator.GetEvent<ViewChangedEvent>().Subscribe(OnViewChangedEvent);

            m_ContentHelpviewDictionary = new ReadOnlyDictionary<Type, Type>(new Dictionary<Type, Type>()
            {
                {typeof(RunningView), typeof(RunningHelpView) },
                {typeof(TrainStatusCommonView), typeof(TrainStatusHelpView) },
                {typeof(AirConditionSetting), typeof(AirConditionSettingHelpView) },
                {typeof(AirConditionStatus), typeof(AirConditionStatusHelpView) }         
            });

            m_CurentContentViewType = typeof (RunningView);
        }

        private void OnViewChangedEvent(ViewChangedEvent.Args args)
        {
            m_CurentContentViewType = args.TargetViewType;
        }

        public override void UpdateState()
        {
            BtnStateProvider state = StateProvider;
            state.IsEnabled = !(m_CurentContentViewType == typeof(FireAlarm) || m_CurentContentViewType == typeof(ExamineAndRepairLogin) || m_CurentContentViewType == typeof(CommunicationStatus));
            state.IsSelected = false;

            base.UpdateState();
        }

        public override void ResponseClick()                        
        {

            if (m_CurentContentViewType != null && m_ContentHelpviewDictionary.ContainsKey(m_CurentContentViewType))
            {
                RequestNavigateToContent(m_ContentHelpviewDictionary[m_CurentContentViewType]);
                StateProvider.IsSelected = true;
                TowStatusButtonSelected(true);
            }
            else
            {
                
            }
        }
    }
}
