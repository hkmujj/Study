using System;
using System.Drawing;

namespace Engine.TCMS.HXD3C.Utils
{
    public class ButtonPlan
    {

        /// <summary>
        /// 左顶点
        /// </summary>
        Point m_LeftTop;

        /// <summary>
        /// 右顶点
        /// </summary>
        Point m_RightTop;

        /// <summary>
        /// 左下点
        /// </summary>
        Point m_LeftBase;

        /// <summary>
        /// 右下点
        /// </summary>
        Point m_RightBase;

        /// <summary>
        /// 按键左顶点
        /// </summary>
        Point m_ButtonTop;

        /// <summary>
        /// 底色宽
        /// </summary>
        int m_BaseWide;

        /// <summary>
        /// 底色高
        /// </summary>
        int m_BaseHigh;

        /// <summary>
        /// 按键宽
        /// </summary>
        int m_ButtonWideM;

        /// <summary>
        /// 按键高
        /// </summary>
        int m_ButtonHighM;

        /// <summary>
        /// 按键画笔
        /// </summary>
        SolidBrush m_ButtonBrush;

        /// <summary>
        /// 按键字
        /// </summary>
        String m_ButtonStr;

        /// <summary>
        /// 字体颜色
        /// </summary>
        SolidBrush m_StrBrush;

        public SolidBrush ColorChioce(string str)
        {
            switch (str)
            {
                case "Yellow":
                    return Common.YellowBrush;
                case "Blue":
                    return Common.BlueBrush;
                case "LBlue":
                    return Common.MarineBlueBrush;
                case "Green":
                    return Common.GreenBrush;
                case "Red":
                    return Common.RedBrush;
                case "LRed":
                    return Common.PinkBrush;
                case "White":
                    return Common.WhiteBrush;
                case "Black":
                    return Common.BlackBrush;
                default:
                    return Common.WhiteBrush;



            }
        }


        public void ButtonUp(Point leftTop, Point rightTop, Point leftBase, Point buttonTop, int baseWide, int baseHigh,
            int buttonWideM, int buttonHighM, string buttonBru, string buttonstr, string strbrush)
        {
            m_LeftTop = leftTop;
            m_RightTop = rightTop;
            m_LeftBase = leftBase;
            m_ButtonTop = buttonTop;
            m_BaseWide = baseWide;
            m_BaseHigh = baseHigh;
            m_ButtonWideM = buttonWideM;
            m_ButtonHighM = buttonHighM;
            m_ButtonBrush = ColorChioce(buttonBru);
            m_ButtonStr = buttonstr;
            m_StrBrush = ColorChioce(strbrush);

        }

        public void DrawButtonUp(Graphics e)
        {
            e.FillRectangle(Common.BlueBrush, new Rectangle(m_LeftTop, new Size(m_BaseWide, m_BaseHigh)));

            for (int i = 0; i < m_BaseWide / 2; i++)
            {
                for (int j = 0; j < m_BaseHigh / 2; j++)
                {
                    e.DrawString(".", Common.Txt1FontR, Common.BlackBrush,
                        new Point(m_LeftTop.X + i * 2, m_LeftTop.Y + j * 2));
                }
            }

            Point[] pPoints = new Point[3] { m_LeftTop, m_RightTop, m_LeftBase };
            e.FillPolygon(Common.WhiteBrush, pPoints);

            e.FillRectangle(Common.BlueBrush, new Rectangle(m_ButtonTop, new Size(m_ButtonWideM, m_ButtonHighM)));
            e.DrawString(m_ButtonStr, Common.Txt12FontB, m_StrBrush,
                new Rectangle(m_ButtonTop, new Size(m_ButtonWideM, m_ButtonHighM)), Common.DrawFormat);

        }


        public void ButtonDown(Point leftTop, Point rightTop, Point leftBase, Point rightBase, Point buttonTop, int baseWide, int baseHigh,
            int buttonWideM, int buttonHighM, string buttonBru, string buttonstr, string strbrush)
        {
            m_LeftTop = leftTop;
            m_RightTop = rightTop;
            m_LeftBase = leftBase;
            m_RightBase = rightBase;
            m_ButtonTop = buttonTop;
            m_BaseWide = baseWide;
            m_BaseHigh = baseHigh;
            m_ButtonWideM = buttonWideM;
            m_ButtonHighM = buttonHighM;
            m_ButtonBrush = ColorChioce(buttonBru);
            m_ButtonStr = buttonstr;
            m_StrBrush = ColorChioce(strbrush);

        }

        public void DrawButtonDown(Graphics e)
        {
            e.FillRectangle(Common.MarineBlueBrush, new Rectangle(m_LeftTop, new Size(m_BaseWide, m_BaseHigh)));

            Point[] bPoints = new Point[3] { m_LeftTop, m_RightTop, m_LeftBase };
            Point[] wPoints = new Point[3] { m_RightTop, m_LeftBase, m_RightBase };

            e.FillPolygon(Common.BlackBrush, bPoints);
            e.FillPolygon(Common.WhiteBrush, wPoints);

            e.FillRectangle(Common.MarineBlueBrush, new Rectangle(m_ButtonTop, new Size(m_ButtonWideM, m_ButtonHighM)));

            e.DrawString(m_ButtonStr, Common.Txt12FontB, m_StrBrush,
                new Rectangle(m_ButtonTop, new Size(m_ButtonWideM, m_ButtonHighM)), Common.DrawFormat);

        }

    }
}