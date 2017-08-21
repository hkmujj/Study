using System;
using System.Linq;
using MMI.Facility.DataType;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Running;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Control.Service
{
    class RunningLogicCaculate : IRunningLogicCaculate
    {

        private readonly IAppConfig m_AppConfig;

        private readonly IRunningParam m_RunningParam;

        private readonly IAppPostCmdService m_AppPostCmdService;

        private readonly IReadOnlyDictionary<int, bool> m_BoolReadOnlyDictionary;
        private readonly IReadOnlyDictionary<int, float> m_FloatReadOnlyDictionary;

        public RunningLogicCaculate(IAppConfig config, IRunningParam runningParam)
        {
            m_AppConfig = config;
            m_RunningParam = runningParam;
            m_AppPostCmdService = m_RunningParam.AppRunningParamDictionary[config.AppName].AppPostCmdService;
            m_BoolReadOnlyDictionary = m_RunningParam.CommunicationFacadeDataService.GetCommunicationDataService(config).WriteService.ReadOnlyBoolDictionary;
            m_FloatReadOnlyDictionary = m_RunningParam.CommunicationFacadeDataService.GetCommunicationDataService(config).WriteService.ReadOnlyFloatDictionary;
        }

        public void Caculate()
        {
            foreach (var appLogicConfig in m_AppConfig.AppLogicConfig.AppLogicConfigDic.Where(appLogicConfig => appLogicConfig.Value.Enable))
            {
                switch (appLogicConfig.Value.LogicRunType)
                {
                    case LogicType.AND :
                        AndLogic(appLogicConfig.Value);
                        break;
                    case LogicType.OR :
                        OrLogic(appLogicConfig.Value);
                        break;
                    case LogicType.GoToViewsIndex :
                        GoToViewsIndexLogic(appLogicConfig.Value);

                        break;
                    case LogicType.RightIsTrunSetFalse :
                        RightIsTrunSetFalseLogic(appLogicConfig.Value);
                        break;
                    case LogicType.RightIsTrunSetZero :
                        RightIsTrunSetZeroLogic(appLogicConfig.Value);
                        break;
                    default :
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void RightIsTrunSetZeroLogic(IAppLogicConfig iAppLogicConfig)
        {

            foreach (var right in iAppLogicConfig.RightDataList.Where(w => w >= 0 && m_FloatReadOnlyDictionary.ContainsKey(w)))
            {
                m_AppPostCmdService.PostCmdA(GetCurrentViewIndex(),
                    CmdType.SetFloatValue,
                    right,
                    0,
                    0);
            }
        }

        private void RightIsTrunSetFalseLogic(IAppLogicConfig iAppLogicConfig)
        {

            foreach (var right in iAppLogicConfig.RightDataList.Where(w => w >= 0 && m_BoolReadOnlyDictionary.ContainsKey(w)))
            {
                m_AppPostCmdService.PostCmdA(GetCurrentViewIndex(),
                    CmdType.SetBoolValue,
                    right,
                    0,
                    0);
            }
        }

        private void GoToViewsIndexLogic(IAppLogicConfig iAppLogicConfig)
        {
            var tmp = true;

            foreach (var left in iAppLogicConfig.LeftDataList)
            {
                if (m_BoolReadOnlyDictionary.ContainsKey(Math.Abs(left)))
                {
                    tmp &= left > 0 ? m_BoolReadOnlyDictionary[Math.Abs(left)] : !m_BoolReadOnlyDictionary[Math.Abs(left)];
                }
                else
                {
                    // TODO ERROR
                    return;
                }
                if (!tmp)
                {
                    break;
                }
            }

            if (tmp && iAppLogicConfig.RightDataList.Count > 0 && m_BoolReadOnlyDictionary.ContainsKey(iAppLogicConfig.RightDataList[0]))
            {
                m_AppPostCmdService.PostCmdA(GetCurrentViewIndex(), CmdType.ChangePage, iAppLogicConfig.RightDataList[0], 0, 0f);
            }
        }

        private void OrLogic(IAppLogicConfig iAppLogicConfig)
        {
            var tmp = false;

            foreach (var left in iAppLogicConfig.LeftDataList)
            {
                if (m_BoolReadOnlyDictionary.ContainsKey(Math.Abs(left)))
                {
                    tmp |= left > 0 ? m_BoolReadOnlyDictionary[Math.Abs(left)] : !m_BoolReadOnlyDictionary[Math.Abs(left)];
                }
                else
                {
                    //TODO error
                    return;
                }
                if (tmp)
                {
                    break;
                }
            }

            foreach (var right in iAppLogicConfig.RightDataList.Where(w => w >= 0 && m_BoolReadOnlyDictionary.ContainsKey(w)))
            {
                m_AppPostCmdService.PostCmdA(GetCurrentViewIndex(),
                    CmdType.SetBoolValue,
                    right,
                    tmp ? 1 : 0,
                    0);
            }
        }

        private void AndLogic(IAppLogicConfig iAppLogicConfig)
        {
            var tmp = true;
            foreach (var left in iAppLogicConfig.LeftDataList)
            {
                if (m_BoolReadOnlyDictionary.ContainsKey(Math.Abs(left)))
                {
                    tmp &= left > 0 ? m_BoolReadOnlyDictionary[Math.Abs(left)] : !m_BoolReadOnlyDictionary[Math.Abs(left)];
                }
                else
                {
                    // TODO ERROR
                    return;
                }
                if (!tmp)
                {
                    break;
                }
            }


            foreach (var right in iAppLogicConfig.RightDataList.Where(w => w >=0 && m_BoolReadOnlyDictionary.ContainsKey(w)))
            {
                m_AppPostCmdService.PostCmdA(GetCurrentViewIndex(),
                    CmdType.SetBoolValue,
                    right,
                    tmp ? 1 : 0,
                    0);
            }

        }

        private int GetCurrentViewIndex()
        {
            return m_RunningParam.AppRunningParamDictionary[m_AppConfig.AppName].RunningViewParam.CurrentRunningViewUnitParam.ViewConfig.Index;
        }
    }
}
