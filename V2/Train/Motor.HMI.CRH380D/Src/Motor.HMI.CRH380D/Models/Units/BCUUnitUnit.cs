using System.Collections.Generic;
using System.ComponentModel;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Motor.HMI.CRH380D.Extension;
using Motor.HMI.CRH380D.Interfaces;
using Motor.HMI.CRH380D.Service;

namespace Motor.HMI.CRH380D.Models.Units
{
    /// <summary>
    ///     BCU单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "BCUUnit")]
    public class BCUUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private bool m_IsChecked;

        /// <summary>
        /// 构造
        /// </summary>
        public BCUUnit()
        {
            if (m_BCUPriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_BCUPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<BCUPriorityService>();
                if (m_BCUPriorityService != null)
                {
                    State = m_BCUPriorityService.GetLowPriority();
                }
            }

        }
        private BCUState m_State;

        /// <summary>
        ///     BCU状态
        /// </summary>
        public BCUState State
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
        /// 编号
        /// </summary>
        [ExcelField("编号")]
        public int Num { get; set; }

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
        ///  BCU故障
        /// </summary>
        [ExcelField("BCU故障")]
        public string Fault { get; set; }

        /// <summary>
        /// BCU正常运行
        /// </summary>
        [ExcelField("BCU正常运行")]
        public string TurnOn { get; set; }
        
        private readonly BCUPriorityService m_BCUPriorityService;


        /// <summary />
        /// <param name="propertyOrFieldName" />
        /// <param name="value" />
        public void SetValue(string propertyOrFieldName, string value)
        {
            
        }

        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        public void DataChanged(IDictionary<int, bool> args)
        {
            args.UpdateIfContain(TurnOn, b =>
            {
                State = m_BCUPriorityService.GetPriorityValue(BCUState.TurnOn, b, this);
            });
            args.UpdateIfContain(Fault, b =>
            {
                State = m_BCUPriorityService.GetPriorityValue(BCUState.Fault, b, this);
            });
        }
    }

    /// <summary>
    ///     BCU状态
    /// </summary>
    public enum BCUState
    {
        /// <summary>
        /// BCU正常运行
        /// </summary>
        [Description("BCU正常运行")]
        TurnOn,
        /// <summary>
        ///　BCU故障
        /// </summary>
        [Description("BCU故障")]
        Fault,
    }
}
