using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380BG.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Event;
using MMI.Facility.Interface.Service;
using Motor.HMI.CRH380BG.Extension;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.Resources.Keys;

namespace Motor.HMI.CRH380BG.Controller
{
    [Export]
    public class CRH380BGDebugger : ControllerBase<CRH380BGViewModel>
    {
        public IEventAggregator EventAggregator { get; private set; }

        [ImportingConstructor]
        public CRH380BGDebugger(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }

        public override void Initalize()
        {
            EventAggregator.GetEvent<CourseStateChangedEvent>()
                .Subscribe(OnCourseStateChanged, ThreadOption.UIThread);

            ResetValues();
        }

        private void ResetValues()
        {
            if (GlobalParam.Instance.InitParam != null)
            {
                var ws = GlobalParam.Instance.InitParam.CommunicationDataService.WritableReadService;
                ws.ChangedInBoolOf(InBoolKeys.Inb制动屏黑屏, true);
                ws.ChangedInBoolOf(InBoolKeys.Inb非制动屏黑屏, true);
            }

            ViewModel.StateViewModel.Model.IsVisble = true;
        }

        private void OnCourseStateChanged(CourseStateChangedArgs args)
        {
            if (args.CourseService.CurrentCourseState != CourseState.Started)
            {
                return;
            }

            ResetValues();
        }
    }
}
