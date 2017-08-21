using System.Drawing;

namespace CommonUtil.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultRectTextBehavierStrategy : IRectTextBehavierStrategy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public DefaultRectTextBehavierStrategy(GDIRectText control)
        {
            Control = control;
        }

        /// <summary>
        /// 所属控件 
        /// </summary>
        public GDIRectText Control { get; private set; }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="point"></param>
        public virtual bool OnMouseDown(Point point)
        {
            // nothing
            return false;
        }

        /// <summary>
        /// 鼠标按下后弹起
        /// </summary>
        /// <param name="point"></param>
        public virtual bool OnMouseUp(Point point)
        {
            // nothing
            return false;
        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="g"></param>
        public virtual void OnDraw(Graphics g)
        {
            if (!Control.Visible)
            {
                return;
            }

            // 背景
            Control.DrawBk(g);

            // 文本
            Control.DrawText(g);

            // 轮廓
            Control.DrawOutline(g);
        }
    }
}