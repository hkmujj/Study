using System.Collections.Generic;
using System.ComponentModel;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Subway.ShenZhenLine7.Extention;
using Subway.ShenZhenLine7.Interfaces;
using Subway.ShenZhenLine7.Service;

namespace Subway.ShenZhenLine7.Models.Units
{
    /// <summary>
    /// 空调单元
    /// </summary>
    [ExcelLocation("深圳地铁7号线子系统界面接口表.xls", "AirConditionUnit")]
    public class AirConditionUnit : NotificationObject, ISetValueProvider, IUnit
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AirConditionUnit()
        {
            if (m_AirConditionPriorityService == null)
            {
                m_AirConditionPriorityService = GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<AirConditionPriorityService>();
                if (m_AirConditionPriorityService != null)
                {
                    State = m_AirConditionPriorityService.GetLowPriority();
                }
            }
        }
        private AirConditionState m_State;
        private readonly AirConditionPriorityService m_AirConditionPriorityService;
        /// <summary>
        /// 空调状态
        /// </summary>
        public AirConditionState State
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

        /// <summary>
        /// 车
        /// </summary>
        [ExcelField("车")]
        public int Car { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        [ExcelField("位置")]
        public int Location { get; set; }
        /// <summary>
        /// 空调故障
        /// </summary>
        [ExcelField("空调故障")]
        public string Fault { get; set; }
        /// <summary>
        /// 空调运行
        /// </summary>
        [ExcelField("空调运行")]
        public string Run { get; set; }
        /// <summary>
        /// 空调断开
        /// </summary>
        [ExcelField("空调断开")]
        public string Off { get; set; }
        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }

        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        public void DataChanged(IDictionary<int, bool> args)
        {
            args.UpdateIfContain(Fault, b =>
             {
                 State = m_AirConditionPriorityService.GetPriorityValue(AirConditionState.Fault, b,this);
             });
            args.UpdateIfContain(Run, b =>
            {
                State = m_AirConditionPriorityService.GetPriorityValue(AirConditionState.Run, b,this);
            });
            args.UpdateIfContain(Off, b =>
            {
                State = m_AirConditionPriorityService.GetPriorityValue(AirConditionState.Off, b,this);
            });
        }
    }

    /// <summary>
    /// 空调枚举
    /// </summary>
    public enum AirConditionState
    {
        /// <summary>
        /// 故障
        /// </summary>
        [Description("空调故障/通信中断")]
        Fault,
        /// <summary>
        /// 运行
        /// </summary>
        [Description("空调运行，无故障")]
        Run,
        /// <summary>
        /// 关闭
        /// </summary>
        [Description("空调断开，无故障")]
        Off,
    }
}