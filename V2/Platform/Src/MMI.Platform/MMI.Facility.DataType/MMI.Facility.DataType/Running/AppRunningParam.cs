using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Running;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.DataType.Running
{
    public class AppRunningParam : IAppRunningParam
    {
        public string AppName { get; set; }

        public ProjectType ProjectType { get { return AppConfig.ProjectType; } }

        public IAppConfig AppConfig { get; set; }

        public ICommunicationDataService CommunicationDataService { set; get; }

        public IRunningParam ParentParam { get; set; }

        public IRunningViewParam RunningViewParam { get; set; }

        public IAppPostCmdService AppPostCmdService { get; set; }

        public IRunningLogicCaculate RunningLogicCaculate { get; set; }

        public IIndexInterpreter IndexInterpreter { get;  set; }
    }
}
