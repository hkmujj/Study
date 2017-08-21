using System;
using System.Drawing;
using CommonUtil.Controls.Button;

namespace CRH2MMI.Common.Control
{
    partial class CRH2Button
    {

        /// <summary>
        /// 闪烁策略
        /// </summary>
        [Obsolete]
        public class CRH2BtnGlintBehavierStrategy : CRH2BtnBehavierStrategy
        {
            /// <summary>
            /// 闪烁周期，越大越慢, 默认为uint.MaxValue 不闪烁, 为0 一直不显示 
            /// </summary>
            public uint Period
            {
                set
                {
                    m_Period = value;
                    m_DoublePeriod = uint.MaxValue;
                    if (m_Period != uint.MaxValue)
                    {
                        m_DoublePeriod = value * 2;
                    }
                }
                get { return m_Period; }
            }

            private uint m_DoublePeriod;

            private uint m_Current;
            private uint m_Period;

            public CRH2BtnGlintBehavierStrategy(GDIButton control)
                : base(control)
            {
                Period = uint.MaxValue;
                m_Current = 0;
            }

            public override void OnDraw(Graphics g)
            {
                if (Period == 0)
                {
                    return;
                }

                if (m_Current < Period)
                {
                    base.OnDraw(g);
                }
                m_Current = (++m_Current) % (m_DoublePeriod);
            }
        }
    }
}
