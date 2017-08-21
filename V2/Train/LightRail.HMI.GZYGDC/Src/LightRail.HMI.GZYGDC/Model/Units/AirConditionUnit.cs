using System.Collections.Generic;
using System.ComponentModel;
using Excel.Interface;
using LightRail.HMI.GZYGDC.Extension;
using LightRail.HMI.GZYGDC.Interfaces;
using Microsoft.Practices.Prism.ViewModel;
using LightRail.HMI.GZYGDC.Service;

namespace LightRail.HMI.GZYGDC.Model.Units
{
    /// <summary>
    /// 空调单元
    /// </summary>
    [ExcelLocation("广州有轨电车车辆屏界面接口表.xls", "AirConditionUnit")]
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
        /// 空调严重故障
        /// </summary>
        [ExcelField("空调严重故障")]
        public string SeriousFault { get; set; }


        /// <summary>
        /// 空调次要故障
        /// </summary>
        [ExcelField("空调次要故障")]
        public string MinorFault { get; set; }



        /// <summary>
        /// 空调通风
        /// </summary>
        [ExcelField("空调通风")]
        public string Aeration { get; set; }


        /// <summary>
        /// 空调减载
        /// </summary>
        [ExcelField("空调减载")]
        public string Deload { get; set; }


        /// <summary>
        /// 空调预冷
        /// </summary>
        [ExcelField("空调预冷")]
        public string Precool { get; set; }



        /// <summary>
        /// 空调运行
        /// </summary>
        [ExcelField("空调运行")]
        public string Run { get; set; }


        /// <summary>
        /// 空调停止
        /// </summary>
        [ExcelField("空调停止")]
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
            args.UpdateIfContain(SeriousFault, b =>
             {
                 State = m_AirConditionPriorityService.GetPriorityValue(AirConditionState.SeriousFault, b,this);
             });
            args.UpdateIfContain(MinorFault, b =>
            {
                State = m_AirConditionPriorityService.GetPriorityValue(AirConditionState.MinorFault, b, this);
            });
            args.UpdateIfContain(Aeration, b =>
            {
                State = m_AirConditionPriorityService.GetPriorityValue(AirConditionState.Aeration, b, this);
            });
            args.UpdateIfContain(Deload, b =>
            {
                State = m_AirConditionPriorityService.GetPriorityValue(AirConditionState.Deload, b, this);
            });
            args.UpdateIfContain(Precool, b =>
            {
                State = m_AirConditionPriorityService.GetPriorityValue(AirConditionState.Precool, b, this);
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
        /// 空调严重故障
        /// </summary>
        [Description("空调严重故障")]
        SeriousFault,


        /// <summary>
        /// 空调次要故障
        /// </summary>
        [Description("空调次要故障")]
        MinorFault,

        /// <summary>
        /// 空调通风
        /// </summary>
        [Description("空调通风")]
        Aeration,

        /// <summary>
        /// 空调减载
        /// </summary>
        [Description("空调减载")]
        Deload,



        /// <summary>
        /// 空调预冷
        /// </summary>
        [Description("空调预冷")]
        Precool,


        /// <summary>
        /// 空调运行
        /// </summary>
        [Description("空调运行")]
        Run,

        /// <summary>
        /// 空调停止
        /// </summary>
        [Description("空调停止")]
        Off,
    }
}