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
    ///     LCB单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "LCBUnit")]
    public class LCBUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private bool m_IsChecked;

        /// <summary>
        /// 构造
        /// </summary>
        public LCBUnit()
        {
            if (m_LCBPriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_LCBPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<LCBPriorityService>();
                if (m_LCBPriorityService != null)
                {
                    State = m_LCBPriorityService.GetLowPriority();
                }
            }

        }
        private LCBState m_State;

        /// <summary>
        ///     LCB状态
        /// </summary>
        public LCBState State
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
        ///  LCB切除
        /// </summary>
        [ExcelField("LCB切除")]
        public string CutOff { get; set; }

        /// <summary>
        /// LCB故障
        /// </summary>
        [ExcelField("LCB故障")]
        public string Fault { get; set; }

        /// <summary>
        /// LCB闭合
        /// </summary>
        [ExcelField("LCB闭合")]
        public string TurnOn { get; set; }

        /// <summary>
        /// LCB断开
        /// </summary>
        [ExcelField("LCB断开")]
        public string TurnOff { get; set; }

        /// <summary>
        /// LCB切入切除
        /// </summary>
        [ExcelField("LCB切入切除")]
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

        private readonly LCBPriorityService m_LCBPriorityService;


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
            args.UpdateIfContain(Fault, b =>
            {
                State = m_LCBPriorityService.GetPriorityValue(LCBState.Fault, b, this);
            });
            args.UpdateIfContain(TurnOn, b =>
            {
                State = m_LCBPriorityService.GetPriorityValue(LCBState.TurnOn, b, this);
            });
            args.UpdateIfContain(Fault, b =>
            {
                State = m_LCBPriorityService.GetPriorityValue(LCBState.Fault, b, this);
            });
            args.UpdateIfContain(TurnOff, b =>
            {
                State = m_LCBPriorityService.GetPriorityValue(LCBState.TurnOff, b, this);
            });
            args.UpdateIfContain(CutOff, b =>
            {
                State = m_LCBPriorityService.GetPriorityValue(LCBState.CutOff, b, this);
            });
        }
    }

    /// <summary>
    ///     LCB状态
    /// </summary>
    public enum LCBState
    {
        /// <summary>
        /// LCB断开
        /// </summary>
        [Description("LCB断开")]
        TurnOff,

        /// <summary>
        ///　LCB切除
        /// </summary>
        [Description("LCB切除")]
        CutOff,

        /// <summary>
        /// LCB故障
        /// </summary>
        [Description("LCB故障")]
        Fault,

        /// <summary>
        /// LCB闭合
        /// </summary>
        [Description("LCB闭合")]
        TurnOn,

    }
}
