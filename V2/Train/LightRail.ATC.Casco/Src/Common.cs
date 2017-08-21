using System;
using System.Drawing;

namespace LightRail.ATC.Casco
{
    /// <summary>
    /// 字体画笔线条格式
    /// </summary>
    public class FormatStyle
    {
        public static int menu = 0;
        public const int Center = 2;
        public const String strFont = "Arial";

        //线条
        //白色
        public static Pen WhitePen = new Pen(Color.FromArgb(255, 255, 255), 1);
        public static Pen WhitePen2 = new Pen(Color.FromArgb(255, 255, 255), 2);
        public static Pen WhitePen3 = new Pen(Color.FromArgb(255, 255, 255), 3);
        public static Pen WhitePen4 = new Pen(Color.FromArgb(255, 255, 255), 4);
        //黑色
        public static Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0), 1);
        public static Pen BlackPen2 = new Pen(Color.FromArgb(0, 0, 0), 2);
        //浅灰、中灰、深灰
        public static Pen LightGreyPen = new Pen(Color.FromArgb(195, 195, 195));
        public static Pen MediumGreyPen = new Pen(Color.FromArgb(150, 150, 150));
        public static Pen DarkGreyPen = new Pen(Color.FromArgb(85, 85, 85));
        //蓝色、深蓝
        public static Pen BluePen = new Pen(Color.FromArgb(3, 17, 34));
        public static Pen VeryDarkBluePen = new Pen(Color.FromArgb(4, 12, 25));
        //淡绿、深绿
        public static Pen LightGreenPen = new Pen(Color.FromArgb(45, 144, 51));
        public static Pen LightGreenPen_2 = new Pen(Color.FromArgb(45, 144, 51), 2);
        public static Pen DarkGreenPen = new Pen(Color.FromArgb(12, 58, 12));
        //黄色、深黄色、橙色
        public static Pen YellowPen = new Pen(Color.FromArgb(223, 223, 0), 2);
        public static Pen DarkYellowPen = new Pen(Color.FromArgb(117, 105, 0));
        public static Pen OrangePen = new Pen(Color.FromArgb(234, 145, 0));
        //红色
        public static Pen RedPen = new Pen(Color.FromArgb(191, 0, 2));
        public static Pen RedPen_2 = new Pen(Color.FromArgb(191, 0, 2), 2);
        //灰蓝色、蓝灰色、淡蓝灰色
        public static Pen PurplePen = new Pen(Color.FromArgb(255, 0, 255), 1);
        public static Pen Gary_BluePen = new Pen(Color.FromArgb(81, 91, 109));
        public static Pen Blue_GrayPen = new Pen(Color.FromArgb(37, 69, 93));
        public static Pen LightBlue_GrayPen = new Pen(Color.FromArgb(128, 139, 158));

        //画笔
        //白色
        public static SolidBrush WhiteSolidBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        //黑色
        public static SolidBrush BlackSolidBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        //浅灰、中灰、深灰
        public static SolidBrush LightGreySolidBrush = new SolidBrush(Color.FromArgb(195, 195, 195));
        public static SolidBrush MediumGreySolidBrush = new SolidBrush(Color.FromArgb(150, 150, 150));
        public static SolidBrush DarkGreySolidBrush = new SolidBrush(Color.FromArgb(85, 85, 85));
        //蓝色、深蓝
        public static SolidBrush BlueSolidBrush = new SolidBrush(Color.FromArgb(3, 17, 34));
        public static SolidBrush VeryDarkBlueSolidBrush = new SolidBrush(Color.FromArgb(4, 12, 25));
        //淡绿、深绿
        public static SolidBrush LightGreenSolidBrush = new SolidBrush(Color.FromArgb(45, 144, 51));
        public static SolidBrush DarkGreenSolidBrush = new SolidBrush(Color.FromArgb(12, 58, 12));
        //青色
        public static SolidBrush BluenessSolidBrush = new SolidBrush(Color.FromArgb(0, 194, 194));
        //黄色、深黄色、橙色
        public static SolidBrush YellowSolidBrush = new SolidBrush(Color.FromArgb(223, 223, 0));
        public static SolidBrush DarkYellowSolidBrush = new SolidBrush(Color.FromArgb(117, 105, 0));
        public static SolidBrush OrangeSolidBrush = new SolidBrush(Color.FromArgb(234, 145, 0));
        //红色
        public static SolidBrush RedSolidBrush = new SolidBrush(Color.FromArgb(191, 0, 2));
        //灰蓝色、蓝灰色、淡蓝灰色
        public static SolidBrush Gary_BlueSolidBrush = new SolidBrush(Color.FromArgb(81, 91, 109));
        public static SolidBrush Blue_GraySolidBrush = new SolidBrush(Color.FromArgb(37, 69, 93));
        public static SolidBrush LightBlue_GraySolidBrush = new SolidBrush(Color.FromArgb(128, 139, 158));

        #region :::::::::::::::::::::::::::::::: 字体 ::::::::::::::::::::::::::::::::::::::::::
        public static Font Font10 = new Font(strFont, 10f);
        public static Font Font12 = new Font(strFont, 12f);
        public static Font Font14 = new Font(strFont, 14f);
        public static Font Font16 = new Font(strFont, 16f);
        public static Font Font18 = new Font(strFont, 18f);
        public static Font Font20 = new Font(strFont, 20f);
        public static Font Font22 = new Font(strFont, 22f);
        public static Font Font24 = new Font(strFont, 24f);
        public static Font Font26 = new Font(strFont, 26f);
        public static Font Font32 = new Font(strFont, 32f);
        public static Font Font34 = new Font(strFont, 34f);
        public static Font Font38 = new Font(strFont, 38f);
        public static Font Font64 = new Font(strFont, 64f);

        public static Font Font8 = new Font(strFont, 8f, FontStyle.Bold);
        public static Font Font10B = new Font(strFont, 10f, FontStyle.Bold);
        public static Font Font12B = new Font(strFont, 12f, FontStyle.Bold);
        public static Font Font14B = new Font(strFont, 14f, FontStyle.Bold);
        public static Font Font16B = new Font(strFont, 16f, FontStyle.Bold);
        public static Font Font18B = new Font(strFont, 18f, FontStyle.Bold);
        public static Font Font20B = new Font(strFont, 20f, FontStyle.Bold);
        public static Font Font22B = new Font(strFont, 22f, FontStyle.Bold);
        public static Font Font24B = new Font(strFont, 24f, FontStyle.Bold);
        public static Font Font26B = new Font(strFont, 26f, FontStyle.Bold);
        public static Font Font32B = new Font(strFont, 32f, FontStyle.Bold);
        public static Font Font34B = new Font(strFont, 34f, FontStyle.Bold);
        public static Font Font38B = new Font(strFont, 38f, FontStyle.Bold);
        public static Font Font64B = new Font(strFont, 64f, FontStyle.Bold);
        #endregion
    }

    /// <summary>
    /// 变化矩形条
    /// </summary>
    public class NeedChangeLength
    {
        /// <summary>
        /// 当前要画的高度值
        /// </summary>
        private float m_ViewValue;

        /// <summary>
        /// 需要增加的高度量
        /// </summary>
        private float m_TmpNeedChangeLength;

        /// <summary>
        /// 递增量
        /// </summary>
        private readonly float tmpStepLength = 5;

        /// <summary>
        /// 黄色画笔
        /// </summary>
        public static SolidBrush yellowBrush = new SolidBrush(Color.Yellow);

        /// <summary>
        /// 绘图起点
        /// </summary>
        PointF drawPoint;

        /// <summary>
        /// 进度条宽度
        /// </summary>
        private readonly int rectWidth;

        /// <summary>
        /// 对应比例
        /// </summary>
        private readonly float rectScale;

        public NeedChangeLength(PointF point, int width, float dizeng, float scale)
        {
            drawPoint = point;
            rectWidth = width;
            tmpStepLength = dizeng;
            rectScale = scale;
        }

        /// <summary>
        /// 绘制纵条
        /// </summary>
        /// <param name="e"></param>
        /// <param name="currentValue"></param>
        /// <param name="drawType"></param>
        public void DrawRectangle(Graphics e, ref float currentValue, int drawType)
        {
            if (m_ViewValue > currentValue)
            {
                if (m_ViewValue - tmpStepLength >= currentValue)
                {
                    m_TmpNeedChangeLength = -tmpStepLength;
                }
                else
                {
                    m_TmpNeedChangeLength = currentValue - m_ViewValue;
                }
            }
            else if (m_ViewValue < currentValue)
            {
                if (m_ViewValue + tmpStepLength <= currentValue)
                {
                    m_TmpNeedChangeLength = tmpStepLength;
                }
                else
                {
                    m_TmpNeedChangeLength = currentValue - m_ViewValue;
                }
            }
            else
            {
                m_TmpNeedChangeLength = 0;
            }
            m_ViewValue += m_TmpNeedChangeLength;

            switch (drawType)
            {
                case 1://横左
                    e.FillRectangle(yellowBrush, new RectangleF(new PointF(drawPoint.X - m_ViewValue * rectScale, drawPoint.Y), new SizeF(m_ViewValue * rectScale, rectWidth)));
                    break;
                case 2://横右
                    e.FillRectangle(yellowBrush, new RectangleF(drawPoint, new SizeF(m_ViewValue * rectScale, rectWidth)));
                    break;
                case 3://纵上
                    e.FillRectangle(yellowBrush, new RectangleF(new PointF(drawPoint.X, drawPoint.Y - m_ViewValue * rectScale), new SizeF(rectWidth, m_ViewValue * rectScale)));
                    break;
                case 4://纵下
                    e.FillRectangle(yellowBrush, new RectangleF(drawPoint, new SizeF(rectWidth, m_ViewValue * rectScale)));
                    break;
            }

        }
    }

    /// <summary>
    /// 闪烁
    /// </summary>
    public class FlashTimer
    {
        /// <summary>
        /// 闪烁用递增量
        /// </summary>
        int m_FlashCount;

        /// <summary>
        /// 闪烁间隔时间
        /// </summary>
        readonly int m_FlashTime;

        public FlashTimer(int interval)
        {
            m_FlashTime = interval;
        }

        /// <summary>
        /// 闪烁判断
        /// </summary>
        /// <returns></returns>
        public bool IsNeedFlash()
        {
            if (m_FlashCount < m_FlashTime * 5)
            {
                ++m_FlashCount;
                return true;
            }
            if (m_FlashCount >= m_FlashTime * 5 && m_FlashCount < m_FlashTime * 10)
            {
                ++m_FlashCount;
                return false;
            }
            m_FlashCount = 0;
            return false;
        }
    }

}
