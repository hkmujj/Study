using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Extension;
using Engine.TPX21F.HXN5B.Model;
using Engine.TPX21F.HXN5B.Resources.Keys;
using Engine.TPX21F.HXN5B.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Event;
using MMI.Facility.Interface.Service;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TPX21F.HXN5B.Controller
{
    [Export]
    public class HXN5BDebugger : ControllerBase<HXN5BViewModel>
    {
        public IEventAggregator EventAggregator { get; private set; }

        [ImportingConstructor]
        public HXN5BDebugger(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }

        public override void Initalize()
        {
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试1 "));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试2 "));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试3 "));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试4 "));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试5 "));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试6 "));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试7 "));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试8 "));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试9 "));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试10"));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试11"));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试12"));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试13"));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试14"));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试15"));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试16"));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试17"));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试18"));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试19"));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试20"));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试21"));
            //ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(new NotifyInfoConfig("测试22"));
            EventAggregator.GetEvent<CourseStateChangedEvent>()
                .Subscribe(OnCourseStateChanged, ThreadOption.UIThread);

            ResetValues();
        }

        private void ResetValues()
        {
            if (GlobalParam.Instance.InitParam != null)
            {
                var ws = GlobalParam.Instance.InitParam.CommunicationDataService.WritableReadService;
                ws.ChangedInBoolOf(InBKeys.MMI亮屏, true);
            }

            ViewModel.Model.IsVisble = true;
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