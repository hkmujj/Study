using System;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Running;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Control.Service
{
    abstract class AppPostCmdServiceBase : IAppPostCmdService
    {
        // ReSharper disable once InconsistentNaming
        protected IRunningParamManager m_RunningParamManager;

        protected AppPostCmdServiceBase(IRunningParamManager runningParamManager, string appName, ProjectType projectType)
        {
            m_RunningParamManager = runningParamManager;
            AppName = appName;
            ProjectType = projectType;
        }

        public ProjectType ProjectType { private set; get; }

        protected ICommunicationDataService CommunicationDataService
        {
            get
            {
                return
                    m_RunningParamManager.RunningParam.CommunicationFacadeDataService.GetCommunicationDataService(ProjectType, AppName);
            }
        }

        public string AppName { get; private set; }

        public abstract void PostCmdA(int index, CmdType nCT, int paraA, int paraB, float paraC);

        public virtual void PostCmdB(int index, CmdType nCT, int paraA, int paraB, float paraC, string str)
        {
            throw new NotImplementedException();
        }
    }
}
