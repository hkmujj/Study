using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using CommonUtil.Util.Extension;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.Roller
{
    /// <summary>
    /// 控件按键的区域
    /// </summary>
    class HotBoxControllerRegion : CommonInnerControlBase
    {
        private readonly GT_HotBoxStatus m_ParentView;

        private readonly CRH1AButton m_ResetAlarmButton;

        private readonly CRH1AButton m_RelaseLockButton;

        public HotBoxControllerRegion(GT_HotBoxStatus parentView, Rectangle rectangle)
        {
            m_ParentView = parentView;
            OutLineRectangle = rectangle;

            var fLocation = new Point(m_OutLineRectangle.Left + 100, m_OutLineRectangle.Top + 13);
            const int INTERVAL = 5;
            var btnSize = new Size(120, 50);
            m_ResetAlarmButton = new CRH1AButton() { Tag = ControllerButtonName.ResetAlarm, ButtonDownEvent = OnButtonDownEvent, ButtonUpEvent = OnButtonUpEvent };
            m_ResetAlarmButton.SetButtonRect(fLocation.X, fLocation.Y, btnSize.Width, btnSize.Height);
            m_ResetAlarmButton.SetButtonColor(192, 192, 192);
            m_ResetAlarmButton.SetButtonText("复位报警器");

            m_RelaseLockButton = new CRH1AButton() { Tag = ControllerButtonName.RelaseLock, ButtonDownEvent = OnButtonDownEvent, ButtonUpEvent = OnButtonUpEvent };
            m_RelaseLockButton.SetButtonRect(fLocation.X + INTERVAL + btnSize.Width, fLocation.Y, btnSize.Width, btnSize.Height);
            m_RelaseLockButton.SetButtonColor(192, 192, 192);
            m_RelaseLockButton.SetButtonText("释放联锁");
            GlobalInfo.Instance.ButtonStatusChange += () =>
            {
                m_RelaseLockButton.IsEnable = GlobalInfo.Instance.ButtonEnable;
                m_ResetAlarmButton.IsEnable = GlobalInfo.Instance.ButtonEnable;
            };
        }

        private void OnButtonUpEvent(object sender, EventArgs e)
        {
            var btn = (CRH1AButton)sender;
            var name = (ControllerButtonName)btn.Tag;
            m_ParentView.Resource.ChangeState(name, false);
        }

        private void OnButtonDownEvent(object sender, EventArgs eventArgs)
        {
            var btn = (CRH1AButton)sender;
            var name = (ControllerButtonName)btn.Tag;
            m_ParentView.Resource.ChangeState(name, true);
        }

        public override void OnDraw(Graphics g)
        {
            m_ResetAlarmButton.OnDraw(g);
            m_RelaseLockButton.OnDraw(g);
        }

        public override bool OnMouseDown(Point point)
        {
            return m_ResetAlarmButton.OnMouseDown(point) || m_RelaseLockButton.OnMouseDown(point);
        }

        public override bool OnMouseUp(Point point)
        {
            return m_ResetAlarmButton.OnMouseUp(point) || m_RelaseLockButton.OnMouseUp(point);
        }
    }
}
