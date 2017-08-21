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
    /// 受电弓单元
    /// </summary>
    [ExcelLocation("广州有轨电车车辆屏界面接口表.xls", "PantographUnit")]
    public class PantographUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private PantographState m_State;

        /// <summary>
        /// 
        /// </summary>
        public PantographUnit()
        {
            m_PantographPriorityService = GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<PantographPriorityService>();
            if (m_PantographPriorityService != null)
            {
                State = m_PantographPriorityService.GetLowPriority();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public PantographState State
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

        private readonly PantographPriorityService m_PantographPriorityService;
        /// <summary>
        /// 车
        /// </summary>
        [ExcelField("车")]
        public int Car { get; set; }


   

        /// <summary>
        /// 受电弓升起
        /// </summary>
        [ExcelField("受电弓升起")]
        public string Rise { get; set; }


        /// <summary>
        /// 受电弓落下
        /// </summary>
        [ExcelField("受电弓落下")]
        public string Ecptoma { get; set; }


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
            args.UpdateIfContain(Rise, b =>
            {
                State = m_PantographPriorityService.GetPriorityValue(PantographState.Rise, b, this);
            });
            args.UpdateIfContain(Ecptoma, b =>
            {
                State = m_PantographPriorityService.GetPriorityValue(PantographState.Ecptoma, b, this);
            });
         

        }
    }

    /// <summary>
    /// 牵引状态
    /// </summary>
    public enum PantographState
    {
        /// <summary>
        /// 受电弓升起
        /// </summary>
        [Description("受电弓升起")]
        Rise,
        /// <summary>
        /// 受电弓落下
        /// </summary>
        [Description("受电弓落下")]
        Ecptoma,
    }
}