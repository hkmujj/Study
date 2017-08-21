using System;
using System.Collections.Generic;
using System.Linq;
using MMI.Facility.Interface.Data;
using Subway.ShenZhenLine11.Config;

namespace Subway.ShenZhenLine11.Extention
{
    public static class CommunicationArgsExtention
    {
        public static bool ContainKeyTrue(this CommunicationDataChangedArgs<bool> value, int index)
        {
            return value.NewValue.ContainsKey(index) && value.NewValue[index];
        }

        public static void IfContanin(this string key, Action<bool> action)
        {
            if (GlobalParam.Instance.IndexConfig != null && GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary.ContainsKey(key))
            {
                var index = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[key];
                action?.Invoke(GlobalParam.Instance.InitPara.CommunicationDataService.ReadService.ReadOnlyBoolDictionary[index]);
            }
        }

        public static void IfContaninTrue(this string key, Action action)
        {
            if (GlobalParam.Instance.IndexConfig != null && GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary.ContainsKey(key))
            {
                var index = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[key];
                if (GlobalParam.Instance.InitPara.CommunicationDataService.ReadService.ReadOnlyBoolDictionary[index])
                {
                    action?.Invoke();
                }
            }
        }

        public static void UpdateAllState(this IDictionary<string, Action> tryeActions, Action normalAction = null)
        {
            if (tryeActions != null)
            {
                normalAction?.Invoke();
                tryeActions.Keys.ToList().ForEach(f=>IfContaninTrue(f,tryeActions[f]));
            }
        }
    }
}