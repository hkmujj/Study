using System;
using System.Collections.Generic;
using System.ComponentModel;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Subway.ShenZhenLine9.Extention;
using Subway.ShenZhenLine9.Interfaces;
using Subway.ShenZhenLine9.Service;

namespace Subway.ShenZhenLine9.Models.Units
{
    /// <summary>
    /// 
    /// </summary>
    [ExcelLocation("深圳地铁7号线子系统界面接口表.xls", "AssistUnit")]
    public class AssistUnit : NotificationObject, IUnit, ISetValueProvider
    {
        private AssistState m_State;

        /// <summary>
        /// 
        /// </summary>
        public AssistUnit()
        {
            m_AssistPriorityService = GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<AssistPriorityService>();
            if (m_AssistPriorityService != null)
            {
                State = m_AssistPriorityService.GetLowPriority();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public AssistState State
        {
            get { return m_State; }
            set
            {
                if (value == m_State)
                {
                    return;
                }
                m_State = value;
                RaisePropertyChanged(() => State);
            }
        }

        private readonly AssistPriorityService m_AssistPriorityService;
        /// <summary>
        /// 车
        /// </summary>
        [ExcelField("车")]
        public int Car { get; set; }
        /// <summary>
        /// 位置  1 辅助变流器 2 充电机
        /// </summary>
        [ExcelField("位置")]
        public int Location { get; set; }
        /// <summary>
        /// 故障
        /// </summary>
        [ExcelField("故障")]
        public string Fault { get; set; }
        /// <summary>
        /// 运行
        /// </summary>
        [ExcelField("运行")]
        public string Run { get; set; }
        /// <summary>
        /// 断开
        /// </summary>
        [ExcelField("断开")]
        public string Off { get; set; }
        /// <summary>
        /// 切除
        /// </summary>
        [ExcelField("切除")]
        public string Cut { get; set; }
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        public void DataChanged(IDictionary<int, bool> args)
        {

            args.UpdateIfContain(Fault, b =>
            {
                State = m_AssistPriorityService.GetPriorityValue(this, IsDC() ? AssistState.DCFault : AssistState.ACFault, b);
            });
            args.UpdateIfContain(Run, b =>
            {
                State = m_AssistPriorityService.GetPriorityValue(this, IsDC() ? AssistState.DCRun : AssistState.ACRun, b);
            });
            args.UpdateIfContain(Off, b =>
            {

                State = m_AssistPriorityService.GetPriorityValue(this, IsDC() ? AssistState.DCOff : AssistState.ACOff, b);
            });
            args.UpdateIfContain(Cut, b =>
            {

                State = m_AssistPriorityService.GetPriorityValue(this, IsDC() ? AssistState.DCCut : AssistState.ACCut, b);
            });
        }
        private bool IsDC()
        {
            return (Car == 1 && Location == 1) || (Car == 6 && Location == 2);
        }
        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum AssistState
    {
        /// <summary>
        /// 辅助变流器故障
        /// </summary>
        ACFault,
        /// <summary>
        /// 充电机故障
        /// </summary>
        [Description("辅助变流器/充电机故障")]
        DCFault,
        /// <summary>
        /// 辅助变流器运行
        /// </summary>
        ACRun,
        /// <summary>
        /// 充电机运行
        /// </summary>
        [Description("辅助变流器/充电机运行，无故障")]
        DCRun,
        /// <summary>
        /// 辅助变流器断开
        /// </summary>
        ACOff,
        /// <summary>
        /// 充电机断开
        /// </summary>
        [Description("辅助变流器/充电机断开，无故障")]
        DCOff,
        /// <summary>
        /// 辅助变流器切除
        /// </summary>
        ACCut,

        /// <summary>
        /// 充电机切除
        /// </summary>
        [Description("辅助变流器/充电机切除")]
        DCCut,
    }

    public enum Generatrix
    {
        /// <summary>
        /// 母线断路器闭合
        /// </summary>
        Close,
        /// <summary>
        /// 母线断路器断开
        /// </summary>
        Open
    }
}