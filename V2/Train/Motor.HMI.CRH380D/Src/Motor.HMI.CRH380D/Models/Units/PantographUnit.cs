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
    ///     受电弓单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "PantographUnit")]
    public class PantographUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private bool m_IsChecked;

        /// <summary>
        /// 构造
        /// </summary>
        public PantographUnit()
        {
            if (m_PantographPriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_PantographPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<PantographPriorityService>();
                if (m_PantographPriorityService != null)
                {
                    State = m_PantographPriorityService.GetLowPriority();
                }
            }

        }
        private PantographState m_State;

        /// <summary>
        ///     受电弓状态
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
        /// 门
        /// </summary>
        [ExcelField("位置")]
        public int Location { get; set; }
        /// <summary>
        ///  受电弓切除
        /// </summary>
        [ExcelField("受电弓切除")]
        public string CutOff { get; set; }

        /// <summary>
        /// 受电弓升起并故障
        /// </summary>
        [ExcelField("受电弓升起并故障")]
        public string TurnUpFault { get; set; }

        /// <summary>
        /// 受电弓降下并故障
        /// </summary>
        [ExcelField("受电弓降下并故障")]
        public string TurnDownFault { get; set; }

        /// <summary>
        /// 受电弓升起
        /// </summary>
        [ExcelField("受电弓升起")]
        public string TurnUp { get; set; }

        /// <summary>
        /// 受电弓降下
        /// </summary>
        [ExcelField("受电弓降下")]
        public string TurnDown { get; set; }

        /// <summary>
        /// 受电弓切入切除
        /// </summary>
        [ExcelField("受电弓切入切除")]
        public string CutInAndCutOFF { get; set; }

        /// <summary>
        /// 选择状态
        /// </summary>
        public bool IsChecked
        {
            get { return m_IsChecked; }
            set
            {
                if (value == m_IsChecked)
                {
                    return;
                }
                m_IsChecked = value;
                RaisePropertyChanged(() => IsChecked);
            }
        }

        private readonly PantographPriorityService m_PantographPriorityService;


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
            args.UpdateIfContain(CutOff, b =>
            {
                State = m_PantographPriorityService.GetPriorityValue(PantographState.CutOff, b, this);
            });
            args.UpdateIfContain(TurnUpFault, b =>
            {
                State = m_PantographPriorityService.GetPriorityValue(PantographState.TurnUpFault, b, this);
            });
            args.UpdateIfContain(TurnDownFault, b =>
            {
                State = m_PantographPriorityService.GetPriorityValue(PantographState.TurnDownFault, b, this);
            });
            args.UpdateIfContain(TurnUp, b =>
            {
                State = m_PantographPriorityService.GetPriorityValue(PantographState.TurnUp, b, this);
            });
            args.UpdateIfContain(TurnDown, b =>
            {
                State = m_PantographPriorityService.GetPriorityValue(PantographState.TurnDown, b, this);
            });
        }
    }

    /// <summary>
    ///     受电弓状态
    /// </summary>
    public enum PantographState
    {
        /// <summary>
        /// 受电弓降下
        /// </summary>
        [Description("受电弓降下")]
        TurnDown,
        /// <summary>
        ///　受电弓切除
        /// </summary>
        [Description("受电弓切除")]
        CutOff,

        /// <summary>
        /// 受电弓升起并故障
        /// </summary>
        [Description("受电弓升起并故障")]
        TurnUpFault,

        /// <summary>
        /// 受电弓降下并故障
        /// </summary>
        [Description("受电弓降下并故障")]
        TurnDownFault,

        /// <summary>
        /// 受电弓升起
        /// </summary>
        [Description("受电弓升起")]
        TurnUp,
    }
}
