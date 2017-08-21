#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-9
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：公共组件-No.3-获取故障信息
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace LightRail.Ethiopia.TMS.Pub
{
    /// <summary>
    /// 功能描述：公共组件-No.3-获取故障信息
    /// 创建人：唐林
    /// 创建时间：2014-7-9
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class VC_C4_GetFault : baseClass
    {
        #region 私有变量
        private List<FaultInfo> _listFault = new List<FaultInfo>();//本地故障列表
        private Int32 readTxtID = 0;//文本标志
        #endregion

        #region 属性
        /// <summary>
        /// 获取当前故障列表属性
        /// </summary>
        public static List<FaultInfo> CurrentFaults
        {
            get { return _currentFaults; }
        }
        private static List<FaultInfo> _currentFaults = new List<FaultInfo>();

        /// <summary>
        /// 获取所有发生故障列表属性
        /// </summary>
        public static List<FaultInfo> AllFaults
        {
            get { return _allFaults; }
        }
        private static List<FaultInfo> _allFaults = new List<FaultInfo>();

        /// <summary>
        /// 读取或设置当前最高优先级的故障属性
        /// </summary>
        public static FaultInfo CurrentFault { get; set; }
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共视图-获取故障";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            LoadFaultInfo();

            return true;
        }

        private void LoadFaultInfo()
        {
            var file = Path.Combine(AppConfig.AppPaths.ConfigDirectory, "故障信息.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (FaultInfo fault in all.Select(cStr => cStr.Split(new char[] { '\t' })).Select(split => new FaultInfo()
            {
                Logic = Convert.ToInt32(split[0]),
                Code = split[1],
                Name = split[2],
                Grade = Convert.ToInt32(split[3])
            }))
            {
                _listFault.Add(fault);
            }
        }

        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            getFaultValue();

            base.paint(dcGs);
        }

        /// <summary>
        /// 获取故障网络值
        /// </summary>
        private void getFaultValue()
        {
            //遍历本地故障列表
            _listFault.ForEach(a =>
            {
                if (BoolList[a.Logic])//发生相应故障
                {
                    if (CurrentFaults.Find(b => a.Logic == b.Logic) == null)//当前故障中不存在该故障，当前列表添加相应故障，记录发生时间
                    {
                        String time = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + DateTime.Now.ToLongTimeString();
                        FaultInfo fault = new FaultInfo() { Code = a.Code, Name = a.Name, Grade = a.Grade, Logic = a.Logic, DateTime = time };
                        CurrentFaults.Insert(0, fault);
                        AllFaults.Insert(0, fault);
                    }
                }
                else//故障正常
                {
                    FaultInfo fault = CurrentFaults.Find(b => a.Logic == b.Logic);
                    if (fault != null)//当前故障列表中已有该故障，记录故障结束时间，当前故障列表移除该故障
                    {
                        fault.OverTime = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + DateTime.Now.ToLongTimeString();
                        CurrentFaults.Remove(a);
                    }
                }
            });

            //当前最高等级故障
            if (CurrentFaults == null || CurrentFaults.Count == 0)
            {
                CurrentFault = null;
                return;
            }

            //获取当前最高优先级故障
            FaultInfo faultClone = CurrentFaults[0];
            for (int i = 0; i < CurrentFaults.Count; i++)
            {
                if (faultClone.Grade < CurrentFaults[i].Grade)
                {
                    faultClone = CurrentFaults[i];
                }
            }
            CurrentFault = faultClone;
        }
        #endregion
    }
}
