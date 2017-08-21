using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Urban.NanJing.AirportLine.DialScreen
{
    /// <summary>
    /// 字体画笔线条格式
    /// </summary>
    public class FormatStyle
    {
        public static int m_Menu = 0;
        public const int Center = 2;
        public const String StrFont = "Arial";

        //线条
        //白色
        public static Pen m_WhitePen = new Pen(Color.FromArgb(255, 255, 255), 1);
        public static Pen m_WhitePen2 = new Pen(Color.FromArgb(255, 255, 255), 2);
        public static Pen m_WhitePen3 = new Pen(Color.FromArgb(255, 255, 255), 3);
        public static Pen m_WhitePen4 = new Pen(Color.FromArgb(255, 255, 255), 4);
        //黑色
        public static Pen m_BlackPen = new Pen(Color.FromArgb(0, 0, 0), 1);
        public static Pen m_BlackPen2 = new Pen(Color.FromArgb(0, 0, 0), 2);
        //浅灰、中灰、深灰
        public static Pen m_LightGreyPen = new Pen(Color.FromArgb(195, 195, 195));
        public static Pen m_MediumGreyPen = new Pen(Color.FromArgb(150, 150, 150));
        public static Pen m_DarkGreyPen = new Pen(Color.FromArgb(85, 85, 85));
        //蓝色、深蓝
        public static Pen m_BluePen = new Pen(Color.FromArgb(3, 17, 34));
        public static Pen m_VeryDarkBluePen = new Pen(Color.FromArgb(4, 12, 25));
        //淡绿、深绿
        public static Pen m_LightGreenPen = new Pen(Color.FromArgb(45, 144, 51));
        public static Pen m_LightGreenPen2 = new Pen(Color.FromArgb(45, 144, 51), 2);
        public static Pen m_DarkGreenPen = new Pen(Color.FromArgb(12, 58, 12));
        //黄色、深黄色、橙色
        public static Pen m_YellowPen = new Pen(Color.FromArgb(223, 223, 0), 2);
        public static Pen m_DarkYellowPen = new Pen(Color.FromArgb(117, 105, 0));
        public static Pen m_OrangePen = new Pen(Color.FromArgb(234, 145, 0));
        //红色
        public static Pen m_RedPen = new Pen(Color.FromArgb(191, 0, 2));
        public static Pen m_RedPen2 = new Pen(Color.FromArgb(191, 0, 2), 2);
        //灰蓝色、蓝灰色、淡蓝灰色
        public static Pen m_PurplePen = new Pen(Color.FromArgb(255, 0, 255), 1);
        public static Pen m_GaryBluePen = new Pen(Color.FromArgb(81, 91, 109));
        public static Pen m_BlueGrayPen = new Pen(Color.FromArgb(37, 69, 93));
        public static Pen m_LightBlueGrayPen = new Pen(Color.FromArgb(128, 139, 158));

        //画笔
        //白色
        public static SolidBrush m_WhiteSolidBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        //黑色
        public static SolidBrush m_BlackSolidBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        //浅灰、中灰、深灰
        public static SolidBrush m_LightGreySolidBrush = new SolidBrush(Color.FromArgb(65, 65, 65));
        public static SolidBrush m_MediumGreySolidBrush = new SolidBrush(Color.FromArgb(150, 150, 150));
        public static SolidBrush m_DarkGreySolidBrush = new SolidBrush(Color.FromArgb(85, 85, 85));
        //蓝色、深蓝
        public static SolidBrush m_BlueSolidBrush = new SolidBrush(Color.FromArgb(3, 17, 34));
        public static SolidBrush m_VeryDarkBlueSolidBrush = new SolidBrush(Color.FromArgb(4, 12, 25));
        //淡绿、深绿
        public static SolidBrush m_LightGreenSolidBrush = new SolidBrush(Color.FromArgb(45, 144, 51));
        public static SolidBrush m_DarkGreenSolidBrush = new SolidBrush(Color.FromArgb(12, 58, 12));
        //青色
        public static SolidBrush m_BluenessSolidBrush = new SolidBrush(Color.FromArgb(0, 194, 194));
        //黄色、深黄色、橙色
        public static SolidBrush m_YellowSolidBrush = new SolidBrush(Color.FromArgb(223, 223, 0));
        public static SolidBrush m_DarkYellowSolidBrush = new SolidBrush(Color.FromArgb(117, 105, 0));
        public static SolidBrush m_OrangeSolidBrush = new SolidBrush(Color.FromArgb(234, 145, 0));
        //红色
        public static SolidBrush m_RedSolidBrush = new SolidBrush(Color.FromArgb(191, 0, 2));
        //灰蓝色、蓝灰色、淡蓝灰色
        public static SolidBrush m_GaryBlueSolidBrush = new SolidBrush(Color.FromArgb(81, 91, 109));
        public static SolidBrush m_BlueGraySolidBrush = new SolidBrush(Color.FromArgb(37, 69, 93));
        public static SolidBrush m_LightBlueGraySolidBrush = new SolidBrush(Color.FromArgb(128, 139, 158));

        #region :::::::::::::::::::::::::::::::: 字体 ::::::::::::::::::::::::::::::::::::::::::
        public static Font m_Font10 = new Font(StrFont, 10f);
        public static Font m_Font12 = new Font(StrFont, 12f);
        public static Font m_Font14 = new Font(StrFont, 14f);
        public static Font m_Font16 = new Font(StrFont, 16f);
        public static Font m_Font18 = new Font(StrFont, 18f);
        public static Font m_Font20 = new Font(StrFont, 20f);
        public static Font m_Font22 = new Font(StrFont, 22f);
        public static Font m_Font24 = new Font(StrFont, 24f);
        public static Font m_Font26 = new Font(StrFont, 26f);
        public static Font m_Font32 = new Font(StrFont, 32f);
        public static Font m_Font34 = new Font(StrFont, 34f);
        public static Font m_Font38 = new Font(StrFont, 38f);
        public static Font m_Font64 = new Font(StrFont, 64f);

        public static Font m_Font8 = new Font(StrFont, 8f, FontStyle.Bold);
        public static Font m_Font10B = new Font(StrFont, 10f, FontStyle.Bold);
        public static Font m_Font12B = new Font(StrFont, 12f, FontStyle.Bold);
        public static Font m_Font14B = new Font(StrFont, 14f, FontStyle.Bold);
        public static Font m_Font16B = new Font(StrFont, 16f, FontStyle.Bold);
        public static Font m_Font18B = new Font(StrFont, 18f, FontStyle.Bold);
        public static Font m_Font20B = new Font(StrFont, 20f, FontStyle.Bold);
        public static Font m_Font22B = new Font(StrFont, 22f, FontStyle.Bold);
        public static Font m_Font24B = new Font(StrFont, 24f, FontStyle.Bold);
        public static Font m_Font26B = new Font(StrFont, 26f, FontStyle.Bold);
        public static Font m_Font32B = new Font(StrFont, 32f, FontStyle.Bold);
        public static Font m_Font34B = new Font(StrFont, 34f, FontStyle.Bold);
        public static Font m_Font38B = new Font(StrFont, 38f, FontStyle.Bold);
        public static Font m_Font64B = new Font(StrFont, 64f, FontStyle.Bold);
        #endregion
    }

    /// <summary>
    /// 表盘
    /// </summary>
    public class Dial
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="中心点"></param>
        /// <param name="刻度"></param>
        /// <param name="所有线段数"></param>
        /// <param name="每点代表的角度"></param>
        /// <param name="初始角度"></param>
        /// <param name="半径"></param>
        /// <param name="长刻度"></param>
        /// <param name="短刻度"></param>
        /// <param name="长短刻度比"></param>
        /// <param name="刻度宽短"></param>
        /// <param name="刻度宽长"></param>
        /// <param name="刻度字体颜色"></param>
        public Dial(PointF 中心点, string[] 刻度, int 所有线段数,
            double 每点代表的角度, double 初始角度, int 半径,
            int 长刻度, int 短刻度, int 长短刻度比,
            Pen 刻度宽短, Pen 刻度宽长, SolidBrush 刻度字体颜色)
        {
            m_DrawFormat.Alignment = (StringAlignment)1;
            m_DrawFormat.LineAlignment = (StringAlignment)1;

            m_Point = 中心点;

            m_ScaleStr = 刻度;
            m_RectKedu = new PointF[刻度.Length];

            m_PointKedu1 = new PointF[所有线段数];
            m_PointKedu2 = new PointF[所有线段数];
            m_Numb = 所有线段数;

            m_MinRadian = 每点代表的角度;

            m_InitRadian = 初始角度;

            m_Bili = 长短刻度比;

            m_PenKeduShort = 刻度宽短;
            m_PenKeduLong = 刻度宽长;

            m_Textbrush = 刻度字体颜色;

            DialInit(半径, 长刻度, 短刻度);

        }

        /// <summary>
        /// 刻度算法
        /// </summary>
        private void DialInit(int 半径, int 长刻度, int 短刻度)
        {
            double angle;
            float sinAngle;
            float cosAngle;

            int j = 0;
            for (int i = 0; i < m_Numb; i++)
            {
                angle = (i * m_MinRadian + m_InitRadian) * Math.PI / 180.0;
                sinAngle = (float)Math.Sin(angle);
                cosAngle = (float)Math.Cos(angle);
                if (i % m_Bili == 0)
                {
                    m_PointKedu1[i].X = m_Point.X + 半径 * Scale * cosAngle;
                    m_PointKedu1[i].Y = m_Point.Y + 半径 * Scale * sinAngle;

                    m_PointKedu2[i].X = m_Point.X + (半径 - 长刻度) * Scale * cosAngle;
                    m_PointKedu2[i].Y = m_Point.Y + (半径 - 长刻度) * Scale * sinAngle;

                    m_RectKedu[j].X = m_Point.X + (半径 - 长刻度 - 30) * Scale * cosAngle;
                    m_RectKedu[j].Y = m_Point.Y + (半径 - 长刻度 - 30) * Scale * sinAngle;
                    j++;
                }
                else
                {
                    m_PointKedu1[i].X = m_Point.X + 半径 * Scale * cosAngle;
                    m_PointKedu1[i].Y = m_Point.Y + 半径 * Scale * sinAngle;

                    m_PointKedu2[i].X = m_Point.X + (半径 - 短刻度) * Scale * cosAngle;
                    m_PointKedu2[i].Y = m_Point.Y + (半径 - 短刻度) * Scale * sinAngle;
                }
            }
        }

        /// <summary>
        /// 表盘绘制
        /// </summary>
        /// <param name="e"></param>
        public void OnDraw(Graphics e, Font font)
        {
            e.SmoothingMode = SmoothingMode.AntiAlias;
            for (int i = 0; i < m_Numb; i++)
            {
                if (i % m_Bili == 0)
                {
                    e.DrawLine(m_PenKeduLong, m_PointKedu1[i], m_PointKedu2[i]);
                }
                else
                {

                    e.DrawLine(m_PenKeduShort, m_PointKedu1[i], m_PointKedu2[i]);
                }
            }
            for (int i = 0; i < m_ScaleStr.Length; i++)
            {
                e.DrawString(m_ScaleStr[i], font, m_Textbrush, m_RectKedu[i], m_DrawFormat);
            }

        }

        #region ::::::::::::::::::::::: init :::::::::::::::::::::::::::
        /// <summary>
        /// 放大缩小系数
        /// </summary>
        internal const float Scale = 1.0f;

        /// <summary>
        /// 刻度点1
        /// </summary>
        public PointF[] m_PointKedu1;

        /// <summary>
        /// 刻度点2
        /// </summary>
        public PointF[] m_PointKedu2;

        /// <summary>
        /// 1点速度代表的弧度
        /// </summary>
        internal double m_MinRadian;

        /// <summary>
        /// 初始弧度
        /// </summary>
        internal double m_InitRadian;

        /// <summary>
        /// 表盘线段总数
        /// </summary>
        int m_Numb = 0;

        /// <summary>
        /// 线段代表的数量
        /// </summary>
        private string[] m_ScaleStr;

        /// <summary>
        /// 表盘上的
        /// </summary>
        public string m_Numbstr;

        /// <summary>
        /// 表盘中心点
        /// </summary>
        public PointF m_Point = new PointF();

        /// <summary>
        /// 刻度位置
        /// </summary>
        public PointF[] m_RectKedu;

        /// <summary>
        /// 刻度宽短
        /// </summary>
        internal Pen m_PenKeduShort;

        /// <summary>
        /// 刻度宽长
        /// </summary>
        internal Pen m_PenKeduLong;

        /// <summary>
        /// 字体颜色
        /// </summary>
        public SolidBrush m_Textbrush;

        /// <summary>
        /// 刻度长短比例，短：长
        /// </summary>
        private int m_Bili;

        StringFormat m_DrawFormat = new StringFormat();
        #endregion
    }

    /// <summary>
    /// 速度指针
    /// </summary>
    public class SpeedPointer
    {
        /// <summary>
        /// 原始图片
        /// </summary>
        private Image m_OriginalPic;

        /// <summary>
        /// 需要绘制的图片
        /// </summary>
        private Image m_DrawPic;

        /// <summary>
        /// 需要画的最大角度
        /// </summary>
        private float m_DialAnglev;

        /// <summary>
        /// 指针初始角度
        /// </summary>
        private float m_InitalAnglev;

        /// <summary>
        /// 指针最大值
        /// </summary>
        private float m_MaxSpeed;

        /// <summary>
        /// 绘图顶点
        /// </summary>
        private RectangleF m_BackImgRect;

        /// <summary>
        /// 绘图中心点
        /// </summary>
        private PointF m_CentralPoint;

        private Matrix m_Matrix;

        /// <summary>
        /// 转动角度
        /// </summary>
        private float m_Anglev = 0;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="maxAnglev">最大角度</param>
        /// <param name="initAnglev">初始角度</param>
        /// <param name="maxValue">指针最大值</param>
        /// <param name="apexPoint">绘图位置大小</param>
        /// <param name="centrePoint">绘图中心点</param>
        /// <param name="pointerImg">指针图片</param>
        public SpeedPointer(float maxAnglev, float initAnglev, float maxValue, RectangleF apexRect, PointF centrePoint, Image pointerImg)
        {
            m_DialAnglev = maxAnglev;
            m_InitalAnglev = initAnglev;
            m_MaxSpeed = maxValue;
            m_BackImgRect = apexRect;
            m_CentralPoint = centrePoint;
            m_DrawPic = pointerImg;
        }

        public void PaintPointerNormal(Graphics g, ref float speed)
        {
            if (speed <= m_MaxSpeed)
            {
                m_Anglev = speed / m_MaxSpeed * m_DialAnglev + m_InitalAnglev;
            }
            m_Matrix = g.Transform;
            m_Matrix.RotateAt(m_Anglev, m_CentralPoint);
            g.Transform = m_Matrix;
            g.DrawImage(m_DrawPic, m_BackImgRect);
            m_Matrix.Reset();
            g.Transform = m_Matrix;
        }

        /// <summary>
        /// 绘指针(指针会变化颜色等)
        /// </summary>
        /// <param name="g"></param>
        /// <param name="speed"></param>
        public void PaintPointer(Graphics g, Image tmpPic, ref float speed)
        {
            if (speed <= m_MaxSpeed)
            {
                m_Anglev = speed / m_MaxSpeed * m_DialAnglev + m_InitalAnglev;
            }
            m_Matrix = g.Transform;
            m_Matrix.RotateAt(m_Anglev, m_CentralPoint);
            g.Transform = m_Matrix;
            g.DrawImage(tmpPic, m_BackImgRect);
            m_Matrix.Reset();
            g.Transform = m_Matrix;
        }
    }

    /// <summary>
    /// 变化矩形条
    /// </summary>
    public class NeedChangeLength
    {
        /// <summary>
        /// 当前要画的高度值
        /// </summary>
        private float m_ViewValue = 0;

        /// <summary>
        /// 需要增加的高度量
        /// </summary>
        private float m_TmpNeedChangeLength = 0;

        /// <summary>
        /// 递增量
        /// </summary>
        private float m_TmpStepLength = 5;

        /// <summary>
        /// 黄色画笔
        /// </summary>
        public static SolidBrush m_YellowBrush = new SolidBrush(Color.Yellow);

        /// <summary>
        /// 绘图起点
        /// </summary>
        PointF m_DrawPoint;

        /// <summary>
        /// 进度条宽度
        /// </summary>
        private int m_RectWidth;

        /// <summary>
        /// 对应比例
        /// </summary>
        private float m_RectScale;

        public NeedChangeLength(PointF point, int width, float dizeng, float scale)
        {
            m_DrawPoint = point;
            m_RectWidth = width;
            m_TmpStepLength = dizeng;
            m_RectScale = scale;
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
                if (m_ViewValue - m_TmpStepLength >= currentValue)
                {
                    m_TmpNeedChangeLength = -m_TmpStepLength;
                }
                else
                {
                    m_TmpNeedChangeLength = currentValue - m_ViewValue;
                }
            }
            else if (m_ViewValue < currentValue)
            {
                if (m_ViewValue + m_TmpStepLength <= currentValue)
                {
                    m_TmpNeedChangeLength = m_TmpStepLength;
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
                    e.FillRectangle(m_YellowBrush, new RectangleF(new PointF(m_DrawPoint.X - m_ViewValue * m_RectScale, m_DrawPoint.Y), new SizeF(m_ViewValue * m_RectScale, m_RectWidth)));
                    break;
                case 2://横右
                    e.FillRectangle(m_YellowBrush, new RectangleF(m_DrawPoint, new SizeF(m_ViewValue * m_RectScale, m_RectWidth)));
                    break;
                case 3://纵上
                    e.FillRectangle(m_YellowBrush, new RectangleF(new PointF(m_DrawPoint.X, m_DrawPoint.Y - m_ViewValue * m_RectScale), new SizeF(m_RectWidth, m_ViewValue * m_RectScale)));
                    break;
                case 4://纵下
                    e.FillRectangle(m_YellowBrush, new RectangleF(m_DrawPoint, new SizeF(m_RectWidth, m_ViewValue * m_RectScale)));
                    break;
                default:
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
        int m_FlashCount = 0;

        /// <summary>
        /// 闪烁间隔时间
        /// </summary>
        int m_FlashTime = 0;

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
            else if (m_FlashCount >= m_FlashTime * 5 && m_FlashCount < m_FlashTime * 10)
            {
                ++m_FlashCount;
                return false;
            }
            else
            {
                m_FlashCount = 0;
                return false;
            }
        }
    }

}
