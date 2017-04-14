using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Running;

namespace MMI.Facility.Control.Service
{
    class NormalModelAppPostCmdService : AppPostCmdServiceBase
    {
        public NormalModelAppPostCmdService(IRunningParamManager runningParamManager, string appName, ProjectType projectType)
            : base(runningParamManager, appName, projectType)
        {
        }

        public override void PostCmdA(int index, CmdType nCT, int paraA, int paraB, float paraC)
        {
            switch (nCT)
            {
                case CmdType.ChangePage:
                    var currentRunningParam = m_RunningParamManager.RunningParam.AppRunningParamDictionary[AppName].RunningViewParam;
                    currentRunningParam.CurrentRunningViewUnitParam = currentRunningParam.ViewUnitParamDic[paraA];

                    break;
                case CmdType.SetBoolValue:
                    {
                        CommunicationDataService.WriteService.ChangeBool(paraA, paraB == 1);
                    }
                    break;
                case CmdType.SetFloatValue:
                    {
                        CommunicationDataService.WriteService.ChangeFloat(paraA, paraC);
                    }
                    break;
                case CmdType.SetInBoolValue:
                    // ignore , nothing to do 
                    break;
                case CmdType.SetInFloatValue:
                    // ignore , nothing to do 
                    break;
            }
        }
    }
}
