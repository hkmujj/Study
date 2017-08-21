using System;
using System.Drawing;
using CommonUtil.Controls.Button;

namespace Engine.TCMS.HXD3C.Utils
{
    public class HXD3ButtonProxy : GDIButton
    {
        public HXD3Button m_ProxyButton;

        private readonly int m_Xoffset;
        private readonly int m_Yoffset;

        public HXD3ButtonProxy(int xoffset = 10, int yoffset = 10)
        {
            m_Xoffset = xoffset;
            m_Yoffset = yoffset;
            ContentTextControl.TextBrush = Common.WhiteBrush;
            ContentTextControl.DrawFont = Common.Txt12FontB;
            ContentTextControl.BackColorVisible = false;
            ContentTextControl.TextFormat = Common.CenterStringFormat;
            OutLineChanged += OnOutLineChanged;
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            UpdateProxyButton();
        }

        private void UpdateProxyButton()
        {
            m_ProxyButton = new HXD3Button(OutLineRectangle,
                new Rectangle(OutLineRectangle.X + m_Xoffset / 2, OutLineRectangle.Y + m_Yoffset / 2, OutLineRectangle.Width - m_Xoffset,
                    OutLineRectangle.Height - m_Yoffset));
        }

        public override void OnDraw(Graphics g)
        {
            if (Visible)
            {
                m_ProxyButton.DrawRectButoonFillAndNoState(g);
                DrawText(g, false);
            }
        }
    }
}