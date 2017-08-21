using System;
using System.Drawing;
using CommonUtil.Util;

namespace CommonUtil.Controls.Button
{
    /// <summary>
    /// 普通策略 , 没有click事件
    /// </summary>
    [Serializable]
    public class BtnBehavierNormalStrategy  : IBtnBehavierStrategy
    {
        [NonSerialized]
        private GDIButton m_Control;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="btn"></param>
        public BtnBehavierNormalStrategy(GDIButton btn)
        {
            Control = btn;
        }

        /// <summary>
        /// 所属控件 
        /// </summary>
        public GDIButton Control
        {
            get { return m_Control; }
            private set { m_Control = value; }
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="point"></param>
        public virtual bool OnMouseDown(Point point)
        {
            if (!Control.Visible)
            {
                return false;
            }
            if (Control.IsEnable)
            {
                if (Control.Contains(point))
                {
                    HandleUtil.OnHandle(Control.ButtonDownEvent, Control, null);
                    Control.OnButtonDown();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 鼠标按下后弹起
        /// </summary>
        /// <param name="point"></param>
        public virtual bool OnMouseUp(Point point)
        {
            if (!Control.Visible)
            {
                return false;
            }
            if (Control.IsEnable)
            {
                if (Control.Contains(point))
                {
                    HandleUtil.OnHandle(Control.ButtonUpEvent, Control, null);
                    Control.OnButtonUp();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// OnDraw
        /// </summary>
        /// <param name="g"></param>
        public virtual void OnDraw(Graphics g)
        {
            Control.DrawDefault(g);
        }
    }
}
