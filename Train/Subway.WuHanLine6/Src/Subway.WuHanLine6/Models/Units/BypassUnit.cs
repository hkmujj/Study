using System.Collections.Generic;

namespace Subway.WuHanLine6.Models.Units
{
    /// <summary>
    /// 旁路信息单元
    /// </summary>
    public class BypassUnit : UnitBase
    {
        private bool? m_States;

        /// <summary>
        /// 状态
        /// </summary>
        public bool? States
        {
            get { return m_States; }
            set
            {
                if (value == m_States)
                {
                    return;
                }
                m_States = value;
                RaisePropertyChanged(() => States);
            }
        }

        /// <summary>
        /// 车
        /// </summary>
        public int Car { get; set; }

        /// <summary>
        /// Bool值改变
        /// </summary>
        /// <param name="args"></param>
        public override void Changed(IDictionary<int, bool> args)
        {
        }
    }
}