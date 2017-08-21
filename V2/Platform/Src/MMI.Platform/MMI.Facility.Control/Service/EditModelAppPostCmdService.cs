using System.Collections.Generic;
using MMI.Facility.DataType.Data;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Running;

namespace MMI.Facility.Control.Service
{
    class EditModelAppPostCmdService : AppPostCmdServiceBase
    {
        public EditModelAppPostCmdService(IRunningParamManager runningParamManager, string appName, ProjectType projectType)
            : base(runningParamManager, appName, projectType)
        {
        }

        public override void PostCmdA(int index, CmdType nCT, int paraA, int paraB, float paraC)
        {
            switch (nCT)
            {
                case CmdType.ChangePage:
                    {
                        var currentRunningParam = m_RunningParamManager.RunningParam.AppRunningParamDictionary[AppName].RunningViewParam;
                        currentRunningParam.CurrentRunningViewUnitParam = currentRunningParam.ViewUnitParamDic[paraA];

                    }
                    break;
                case CmdType.SetBoolValue:
                    {
                        if (!CommunicationDataService.WriteService.ReadOnlyBoolDictionary.ContainsKey(paraA))
                        {
                            throw new KeyNotFoundException(string.Format("Can not set out bool value where index ={0}", paraA));
                        }
                        CommunicationDataService.WriteService.ChangeBool(paraA, paraB == 1);
                    }
                    break;
                case CmdType.SetFloatValue:
                    {
                        if (!CommunicationDataService.WriteService.ReadOnlyFloatDictionary.ContainsKey(paraA))
                        {
                            throw new KeyNotFoundException(string.Format("Can not set out float value where index ={0}", paraA));
                        }
                        CommunicationDataService.WriteService.ChangeFloat(paraA, paraC);
                    }
                    break;
                case CmdType.SetInBoolValue:
                    {
                        var dataService = CommunicationDataService.ReadService as CommunicatonDataServiceBase;
                        if (dataService != null)
                        {
                            if (!dataService.ReadOnlyBoolDictionary.ContainsKey(paraA))
                            {
                                throw new KeyNotFoundException(string.Format("Can not set in bool value where index ={0}", paraA));
                            }
                            dataService.ChangeBool(paraA, paraB == 1, true);
                        }
                    }
                    break;
                case CmdType.SetInFloatValue:
                    {
                        var dataService = CommunicationDataService.ReadService as CommunicatonDataServiceBase;
                        if (dataService != null)
                        {
                            if (!dataService.ReadOnlyFloatDictionary.ContainsKey(paraA))
                            {
                                throw new KeyNotFoundException(string.Format("Can not set in float value where index ={0}", paraA));
                            }
                            dataService.ChangeFloat(paraA, paraC, true);
                        }
                    }
                    break;
            }
        }
    }
}
