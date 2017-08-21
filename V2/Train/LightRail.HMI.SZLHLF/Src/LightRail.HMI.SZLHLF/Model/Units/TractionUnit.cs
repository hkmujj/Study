using System.Collections.Generic;
using System.ComponentModel;
using Excel.Interface;
using LightRail.HMI.SZLHLF.Extension;
using LightRail.HMI.SZLHLF.Interfaces;
using LightRail.HMI.SZLHLF.Service;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.SZLHLF.Model.Units
{
    /// <summary>
    /// 牵引单元
    /// </summary>
    [ExcelLocation("龙华车辆屏界面接口表.xls", "TractionUnit")]
    public class TractionUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private TractionState m_State;

        /// <summary>
        /// 
        /// </summary>
        public TractionUnit()
        {
            if (GlobalParam.Instance.InitParam != null)
                m_TractionPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<TractionPriorityService>();
            if (m_TractionPriorityService != null)
            {
                State = m_TractionPriorityService.GetLowPriority();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public TractionState State
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

        private readonly TractionPriorityService m_TractionPriorityService;
        /// <summary>
        /// 车
        /// </summary>
        [ExcelField("车")]
        public int Car { get; set; }


        /// <summary>
        /// 牵引严重故障
        /// </summary>
        [ExcelField("牵引严重故障")]
        public string SeriousFault { get; set; }


        /// <summary>
        /// 牵引次要故障
        /// </summary>
        [ExcelField("牵引次要故障")]
        public string MinorFault { get; set; }

        [ExcelField("牵引正常")]
        /// <summary>
        /// 正常
        /// </summary>
        public string Normal { get; set; }

        [ExcelField("牵引未运行")]
        /// <summary>
        /// 未运行
        /// </summary>
        public string NotOperation { get; set; }



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
                State = m_TractionPriorityService.GetPriorityValue(TractionState.SeriousFault, b, this);
            });
            args.UpdateIfContain(MinorFault, b =>
            {
                State = m_TractionPriorityService.GetPriorityValue(TractionState.MinorFault, b, this);
            });
            args.UpdateIfContain(Normal, b =>
            {
                State = m_TractionPriorityService.GetPriorityValue(TractionState.Normal, b, this);
            });
            args.UpdateIfContain(NotOperation, b =>
            {
                State = m_TractionPriorityService.GetPriorityValue(TractionState.NotOperation, b, this);
            });
        }
    }

    /// <summary>
    /// 牵引状态
    /// </summary>
    public enum TractionState
    {
        [Description("牵引正常")]
        /// <summary>
        /// 正常
        /// </summary>
        Normal,
        [Description("牵引未运行")]
        /// <summary>
        /// 未运行
        /// </summary>
        NotOperation,
        /// <summary>
        /// 牵引严重故障
        /// </summary>
        [Description("牵引严重故障")]
        SeriousFault,
        /// <summary>
        /// 牵引次要故障
        /// </summary>
        [Description("牵引次要故障")]
        MinorFault,
    }
}