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
    /// 弹簧单元
    /// </summary>
    [ExcelLocation("广州有轨电车车辆屏界面接口表.xls", "SpringUnit")]
    public class SpringUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private SpringState m_State;

        /// <summary>
        /// 
        /// </summary>
        public SpringUnit()
        {
            m_SpringPriorityService = GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<SpringPriorityService>();
            if (m_SpringPriorityService != null)
            {
                State = m_SpringPriorityService.GetLowPriority();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public SpringState State
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

        private readonly SpringPriorityService m_SpringPriorityService;
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
        /// 弹簧缓解
        /// </summary>
        [ExcelField("弹簧缓解")]
        public string Relief { get; set; }


        /// <summary>
        /// 弹簧施加
        /// </summary>
        [ExcelField("弹簧施加")]
        public string Infliction { get; set; }


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
            args.UpdateIfContain(Relief, b =>
            {
                State = m_SpringPriorityService.GetPriorityValue(SpringState.Relief, b, this);
            });
            args.UpdateIfContain(Infliction, b =>
            {
                State = m_SpringPriorityService.GetPriorityValue(SpringState.Infliction, b, this);
            });
         

        }
    }

    /// <summary>
    /// 牵引状态
    /// </summary>
    public enum SpringState
    {
        /// <summary>
        /// 弹簧缓解
        /// </summary>
        [Description("弹簧缓解")]
        Relief,
        /// <summary>
        /// 弹簧施加
        /// </summary>
        [Description("弹簧施加")]
        Infliction,
    }
}