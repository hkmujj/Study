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
    public class DrNumSetting : baseClass
    {
        private Point m_TxtPoint;
        private Rectangle m_TxtRect;
        private FjButtonEx[] m_BtnArr;
        public static string m_StrNumber;

        void btn_onMouseDown(FjButtonEx btnSender, Point pt)
        {
            switch (btnSender.m_BtnIndex)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    m_StrNumber += btnSender.m_BtnIndex;
                    break;
                case 10:
                    m_StrNumber = (m_StrNumber != string.Empty) ? m_StrNumber.Remove(m_StrNumber.Length - 1) : string.Empty;
                    break;
                case 11:
                    m_StrNumber += ",";
                    break;
                case 12:
                    m_StrNumber = string.Empty;
                    append_postCmd(CmdType.ChangePage, 53, 0, 0);
                    break;
                case 13:
                    append_postCmd(CmdType.ChangePage, 55, 0, 0);
                    break;
                default:
                    break;
            }
        }

        public override string GetInfo()
        {
            return "DrNumSetting";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_StrNumber = string.Empty;
            m_TxtPoint = new Point(345, 95);
            m_TxtRect = new Rectangle(480, 90, 160, 30);
            m_BtnArr = new FjButtonEx[] {
                new FjButtonEx(0, new Rectangle(437, 365, 99, 48), ImageResourceFacade._0),
                new FjButtonEx(1, new Rectangle(335, 215, 99, 48), ImageResourceFacade._1),
                new FjButtonEx(2, new Rectangle(437, 215, 99, 48), ImageResourceFacade._2),
                new FjButtonEx(3, new Rectangle(540, 215, 99, 48), ImageResourceFacade._3),
                new FjButtonEx(4, new Rectangle(335, 265, 99, 48), ImageResourceFacade._4),
                new FjButtonEx(5, new Rectangle(437, 265, 99, 48), ImageResourceFacade._5),
                new FjButtonEx(6, new Rectangle(540, 265, 99, 48), ImageResourceFacade._6),
                new FjButtonEx(7, new Rectangle(335, 315, 99, 48), ImageResourceFacade._7),
                new FjButtonEx(8, new Rectangle(437, 315, 99, 48), ImageResourceFacade._8),
                new FjButtonEx(9, new Rectangle(540, 315, 99, 48), ImageResourceFacade._9),
                new FjButtonEx(10, new Rectangle(335, 365, 99, 48), ImageResourceFacade.arrowLeft),
                new FjButtonEx(11, new Rectangle(540, 365, 99, 48), ImageResourceFacade.dot),
                new FjButtonEx(12, new Rectangle(335, 415, 73, 48), ImageResourceFacade.exit1),
                new FjButtonEx(13, new Rectangle(566, 415, 73, 48), ImageResourceFacade.enterAll)
            };
            
            foreach (var btn in m_BtnArr)
                btn.onMouseDown += btn_onMouseDown;
            return true;
        }

        public override void paint(Graphics g)
        {
            g.FillRectangle(GdiCommon.BkgBrush, GlobleRect.BKRect);
            g.DrawString("Dirver Number", GdiCommon.Txt14Font, GdiCommon.GreyBrush, m_TxtPoint);
            g.FillRectangle(GdiCommon.OceanBlueBrush, m_TxtRect);
            foreach (var btn in m_BtnArr)
            {
                btn.OnDraw(g);
            }
            g.DrawString(m_StrNumber, GdiCommon.Txt14Font, GdiCommon.YellowBrush, m_TxtRect, GdiCommon.LeftFormat);
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