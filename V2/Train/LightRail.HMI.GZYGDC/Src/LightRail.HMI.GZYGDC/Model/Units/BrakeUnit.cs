using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CommonUtil.Util;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using LightRail.HMI.GZYGDC.Extension;
using LightRail.HMI.GZYGDC.Interfaces;
using LightRail.HMI.GZYGDC.Service;

namespace LightRail.HMI.GZYGDC.Model.Units
{
    /// <summary>
    /// 制动单元
    /// </summary>
    [ExcelLocation("广州有轨电车车辆屏界面接口表.xls", "BrakeUnit")]
    public class BrakeUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private BrakeState m_State;

        /// <summary>
        /// 
        /// </summary>
        public BrakeUnit()
        {
            m_BrakePriorityService = GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<BrakePriorityService>();
            if (m_BrakePriorityService != null)
            {
                State = m_BrakePriorityService.GetLowPriority();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public BrakeState State
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

        private readonly BrakePriorityService m_BrakePriorityService;
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
        /// 制动严重故障
        /// </summary>
        [ExcelField("制动严重故障")]
        public string SeriousFault { get; set; }


        /// <summary>
        /// 制动次要故障
        /// </summary>
        [ExcelField("制动次要故障")]
        public string MinorFault { get; set; }

        /// <summary>
        /// 制动紧急缓解
        /// </summary>
        [ExcelField("制动紧急缓解")]
        public string EmergencyRelief { get; set; }

        /// <summary>
        /// 制动施加
        /// </summary>
        [ExcelField("制动施加")]
        public string Infliction { get; set; }

        /// <summary>
        /// 制动缓解
        /// </summary>
        [ExcelField("制动缓解")]
        public string Relief { get; set; }


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
                State = m_BrakePriorityService.GetPriorityValue(BrakeState.SeriousFault, b, this);
            });
            args.UpdateIfContain(MinorFault, b =>
            {
                State = m_BrakePriorityService.GetPriorityValue(BrakeState.MinorFault, b, this);
            });
            args.UpdateIfContain(EmergencyRelief, b =>
            {
                State = m_BrakePriorityService.GetPriorityValue(BrakeState.EmergencyRelief, b, this);
            });
            args.UpdateIfContain(Infliction, b =>
            {
                State = m_BrakePriorityService.GetPriorityValue(BrakeState.Infliction, b, this);
            });
            args.UpdateIfContain(Relief, b =>
            {
                State = m_BrakePriorityService.GetPriorityValue(BrakeState.Relief, b, this);
            });
           

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum BrakeState
    {
        /// <summary>
        /// 制动电源严重故障
        /// </summary>
        [Description("制动电源严重故障")]
        SeriousFault,
        /// <summary>
        /// 制动电源次要故障
        /// </summary>
        [Description("制动电源次要故障")]
        MinorFault,
        /// <summary>
        /// 制动紧急缓解
        /// </summary>
        [Description("制动紧急缓解")]
        EmergencyRelief,
        /// <summary>
        /// 制动施加
        /// </summary>
        [Description("制动施加")]
        Infliction,
        /// <summary>
        /// 制动缓解
        /// </summary>
        [Description("制动缓解")]
        Relief,
       
    }
}