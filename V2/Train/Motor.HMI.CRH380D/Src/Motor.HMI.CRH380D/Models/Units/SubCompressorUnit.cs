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
    ///     辅助压缩机单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "SubCompressorUnit")]
    public class SubCompressorUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private bool m_IsChecked;

        /// <summary>
        /// 构造
        /// </summary>
        public SubCompressorUnit()
        {
            if (m_SubCompressorPriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_SubCompressorPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<SubCompressorPriorityService>();
                if (m_SubCompressorPriorityService != null)
                {
                    State = m_SubCompressorPriorityService.GetLowPriority();
                }
            }

        }
        private SubCompressorState m_State;

        /// <summary>
        ///     辅助压缩机状态
        /// </summary>
        public SubCompressorState State
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
        ///  辅助压缩机故障
        /// </summary>
        [ExcelField("辅助压缩机故障")]
        public string Fault { get; set; }

        /// <summary>
        /// 辅助压缩机运行
        /// </summary>
        [ExcelField("辅助压缩机运行")]
        public string TurnOn { get; set; }

        /// <summary>
        /// 辅助压缩机未运行
        /// </summary>
        [ExcelField("辅助压缩机未运行")]
        public string TurnOff { get; set; }
        

        private readonly SubCompressorPriorityService m_SubCompressorPriorityService;


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
                State = m_SubCompressorPriorityService.GetPriorityValue(SubCompressorState.Fault, b, this);
            });
            args.UpdateIfContain(TurnOn, b =>
            {
                State = m_SubCompressorPriorityService.GetPriorityValue(SubCompressorState.TurnOn, b, this);
            });
            args.UpdateIfContain(TurnOff, b =>
            {
                State = m_SubCompressorPriorityService.GetPriorityValue(SubCompressorState.TurnOff, b, this);
            });
        }
    }

    /// <summary>
    ///     辅助压缩机状态
    /// </summary>
    public enum SubCompressorState
    {
        /// <summary>
        /// 辅助压缩机未运行
        /// </summary>
        [Description("辅助压缩机未运行")]
        TurnOff,

        /// <summary>
        ///　辅助压缩机故障
        /// </summary>
        [Description("辅助压缩机故障")]
        Fault,

        /// <summary>
        /// 辅助压缩机运行
        /// </summary>
        [Description("辅助压缩机运行")]
        TurnOn,
        
    }
}
