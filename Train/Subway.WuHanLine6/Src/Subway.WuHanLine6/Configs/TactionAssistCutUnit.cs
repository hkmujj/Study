using System;
using System.Collections.Generic;
using System.Linq;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Models;

namespace Subway.WuHanLine6.Configs
{

    /// <summary>
    /// 
    /// </summary>
    [ExcelLocation("牵引辅助切除复位接口.xls", "Sheet1")]
    public sealed class TactionAssistCutUnit : NotificationObject, ISetValueProvider
    {
        private bool m_IsEnable;
        private bool m_IsCheck;

        /// <summary>
        /// 
        /// </summary>
        public TactionAssistCutUnit()
        {
            IsEnable = true;
            IsCheck = false;
        }
        /// <summary>
        /// 编号
        /// </summary>
        [ExcelField("编号")]
        public int Index { get; set; }

        /// <summary>
        /// 关联ID
        /// </summary>
        [ExcelField("关联ID")]
        public int ReleadIndex { get; set; }
        /// <summary>
        /// 按下发送
        /// </summary>
        [ExcelField("按下发送")]
        public string Down { get; set; }

        /// <summary>
        /// 弹起发送
        /// </summary>
        [ExcelField("弹起发送")]
        public string Up { get; set; }

        /// <summary>
        /// Check状态改变
        /// </summary>
        public event Action<bool> IsCheckChanged;
        /// <summary>
        /// Check状态
        /// </summary>
        public bool IsCheck
        {
            get { return m_IsCheck; }
            set
            {
                if (value == m_IsCheck)
                {
                    return;
                }
                m_IsCheck = value;
                OnIsCheckChanged(value);
                RaisePropertyChanged(() => IsCheck);
            }
        }

        /// <summary>
        /// 是否可以使用
        /// </summary>
        public bool IsEnable
        {
            get { return m_IsEnable; }
            set
            {
                if (value == m_IsEnable)
                {
                    return;
                }
                m_IsEnable = value;
                RaisePropertyChanged(() => IsEnable);
            }
        }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }

        private void OnIsCheckChanged(bool obj)
        {
            IsCheckChanged?.Invoke(obj);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class TractionAssistCutFactory
    {
        static TractionAssistCutFactory()
        {
            Instance = new TractionAssistCutFactory();
        }
        /// <summary>
        /// 
        /// </summary>
        public static TractionAssistCutFactory Instance { get; private set; }
        private readonly List<TactionAssistCutUnit> m_AllList;
        /// <summary>
        /// 
        /// </summary>
        private TractionAssistCutFactory()
        {
            m_AllList = ExcelParser.Parser<TactionAssistCutUnit>(GlobalParams.Instance.AppConfigPath).ToList();
            Load();
        }

        private void Load()
        {
            m_AllList.ForEach(f => f.IsCheckChanged += (b) =>
            {
                if (f.ReleadIndex != 0)
                {
                    var tmp = m_AllList.FirstOrDefault(d => d.Index == f.ReleadIndex);
                    if (tmp != null)
                    {
                        tmp.IsEnable = !b;
                    }
                }
                if (string.IsNullOrEmpty(f.Up))
                {
                    f.Down.SendBoolData(b, true);
                }
                else
                {
                    if (b)
                    {
                        f.Down.SendBoolData(true);
                    }
                    else
                    {
                        f.Down.SendBoolData(false);
                        f.Up.SendBoolData(true);
                    }
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TactionAssistCutUnit> GetAllUnit()
        {
            return m_AllList;
        }
        /// <summary>
        /// 获取TactionAssistCutUnit
        /// </summary>
        /// <param name="index">编号</param>
        /// <returns></returns>
        public TactionAssistCutUnit GetTractionUnit(int index)
        {
            return m_AllList.FirstOrDefault(f => f.Index == index);
        }

    }
}