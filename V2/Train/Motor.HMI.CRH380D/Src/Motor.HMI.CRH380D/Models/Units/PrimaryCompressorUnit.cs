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
    ///     主压缩机单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "PrimaryCompressorUnit")]
    public class PrimaryCompressorUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private bool m_IsChecked;

        /// <summary>
        /// 构造
        /// </summary>
        public PrimaryCompressorUnit()
        {
            if (m_PrimaryCompressorPriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_PrimaryCompressorPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<PrimaryCompressorPriorityService>();
                if (m_PrimaryCompressorPriorityService != null)
                {
                    State = m_PrimaryCompressorPriorityService.GetLowPriority();
                }
            }

        }
        private PrimaryCompressorState m_State;

        /// <summary>
        ///     主压缩机状态
        /// </summary>
        public PrimaryCompressorState State
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
        ///  主压缩机故障
        /// </summary>
        [ExcelField("主压缩机故障")]
        public string Fault { get; set; }

        /// <summary>
        /// 主压缩机切除
        /// </summary>
        [ExcelField("主压缩机切除")]
        public string Cut { get; set; }

        /// <summary>
        /// 主压缩机运行
        /// </summary>
        [ExcelField("主压缩机运行")]
        public string TurnOn { get; set; }

        /// <summary>
        /// 主压缩机未运行
        /// </summary>
        [ExcelField("主压缩机未运行")]
        public string TurnOff { get; set; }

         /// <summary>
        /// 主压缩机手动运行
        /// </summary>
        [ExcelField("主压缩机手动运行")]
        public string ManualTurnOn { get; set; }

        /// <summary>
        /// 主压缩机切入切除
        /// </summary>
        [ExcelField("主压缩机切入切除")]
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

        private readonly PrimaryCompressorPriorityService m_PrimaryCompressorPriorityService;


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
            args.UpdateIfContain(Cut, b =>
            {
                State = m_PrimaryCompressorPriorityService.GetPriorityValue(PrimaryCompressorState.Cut, b, this);
            });
            args.UpdateIfContain(Fault, b =>
            {
                State = m_PrimaryCompressorPriorityService.GetPriorityValue(PrimaryCompressorState.Fault, b, this);
            });
            args.UpdateIfContain(TurnOn, b =>
            {
                State = m_PrimaryCompressorPriorityService.GetPriorityValue(PrimaryCompressorState.TurnOn, b, this);
            });
            args.UpdateIfContain(TurnOff, b =>
            {
                State = m_PrimaryCompressorPriorityService.GetPriorityValue(PrimaryCompressorState.TurnOff, b, this);
            });
            args.UpdateIfContain(ManualTurnOn, b =>
            {
                State = m_PrimaryCompressorPriorityService.GetPriorityValue(PrimaryCompressorState.ManualTurnOn, b, this);
            });
        }
    }

    /// <summary>
    ///     主压缩机状态
    /// </summary>
    public enum PrimaryCompressorState
    {
        /// <summary>
        /// 主压缩机未运行
        /// </summary>
        [Description("主压缩机未运行")]
        TurnOff,

        /// <summary>
        ///　主压缩机故障
        /// </summary>
        [Description("主压缩机故障")]
        Fault,

        /// <summary>
        /// 主压缩机切除
        /// </summary>
        [Description("主压缩机切除")]
        Cut,
        
        /// <summary>
        /// 主压缩机运行
        /// </summary>
        [Description("主压缩机运行")]
        TurnOn,
        /// <summary>
        /// 主压缩机手动运行
        /// </summary>
        [Description("主压缩机手动运行")]
        ManualTurnOn,
    }
}
