using System;
using System.Drawing;
using MMITool.Common.Util;

namespace MMITool.Common.Controls.Button
{
    /// <summary>
    /// 按键的 Click 策略
    /// </summary>
    [Obsolete("未完成")]
    public class BtnBehavierClickStrategy : IBtnBehavierStrategy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public BtnBehavierClickStrategy(GDIButton control)
        {
            Control = control;
        }

        /// <summary>
        /// 所属控件 
        /// </summary>
        public GDIButton Control { get; private set; }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="point"></param>
        public bool OnMouseDown(Point point)
        {
            //BtnManager.Instance.LastMouseDownBtn = null;
            if (!Control.Visible)
            {
                return false;
            }
            if (Control.IsEnable)
            {
                if (Control.Contains(point))
                {
                    Control.OnButtonDown();
                    //HandleUtil.OnHandle(Control.ButtonDownEvent, this.Control, null);
                    BtnManager.Instance.LastMouseDownBtn = Control;
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// 鼠标按下后弹起
        /// </summary>
        /// <param name="point"></param>
        public bool OnMouseUp(Point point)
        {
            bool bState = false;
            if (Control.Visible)
            {
                if (Control.IsEnable)
                {
                    if (Control.Contains(point))
                    {
                        Control.OnButtonUp();
                        if (Control.Contains(MouseMgr.Instance.MouseDownPoint))
                        {
                            HandleUtil.OnHandle(Control.ButtonDownEvent, Control, null);
                            HandleUtil.OnHandle(Control.ButtonUpEvent, Control, null);
                            HandleUtil.OnHandle(Control.ButtonClickEvent, Control, null);
                            BtnManager.Instance.LastMouseDownBtn = null;
                        }
                        else if (BtnManager.Instance.LastMouseDownBtn != null)
                        {
                            BtnManager.Instance.LastMouseDownBtn.OnButtonUp();
                            BtnManager.Instance.LastMouseDownBtn = null;
                        }
                        bState = true;
                    }
                }
            }
            return bState;

        }

        /// <summary>
        /// OnDraw
        /// </summary>
        /// <param name="g"></param>
        public void OnDraw(Graphics g)
        {
            Control.DrawDefault(g);
        }
    }


}
