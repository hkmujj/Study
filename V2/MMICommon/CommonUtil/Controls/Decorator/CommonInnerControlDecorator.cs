using System.Drawing;

namespace CommonUtil.Controls.Decorator
{
    /// <summary>
    /// 
    /// </summary>
    public class CommonInnerControlDecorator : CommonInnerControlBase
    {
        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// 
        /// </summary>
        protected CommonInnerControlBase m_CommonInnerControl;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerControl"></param>
        public CommonInnerControlDecorator(CommonInnerControlBase innerControl)
        {
            m_CommonInnerControl = innerControl;
        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            if (m_CommonInnerControl != null)
            {
                m_CommonInnerControl.OnDraw(g);
            }
        }

        /// <summary>
        /// 刷新并绘图, 会调用 Refresh
        /// </summary>
        /// <param name="g"></param>
        public override void OnPaint(Graphics g)
        {
            if (m_CommonInnerControl != null)
            {
                m_CommonInnerControl.OnPaint(g);
            }
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="point"></param>
        public override bool OnMouseDown(Point point)
        {
            if (m_CommonInnerControl != null)
            {
                return m_CommonInnerControl.OnMouseDown(point);
            }
            return false;
        }

        /// <summary>
        /// 鼠标按下后弹起
        /// </summary>
        /// <param name="point"></param>
        public override bool OnMouseUp(Point point)
        {
            if (m_CommonInnerControl != null)
            {
                return m_CommonInnerControl.OnMouseUp(point);
            }
            return false;
        }
    }
}
