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
    ///     门单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "StationUnit")]
    public class StationUnit : NotificationObject, ISetValueProvider, IUnit
    {
        /// <summary>
        /// 构造
        /// </summary>
        public StationUnit()
        {
            if (m_StationPriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_StationPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<StationPriorityService>();
                if (m_StationPriorityService != null)
                {
                    State = m_StationPriorityService.GetLowPriority();
                }
            }

        }
        private StationState m_State;

        /// <summary>
        ///     门状态
        /// </summary>
        public StationState State
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
        [ExcelField("门")]
        public int Location { get; set; }
        /// <summary>
        ///  门切除
        /// </summary>
        [ExcelField("门切除")]
        public string Cut { get; set; }

        /// <summary>
        /// 门打开并切除
        /// </summary>
        [ExcelField("门打开并切除")]
        public string OpenAndCut { get; set; }

        /// <summary>
        /// 门故障
        /// </summary>
        [ExcelField("门故障")]
        public string Fault { get; set; }

        /// <summary>
        /// 门释放
        /// </summary>
        [ExcelField("门释放")]
        public string Release { get; set; }

        /// <summary>
        /// 门开
        /// </summary>
        [ExcelField("门开")]
        public string Opened { get; set; }

        /// <summary>
        /// 门关好
        /// </summary>
        [ExcelField("门关")]
        public string Closed { get; set; }

        
        private readonly StationPriorityService m_StationPriorityService;


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
                State = m_StationPriorityService.GetPriorityValue(StationState.Cut, b,this);
            });
            args.UpdateIfContain(OpenAndCut, b =>
            {
                State = m_StationPriorityService.GetPriorityValue(StationState.OpenAndCut, b, this);
            });
            args.UpdateIfContain(Fault, b =>
            {
                State = m_StationPriorityService.GetPriorityValue(StationState.Fault, b, this);
            });
            args.UpdateIfContain(Release, b =>
            {
                State = m_StationPriorityService.GetPriorityValue(StationState.Release, b, this);
            });
            args.UpdateIfContain(Opened, b =>
            {
                State = m_StationPriorityService.GetPriorityValue(StationState.Opened, b, this);
            });
            args.UpdateIfContain(Closed, b =>
            {
                State = m_StationPriorityService.GetPriorityValue(StationState.Closed, b,this);
            });
        }
    }

    /// <summary>
    ///     门状态
    /// </summary>
    public enum StationState
    {
        /// <summary>
        ///　门关
        /// </summary>
        [Description("门关")]
        Closed,

        /// <summary>
        /// 紧急解锁
        /// </summary>
        [Description("门切除")]
        Cut,

        /// <summary>
        /// 门切除
        /// </summary>
        [Description("门打开并切除")]
        OpenAndCut,

        /// <summary>
        /// 门故障
        /// </summary>
        [Description("门故障")]
        Fault,

        /// <summary>
        /// 门故障
        /// </summary>
        [Description("门释放")]
        Release,

        /// <summary>
        /// 门开
        /// </summary>
        [Description("门开")]
        Opened
    }
}
