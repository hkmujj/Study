using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.DMI.Controls;
using Urban.Iran.DMI.Resources.Facade;

namespace Urban.Iran.DMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class SysInfo : baseClass
    {
        private Point m_TxtPoint;
        private Rectangle m_TitleRect;
        private FjButtonEx m_BtnExit;

        private void btnExit_onMouseDown(FjButtonEx btnSender, Point pt)
        {
            append_postCmd(CmdType.ChangePage, 56, 0, 0);
        }

        public override string GetInfo()
        {
            return "SysInfo";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_TxtPoint = new Point(335, 60);
            m_TitleRect = new Rectangle(335, 12, 305, 35);
            m_BtnExit = new FjButtonEx(0, new Rectangle(488, 262, 111, 56), ImageResourceFacade.exit);
            m_BtnExit.onMouseDown += btnExit_onMouseDown;

            return true;
        }

        public override void paint(Graphics g)
        {
            g.FillRectangle(GdiCommon.GreyBrush, m_TitleRect);
            g.DrawString("System Information", GdiCommon.Txt15Font, GdiCommon.WhiteBrush, m_TitleRect,
                GdiCommon.CenterFormat);
            g.DrawString("DMI Software version 1.0", GdiCommon.Txt12Font, GdiCommon.WhiteBrush, m_TxtPoint);
            m_BtnExit.OnDraw(g);
        }

        public override bool mouseDown(Point point)
        {
            if (m_BtnExit.IsVisible(point))
            {
                m_BtnExit.OnMouseDown(point);
                return true;
            }
            return false;
        }
    }
}