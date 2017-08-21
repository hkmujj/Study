using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.DMI.Controls;
using Urban.Iran.DMI.Resources.Facade;

namespace Urban.Iran.DMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DrNumSubmit : baseClass
    {
        private Rectangle m_SubmitRect;
        private FjButtonEx[] m_BtnArr;
        private Bitmap m_BmpSubmit;
        private Point[] m_PtArr;

        private void btn_onMouseDown(FjButtonEx btnSender, Point pt)
        {
            if (btnSender.m_BtnIndex == 0)
            {
                var temp = 0.0f;
                float.TryParse(DrNumSetting.m_StrNumber, out temp);
                append_postCmd(CmdType.SetFloatValue, 20, 0, temp);
                append_postCmd(CmdType.ChangePage, 53, 0, 0);
                DrNumSetting.m_StrNumber = string.Empty;
            }
            else if (btnSender.m_BtnIndex == 1)
            {
                append_postCmd(CmdType.ChangePage, 53, 0, 0);
            }
        }


        public override string GetInfo()
        {
            return "DrNumSubmit";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_BmpSubmit = ImageResourceFacade.submit;
            m_SubmitRect = new Rectangle(330, 160, 310, 50);
            m_BtnArr = new FjButtonEx[]
            {
                new FjButtonEx(0, new Rectangle(335, 265, 101, 51), ImageResourceFacade.yes),
                new FjButtonEx(1, new Rectangle(438, 265, 101, 51), ImageResourceFacade.no),
            };
            foreach (var btn in m_BtnArr)
            {
                btn.onMouseDown += btn_onMouseDown;
            }

            m_PtArr = new Point[]
            {
                new Point(335, 40),
                new Point(335, 70),
                new Point(510, 183)
            };
            return true;
        }

        public override void paint(Graphics g)
        {

            g.FillRectangle(GdiCommon.BkgBrush, GlobleRect.BKRect);
            g.DrawImage(m_BmpSubmit, m_SubmitRect);
            foreach (var btn in m_BtnArr)
            {
                btn.OnDraw(g);
            }

            g.DrawString(
                "Driver no.   " +
                ((DrNumSetting.m_StrNumber != string.Empty) ? DrNumSetting.m_StrNumber.PadLeft(6, '0') : "000000"),
                GdiCommon.Txt12Font, GdiCommon.GreyBrush, m_PtArr[0]);
            g.DrawString("Train   no.   " + FloatList[30].ToString("000000"), GdiCommon.Txt12Font, GdiCommon.GreyBrush,
                m_PtArr[1]);
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