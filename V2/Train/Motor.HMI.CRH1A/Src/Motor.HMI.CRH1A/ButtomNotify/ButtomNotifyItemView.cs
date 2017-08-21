using System;
using System.Drawing;
using CommonUtil.Controls;
using Motor.HMI.CRH1A.Common;

namespace Motor.HMI.CRH1A.ButtomNotify
{
    public class ButtomNotifyItemView : CommonInnerControlBase, IDisposable
    {
        public ButtomNotifyModelWrapper ShowingTarget { set; get; }

        private  StringFormat m_ContentFormat = new StringFormat()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };

        private Font m_ContentFont = new Font("Arial", 10, FontStyle.Bold);
        private CRH1AButton m_ConfigButton;

        private Rectangle m_ContentRectangle;

        public event Action<ButtomNotifyModelWrapper> EnsureNotify;

        public override void Init()
        {
            //初始化确认按钮
            m_ConfigButton = new CRH1AButton();
            m_ConfigButton.SetButtonColor(192, 192, 192);
            m_ConfigButton.SetButtonRect(OutLineRectangle.X + 708, OutLineRectangle.Y, 85, OutLineRectangle.Height);
            m_ConfigButton.SetButtonText("确认");

            m_ContentRectangle = Rectangle.Inflate(OutLineRectangle, -10, 0);
        }

        public override void OnDraw(Graphics g)
        {
            g.FillRectangle(Brushes.Yellow, OutLineRectangle);

            g.DrawString(ShowingTarget.NotifyModel.Content, m_ContentFont, Brushes.Black, m_ContentRectangle, m_ContentFormat);

            m_ConfigButton.OnDraw(g);
        }

        public override bool OnMouseDown(Point point)
        {
            if (m_ConfigButton.Contains(point))
            {
                m_ConfigButton.OnMouseDown(point);
                
                return true;
            }
            return false;
        }

        public override bool OnMouseUp(Point point)
        {
            if (m_ConfigButton.Contains(point))
            {
                m_ConfigButton.OnMouseUp(point);
                OnEnsureNotify(ShowingTarget);
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            if (m_ContentFormat != null)
            {
                m_ContentFormat.Dispose();
                m_ContentFormat = null;
            }
            if (m_ContentFont != null)
            {
                m_ContentFont.Dispose();
                m_ContentFont = null;
            }
        }

        protected void OnEnsureNotify(ButtomNotifyModelWrapper obj)
        {
            var handler = EnsureNotify;
            if (handler != null)
            {
                handler(obj);
            }
        }
    }
}