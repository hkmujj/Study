using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Subway.WuHanLine6.Models;

namespace Subway.WuHanLine6.Extention
{
    /// <summary>
    ///
    /// </summary>
    public static class CommunicationExtention
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public static void UpdateIfContainWhereTrue(this IDictionary<int, bool> dic, string key, Action<bool> action)
        {
            if (GlobalParams.Instance.IndexConfig.InBoolDescriptionDictionary.ContainsKey(key))
            {
                var index = GlobalParams.Instance.IndexConfig.InBoolDescriptionDictionary[key];
                if (dic.ContainsKey(index))
                {
                    if (dic[index])
                    {
                        if (action != null)
                        {
                            action.Invoke(dic[index]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 多条件更新值
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="keyAction"></param>
        /// <param name="normalAction"></param>
        public static void UpdateAllTrue(this IDictionary<int, bool> dic, IDictionary<string, Action<bool>> keyAction, Action normalAction)
        {
            if (keyAction.Any(a => GlobalParams.Instance.IndexConfig.InBoolDescriptionDictionary.ContainsKey(a.Key)) && keyAction.Any(a => dic.ContainsKey(GlobalParams.Instance.IndexConfig.InBoolDescriptionDictionary[a.Key])))
            {
                if (normalAction != null)
                {
                    normalAction.Invoke();
                }
                foreach (var action in keyAction)
                {
                    UpdateIfContainWhereTrue(dic, action.Key, action.Value);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public static void UpdateIfContain(this IDictionary<int, bool> dic, string key, Action<bool> action)
        {
            if (GlobalParams.Instance.IndexConfig.InBoolDescriptionDictionary.ContainsKey(key))
            {
                var index = GlobalParams.Instance.IndexConfig.InBoolDescriptionDictionary[key];
                if (dic.ContainsKey(index))
                {
                    if (action != null)
                    {
                        action.Invoke(dic[index]);
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public static void UpdateIfContain(this IDictionary<int, float> dic, string key, Action<float> action)
        {
            if (GlobalParams.Instance.IndexConfig.InFloatDescriptionDictionary.ContainsKey(key))
            {
                var index = GlobalParams.Instance.IndexConfig.InFloatDescriptionDictionary[key];
                if (dic.ContainsKey(index))
                {
                    action?.Invoke(dic[index]);
                }
            }
        }

        /// <summary>
        /// 发送Bool值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="logic"></param>
        /// <param name="isClear">是否清楚发送数据 true清除 false 不清除</param>
        public static void SendBoolData(this string logic, bool value, bool isClear = false)
        {
            if (GlobalParams.Instance.IndexConfig == null)
            {
                return;
            }
            if (GlobalParams.Instance.IndexConfig.OutBoolDescriptionDictionary.ContainsKey(logic))
            {
                GlobalParams.Instance.InitParam.CommunicationDataService.WriteService.ChangeBool(
                    GlobalParams.Instance.IndexConfig.OutBoolDescriptionDictionary[logic], value);
                if (isClear)
                {
                    ThreadPool.QueueUserWorkItem((satet) =>
                    {
                        Thread.Sleep(100);
                        GlobalParams.Instance.InitParam.CommunicationDataService.WriteService.ChangeBool(
                   GlobalParams.Instance.IndexConfig.OutBoolDescriptionDictionary[logic], false);
                    });
                }
            }
        }

        /// <summary>
        /// 发送Float值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="logic"></param>
        public static void SendFloatData(this string logic, float value)
        {
            if (GlobalParams.Instance.IndexConfig == null)
            {
                return;
            }
            if (GlobalParams.Instance.IndexConfig.OutFloatDescriptionDictionary.ContainsKey(logic))
            {
                GlobalParams.Instance.InitParam.CommunicationDataService.WriteService.ChangeFloat(
                    GlobalParams.Instance.IndexConfig.OutFloatDescriptionDictionary[logic], value);
            }
        }
    }
}