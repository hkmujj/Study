using System;
using System.Windows.Threading;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using Subway.TCMS.Infrasturcture.Evnts;

namespace Subway.TCMS.Infrasturcture.Extention
{
    public static class CommunicationExtention
    {
        /// <summary>
        /// 更新Bool量
        /// </summary>
        /// <param name="args">数据变化参数以及接口服务</param>
        /// <param name="indexName">接口名称</param>
        /// <param name="action">需要封送的操作委托</param>
        /// <param name="isMainThread">是否封送到主线程</param>
        public static void UpdateIfContanin(this CommunicationDataChangedWrapperArgs<bool> args, string indexName, Action<bool> action, bool isMainThread = false)
        {
            if (args.ProjectDecriptionService.InBoolDictionary.ContainsKey(indexName))
            {
                if (isMainThread)
                {
                    App.Current.GetMainDispatcher().BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        args.DataChangedArgs.UpdateIfContains(args.ProjectDecriptionService.InBoolDictionary[indexName], action);
                    }));
                }
                else
                {
                    args.DataChangedArgs.UpdateIfContains(args.ProjectDecriptionService.InBoolDictionary[indexName], action);
                }

            }
            else
            {
                AppLog.Warn("找不到接口名称：{0}", indexName);
            }
        }
        /// <summary>
        /// 更新Float量
        /// </summary>
        /// <param name="args">数据变化参数以及接口服务</param>
        /// <param name="indexName">接口名称</param>
        /// <param name="action">需要封送的操作委托</param>
        /// <param name="isMainThread">是否封送到主线程</param>
        public static void UpdateIfContanin(this CommunicationDataChangedWrapperArgs<float> args, string indexName, Action<float> action, bool isMainThread = false)
        {
            if (args.ProjectDecriptionService.InFloatDictionary.ContainsKey(indexName))
            {
                if (isMainThread)
                {
                    App.Current.GetMainDispatcher().BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        args.DataChangedArgs.UpdateIfContains(args.ProjectDecriptionService.InFloatDictionary[indexName], action);
                    }));
                }
                else
                {
                    args.DataChangedArgs.UpdateIfContains(args.ProjectDecriptionService.InFloatDictionary[indexName], action);
                }
            }
            else
            {
                AppLog.Warn("找不到接口名称：{0}", indexName);
            }
        }
    }
}
