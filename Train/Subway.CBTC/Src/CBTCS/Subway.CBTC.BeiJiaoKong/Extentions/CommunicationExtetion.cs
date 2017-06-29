using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Subway.CBTC.BeiJiaoKong.Models;

namespace Subway.CBTC.BeiJiaoKong.Extentions
{
    /// <summary>
    /// 
    /// </summary>
    public static class CommunicationExtetion
    {
        /// <summary>
        /// 发送Bool值
        /// </summary>
        /// <param name="key">需要发送的Key</param>
        /// <param name="value">需要发送的值</param>
        /// <param name="isReset">是否反转值</param>
        /// <param name="milliseconds">反转的间隔</param>
        public static void SendBoolValue(this string key, bool value, bool isReset = false, int milliseconds = 200)
        {
            if (GlobalParams.Instance.InitParam == null)
            {
                return;
            }

            if (!GlobalParams.Instance.ProjectIndexDescription.OutBoolDescriptionDictionary.ContainsKey(key))
            {
                return;
            }
            int index = GlobalParams.Instance.ProjectIndexDescription.OutBoolDescriptionDictionary[key];
            GlobalParams.Instance.InitParam.CommunicationDataService.WriteService.ChangeBool(index, value);
            if (isReset)
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(milliseconds));
                    GlobalParams.Instance.InitParam.CommunicationDataService.WriteService.ChangeBool(index, !value);
                });
            }
        }

        /// <summary>
        /// 发送Float值
        /// </summary>
        /// <param name="key">需要发送的Key</param>
        /// <param name="value">需要发送的值</param>
        public static void SendFloatValue(this string key, float value)
        {
            if (GlobalParams.Instance.InitParam == null)
            {
                return;
            }

            if (!GlobalParams.Instance.ProjectIndexDescription.OutFloatDescriptionDictionary.ContainsKey(key))
            {
                return;
            }
            int index = GlobalParams.Instance.ProjectIndexDescription.OutFloatDescriptionDictionary[key];
            GlobalParams.Instance.InitParam.CommunicationDataService.WriteService.ChangeFloat(index, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public static void IfContain(this IDictionary<int, bool> args, string key, Action<bool> action)
        {
            if (GlobalParams.Instance.InitParam != null)
            {
                var idnex = GlobalParams.Instance.ProjectIndexDescription.InBoolDescriptionDictionary[key];
                if (args.ContainsKey(idnex))
                {
                    if (action!=null)
                    {
                        action.Invoke(args[idnex]);
                    }
                    //action?.Invoke(args[idnex]);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public static void IfContain(this IDictionary<int, float> args, string key, Action<float> action)
        {
            if (GlobalParams.Instance.InitParam != null)
            {
                var idnex = GlobalParams.Instance.ProjectIndexDescription.InFloatDescriptionDictionary[key];
                if (args.ContainsKey(idnex))
                {
                    if (action!=null)
                    {
                        action.Invoke(args[idnex]);
                    }
                    //action?.Invoke(args[idnex]);
                }
            }
        }
    }
}