using System.Drawing;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.DMI.Controls;
using Urban.Iran.DMI.Resources.Facade;

namespace Urban.Iran.DMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Miscellaneous : baseClass
    {
        private FjButtonEx[] m_BtnArr;
        public static int m_BriAlpha = 50;

        void btn_onMouseDown(FjButtonEx btnSender, Point pt)
        {
            switch (btnSender.m_BtnIndex)
            {
                case 0:
                    m_BriAlpha = ((m_BriAlpha - 50) >= 0) ? (m_BriAlpha - 50) : 0;
                    break;
                case 1:
                    m_BriAlpha = (m_BriAlpha + 50 >= 255) ? 255 : (m_BriAlpha + 50);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    append_postCmd(CmdType.ChangePage, 56, 0, 0);
                    break;
                default:
                    break;
            }
        }

        public override string GetInfo()
        {
            return "Miscellaneous";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_BtnArr = new FjButtonEx[]{
                new FjButtonEx(0, new Rectangle(335, 35, 150, 73), ImageResourceFacade.increaseBri),
                new FjButtonEx(1, new Rectangle(487, 35, 150, 73), ImageResourceFacade.decreaseBri),
                new FjButtonEx(2, new Rectangle(335, 110, 150, 73), ImageResourceFacade.increaseSound),
                new FjButtonEx(3, new Rectangle(487, 110, 150, 73), ImageResourceFacade.decreaseSound),
                new FjButtonEx(4, new Rectangle(335, 185, 150, 73), ImageResourceFacade.lockClean),
                new FjButtonEx(5, new Rectangle(488, 262, 111, 56), ImageResourceFacade.exit)
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
            foreach (var btn in m_BtnArr)
            {
                btn.OnDraw(g);
            }
        }

        public override bool mouseDown(Point point)
        {
            foreach (var btn in m_BtnArr.Where(btn => btn.IsVisible(point)))
            {
                btn.OnMouseDown(point);
                return true;
            }
            return false;
        }
    }
}