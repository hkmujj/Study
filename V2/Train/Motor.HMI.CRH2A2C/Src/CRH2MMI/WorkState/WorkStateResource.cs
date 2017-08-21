using System;
using System.Collections.Generic;
using System.Linq;
using CommonUtil.Util;
using CRH2MMI.Common.Global;

namespace CRH2MMI.WorkState
{
    class WorkStateResource
    {
        private readonly WorkStateView m_WorkStateView;

        public static WorkStateResource Instance { private set; get; }

        private List<LevelConfig> m_LevelConfigs;

        public WorkStateResource(WorkStateView workStateView)
        {
            m_WorkStateView = workStateView;
            Instance = this;
            Init();
        }

        private void Init()
        {
            m_LevelConfigs = new List<LevelConfig>(GlobalInfo.CurrentCRH2Config.WorkStateConfig.LevenConfigs.Configs);
            m_LevelConfigs.Sort(
                (config1, config2) => config2.Priority.CompareTo(config1.Priority));
        }

        public string GetLevel()
        {
            foreach (var level in m_LevelConfigs)
            {
                if (m_WorkStateView.BoolList[m_WorkStateView.GetInBoolIndex(level.InBoolColoumNames.First())])
                {
                    return level.Name;
                }
            }
            return "OFF";
        }

        public BrakeType GetBrakeType()
        {
            var types = Enum.GetValues(typeof(BrakeType)).Cast<BrakeType>().ToList();
            types.Remove(BrakeType.None);
            var packConfigs = GlobalInfo.CurrentCRH2Config.WorkStateConfig.ParkConfigs;

            var brake = BrakeType.None;

            types.ForEach(e =>
            {
                if (m_WorkStateView.BoolList[m_WorkStateView.GetInBoolIndex(
                            packConfigs.Find(f => f.Name == EnumUtil.GetDescription(e).First())
                                .InBoolColoumNames.First())])
                {
                    brake |= e;
                }
            });

            return brake;
        }
    }
}
