using System.Drawing;

namespace CommonUtil.Controls.Decorator
{
    /// <summary>
    /// 
    /// </summary>
    public class CommonInnerControlGlintDecorator : CommonInnerControlDecorator
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

        /// <summary>
        /// 当闪烁到灭时，是否需要 OnPaint  是否需要 调用 Refresh
        /// </summary>
        public bool NeedRefresh { set; get; }

        private uint m_DoublePeriod;

        private uint m_Current;
        private uint m_Period;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerControl"></param>
        public CommonInnerControlGlintDecorator(CommonInnerControlBase innerControl)
            : base(innerControl)
        {
            Period = uint.MaxValue;
            m_Current = 0;
            NeedRefresh = false;
        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="g"></param>
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

        /// <summary>
        /// 刷新并绘图, 会调用 Refresh
        /// </summary>
        /// <param name="g"></param>
        public override void OnPaint(Graphics g)
        {
            if (Period == 0)
            {
                return;
            }

            if (m_Current < Period)
            {
                base.OnPaint(g);
            }
            else if (NeedRefresh)
            {
                m_CommonInnerControl.Refresh();
            }

            m_Current = (++m_Current) % (m_DoublePeriod);
        }
    }
}
