using System;
using CommonUtil.Util;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Motor.HMI.CRH380D.Interfaces;
using Motor.HMI.CRH380D.Models.State;

namespace Motor.HMI.CRH380D.Models.ConfigModel
{
    /// <summary>
    /// 事件信息
    /// </summary>
    [ExcelLocation("事件信息表.xls", "EventInfo")]
    public class EventInfo : NotificationObject, IEventInfo, ISetValueProvider
    {
        static EventInfo()
        {
            Empty = new EventInfo();
        }
        /// <summary>
        /// 空
        /// </summary>
        public static readonly EventInfo Empty;

        private bool m_IsChecked;

        /// <summary>
        /// 显示编号
        /// </summary>
        public int? Number { get; set; }

        /// <summary>
        /// 逻辑号
        /// </summary>
        [ExcelField("逻辑号")]
        public int Index { get; set; }

        /// <summary>
        /// 所属系统
        /// </summary>
        [ExcelField("所属系统")]
        public EventSystemState System { get; set; }

        /// <summary>
        /// 车厢号
        /// </summary>
        [ExcelField("车厢号")]
        public int CarNum { get; set; }

        /// <summary>
        /// 故障代码
        /// </summary>
        [ExcelField("故障代码")]
        public string FaultCode { get; set; }

        /// <summary>
        /// 单元
        /// </summary>
       [ExcelField("单元")]
        public int UnitNum { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        [ExcelField("故障等级")]
        public EventLevel Level { get; set; }

        /// <summary>
        /// 是否选择
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

        /// <summary>
        /// 是否确认
        /// </summary>
        public bool IsConfirm { get; set; }

        /// <summary>
        /// 故障内容
        /// </summary>
        [ExcelField("故障内容")]
        public string Describe { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime? HappenTime { get; set; }
        /// <summary>
        /// 故障帮助信息
        /// </summary>
        [ExcelField("故障帮助信息")]
        public string HelpInfo { get; set; }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEventInfo Clone<T>() where T : IEventInfo
        {
            var t = new EventInfo
            {
                Index = Index,
                System=System,
                CarNum = CarNum,
                FaultCode = FaultCode,
                UnitNum=UnitNum,
                Describe = Describe,
                IsConfirm = false,
                Level = Level,
                HelpInfo = HelpInfo,
            };
            return t;
        }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Level":
                    try
                    {
                        Level = (EventLevel)int.Parse(value);
                    }
                    catch (Exception e)
                    {
                        AppLog.Error(string.Format("事件信息转换失败 字段：{0} 值：{1}", propertyOrFieldName, value));
                        AppLog.Error(e.ToString());
                    }
                    break;

                case "System":
                    try
                    {
                        EventSystemState type;
                        Enum.TryParse(value, true, out type);
                        System = type;
                    }
                    catch (Exception e)
                    {
                        AppLog.Error(string.Format("事件信息转换失败 字段：{0} 值：{1}", propertyOrFieldName, value));
                        AppLog.Error(e.ToString());
                    }
                    break;
            }
        }
    }
}