using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.DMI.Controls;
using Urban.Iran.DMI.Resources.Facade;

namespace Urban.Iran.DMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class SpecPage : baseClass
    {
        private Point m_TxtPoint;
        private FjButtonEx[] m_BtnArr;

        private void btn_onMouseDown(FjButtonEx btnSender, Point pt)
        {
            switch (btnSender.m_BtnIndex)
            {
                case 0:
                    break;
                case 1:
                    append_postCmd(CmdType.ChangePage, 57, 0, 0);
                    break;
                case 2:
                    append_postCmd(CmdType.ChangePage, 58, 0, 0);
                    break;
                case 3:
                    append_postCmd(CmdType.ChangePage, 52, 0, 0);
                    break;
            }
        }

        public override string GetInfo()
        {
            return "SpecPage";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_TxtPoint = new Point(335, 15);
            m_BtnArr = new FjButtonEx[]
            {
                new FjButtonEx(0, new Rectangle(335, 35, 150, 73), ImageResourceFacade.test),
                new FjButtonEx(1, new Rectangle(335, 110, 150, 73), ImageResourceFacade.miscellaneous),
                new FjButtonEx(2, new Rectangle(335, 185, 150, 73), ImageResourceFacade.sysInfo),
                new FjButtonEx(3, new Rectangle(488, 262, 111, 56), ImageResourceFacade.exit),
            };
            foreach (var btn in m_BtnArr)
            {
                btn.onMouseDown += btn_onMouseDown;
            }
            return true;
        }

        public override void paint(Graphics g)
        {
            g.FillRectangle(GdiCommon.BkgBrush, GlobleRect.BKRect);
            g.DrawString("Spec", GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, m_TxtPoint);
            foreach (var btn in m_BtnArr)
            {
                btn.OnDraw(g);
            }
        }

        public override bool mouseDown(Point point)
        {
            foreach (var btn in m_BtnArr)
            {
                if (btn.IsVisible(point))
                {
                    btn.OnMouseDown(point);
                    return true;
                }
            }
            return false;
        }
    }
}