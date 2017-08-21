using System;
using System.Collections.Generic;
using System.Linq;
using Subway.ShenZhenLine7.Models;

namespace Subway.ShenZhenLine7.Extention
{
    /// <summary>
    /// 网络数据扩展
    /// </summary>
    public static class NetWorkDataExtention
    {
        /// <summary>
        /// 操作数据
        /// </summary>
        /// <param name="value">数据集合</param>
        /// <param name="key">key</param>
        /// <param name="action">委托</param>
        public static void UpdateIfContain(this IDictionary<int, bool> value, string key, Action<bool> action)
        {
            Action<bool, int> ac = new Action<bool, int>(((b, i) => { }));
            ac.Invoke(true, 1);
            if (GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary.ContainsKey(key))
            {
                var index = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[key];
                if (value.ContainsKey(index))
                {
                    action?.Invoke(value[index]);
                }
            }
        }
        /// <summary>
        /// 操作数据
        /// </summary>
        /// <param name="value">数据集合</param>
        /// <param name="key">key</param>
        /// <param name="action">委托</param>
        public static void UpdateIfContainWhenTrue(this IDictionary<int, bool> value, string key, Action action)
        {
            if (GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary.ContainsKey(key))
            {
                var index = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[key];
                if (value.ContainsKey(index) && value[index])
                {
                    action?.Invoke();
                }
            }
        }

        /// <summary>
        /// 操作数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="keyActions"></param>
        /// <param name="normalAction"></param>
        public static void UpdateIfContainWhenAllTrue(this IDictionary<int, bool> value, IDictionary<string, Action> keyActions, Action normalAction)
        {
            if (keyActions.Any(a => GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary.ContainsKey(a.Key) && keyActions.Any(s => value.ContainsKey(GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[s.Key]))))
            {
                normalAction?.Invoke();
                keyActions.ForEach(((s, action) => UpdateIfContainWhenTrue(value, s, action)));
            }
        }

        /// <summary>
        /// 数据操作
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public static void UpdateIfContain(this IDictionary<int, float> value, string key, Action<float> action)
        {
            if (GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary.ContainsKey(key))
            {
                var index = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[key];
                if (value.ContainsKey(index))
                {
                    action?.Invoke(value[index]);
                }
            }
        }

        /// <summary>
        /// 发送Bool变量
        /// </summary>
        /// <param name="key">需要设定的接口名称</param>
        /// <param name="value">需要设定的值</param>
        /// <param name="isRest">是否重置为未设定以前的值 默认不重置</param>
        /// <param name="isInBool"></param>
        public static void SendBoolValue(this string key, bool value, bool isRest = false, bool isInBool = false)
        {
            if (GlobalParam.Instance.InitParam == null)
            {
                return;
            }
            if (isInBool)
            {
                if (GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary.ContainsKey(key))
                {
                    GlobalParam.Instance.InitParam.CommunicationDataService.WritableReadService.ChangeBool(
                        GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[key], value);
                }
                return;
            }

            if (GlobalParam.Instance.IndexConfig.OutBoolDescriptionDictionary.ContainsKey(key))
            {
                GlobalParam.Instance.InitParam.CommunicationDataService.WriteService.ChangeBool(
                    GlobalParam.Instance.IndexConfig.OutBoolDescriptionDictionary[key], value);
            }

            if (isRest)
            {
                GlobalParam.Instance.InitParam.CommunicationDataService.WriteService.ChangeBool(
                    GlobalParam.Instance.IndexConfig.OutBoolDescriptionDictionary[key], !value);
            }
        }

        /// <summary>
        /// 发送float变量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SendFloatValue(this string key, float value)
        {
            if (GlobalParam.Instance.IndexConfig.OutFloatDescriptionDictionary.ContainsKey(key))
            {
                GlobalParam.Instance.InitParam.CommunicationDataService.WriteService.ChangeFloat(
                    GlobalParam.Instance.IndexConfig.OutFloatDescriptionDictionary[key], value);
            }
        }
    }
}