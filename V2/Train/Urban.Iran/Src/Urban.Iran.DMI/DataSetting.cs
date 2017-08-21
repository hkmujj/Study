using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.DMI.Controls;
using Urban.Iran.DMI.Resources.Facade;

namespace Urban.Iran.DMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DataSetting : baseClass
    {
        private FjButtonEx[] m_BtnArr;

        private void btn_onMouseDown(FjButtonEx btnSender, Point pt)
        {
            switch (btnSender.m_BtnIndex)
            {
                case 0:
                    append_postCmd(CmdType.ChangePage, 54, 0, 0);
                    break;
                case 1:
                    append_postCmd(CmdType.ChangePage, 52, 0, 0);
                    break;
                default:
                    break;
            }
        }

        public override string GetInfo()
        {
            return "DataSetting";
        }

        public override bool init(ref int nErrorObjectIndex)
        {

            m_BtnArr = new FjButtonEx[2]
            {
                new FjButtonEx(0, new Rectangle(336, 35, 154, 76), ImageResourceFacade.driverNumber),
                new FjButtonEx(1, new Rectangle(488, 262, 111, 56), ImageResourceFacade.exit),

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