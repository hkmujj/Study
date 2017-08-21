using System.Collections.Generic;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Subway.ShenZhenLine7.Extention;

namespace Subway.ShenZhenLine7.Models.Units
{
    /// <summary>
    /// 旁路单元
    /// </summary>
    [ExcelLocation("旁路信息.xls", "Sheet1")]
    public class BypassUnit : NotificationObject, ISetValueProvider
    {
        private bool m_Status6;
        private bool m_Status1;

        /// <summary>
        /// 编号
        /// </summary>
        [ExcelField("编号")]
        public int Index { get; set; }

        /// <summary>
        /// 旁路
        /// </summary>
        [ExcelField("旁路")]
        public string Bypass { get; set; }
        /// <summary>
        /// 逻辑号1
        /// </summary>
        [ExcelField("逻辑号1")]
        public string Logic1 { get; set; }
        /// <summary>
        /// 逻辑号6
        /// </summary>
        [ExcelField("逻辑号6")]
        public string Logic6 { get; set; }

        /// <summary>
        /// 状态1
        /// </summary>
        public bool Status1
        {
            get { return m_Status1; }
            set
            {
                if (value == m_Status1)
                {
                    return;
                }
                m_Status1 = value;
                RaisePropertyChanged(() => Status1);
            }
        }

        /// <summary>
        /// 状态6
        /// </summary>
        public bool Status6
        {
            get { return m_Status6; }
            set
            {
                if (value == m_Status6)
                {
                    return;
                }
                m_Status6 = value;
                RaisePropertyChanged(() => Status6);
            }
        }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }

        /// <summary>
        /// 数据改变
        /// </summary>
        /// <param name="args"></param>
        public void DataChanged(IDictionary<int, bool> args)
        {
            args.UpdateIfContain(Logic1, b => Status1 = b);
            args.UpdateIfContain(Logic6, b => Status6 = b);
        }
    }
}