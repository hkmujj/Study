using System;
using System.Drawing;
using CommonUtil.Controls;

namespace CRH2MMI.DoorInfo
{
    internal class PressReleaseDoor : DoorUnit
    {
        private readonly GDIRectText m_RectText;

        public PressReleaseDoor(DoorInBoolModel model)
            : base(model)
        {
            m_RectText = new GDIRectText() { NeedDarwOutline = true };
            m_RectText.SetBkColor(Color.Black);
            m_RectText.SetTextColor(new SolidBrush(Color.White));
            m_RectText.SetText("");
            m_RectText.SetTextStyle(10, FormatStyle.Center, false, "Arial");
            OutLineChanged = OnOutLineChanged; 
        }


        public PressReleaseDoor(DoorLocation doorNo, int trainNo)
            : base(doorNo, trainNo)
        {
            m_RectText = new GDIRectText() { NeedDarwOutline = true };
            m_RectText.SetBkColor(Color.Black);
            m_RectText.SetTextColor(new SolidBrush(Color.White));
            m_RectText.SetText("");
            m_RectText.SetTextStyle(10, FormatStyle.Center, false, "Arial");
            OutLineChanged = OnOutLineChanged;
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            m_RectText.OutLineRectangle= new Rectangle(OutLineRectangle.X, OutLineRectangle.Y, OutLineRectangle.Width, OutLineRectangle.Height);
        }

        public override void OnDraw(Graphics g)
        {
            if (State == DoorState.Press)
            {
                m_RectText.Text = "压";
            }
            if (State== DoorState.Relase)
            {
                m_RectText.Text = "";
            }
            m_RectText.OnDraw(g);
        }
    }
}
