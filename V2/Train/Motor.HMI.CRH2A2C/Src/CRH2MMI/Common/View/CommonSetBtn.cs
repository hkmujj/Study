using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using CommonUtil.Util;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Models;
using CommonUtil.Controls;


namespace CRH2MMI.Common.View
{
    /// <summary>
    /// 右下角的按键设定
    /// </summary>
    public class CommonSetBtn : CommonInnerControlBase
    {
        /// <summary>
        /// 设置
        /// </summary>
        private readonly CRH2Button m_SetBtn;

        /// <summary>
        /// 设置按键被点击的事件
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public EventHandler SetButtonDown;

        [SuppressMessage("ReSharper", "InconsistentNaming")] 
        public EventHandler SetButtonUp;

        public CRH2Button SetBtnEntity
        {
            get { return m_SetBtn; }
        }

        private readonly ButtonStateRecorder m_ButtonStateRecorder;

        /// <summary>
        /// 是否自动弹起
        /// </summary>
        public bool AutoUp { set; get; }

        /// <summary>
        /// 是否可被按下
        /// </summary>
        public bool CanDown { set; get; }

        public CommonSetBtn()
        {
            AutoUp = true;
            CanDown = false;

            m_ButtonStateRecorder = new ButtonStateRecorder();
            //设定
            m_SetBtn = new CRH2Button
            {
                OutLineRectangle = new Rectangle(630, 495, 150, 45),
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                ButtonDownEvent = (sender, args) => HandleUtil.OnHandle(SetButtonDown, this, null),
                ButtonUpEvent = (sender, args) => HandleUtil.OnHandle(SetButtonUp, this, null),
                TextColor = Color.White,
                Text = "设   定",
            };
        }

        public override void OnDraw(Graphics g)
        {
            m_SetBtn.OnDraw(g);
        }

        public override bool OnMouseDown(Point point)
        {
            m_ButtonStateRecorder.State = MouseState.MouseDown;
            m_ButtonStateRecorder.Location = point;
            var state = m_SetBtn.OnMouseDown(point);
            if (state && CanDown)
            {
                m_SetBtn.CurrentMouseState = MouseState.MouseDown;
            }
            return state;
        }

        public override bool OnMouseUp(Point point)
        {
            var location = point;
            if (AutoUp && m_ButtonStateRecorder.State == MouseState.MouseDown)
            {
                m_ButtonStateRecorder.State = MouseState.MouseUp;
                location = m_ButtonStateRecorder.Location;
            }

            var state = m_SetBtn.OnMouseUp(location);
            if (state)
            {
                m_SetBtn.CurrentMouseState = MouseState.MouseUp;
            }
            return state;
        }
    }
}
