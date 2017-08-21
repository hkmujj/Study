using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.系统.Temperature
{
    public class GDITemperature
    {

        public int Type { get; private set; }
        public int ID { get; private set; }

        private readonly CRH3C380BBase m_BaseClass;

        public GDITemperature(CRH3C380BBase baseClase, int id, int type)
        {
            m_BaseClass = baseClase;
            BackgroundColor = new SolidBrush(Color.Yellow);
            TextFormat = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
            DrawFont = new Font("宋体", 20f);
            TextColor = new SolidBrush(Color.Black);
            LinePen = new Pen(Color.White);
            NeedOutLine = false;
            CurrenView = 0;
            Type = type;
            ID = id;
            InFoaltDictionary = new Dictionary<int, int>();
            OutRectangle = new Dictionary<int, RectangleF>();
        }
        /// <summary>
        /// 文字布局信息
        /// </summary>
        public StringFormat TextFormat { get; set; }

        /// <summary>
        /// 文字字体
        /// </summary>
        public Font DrawFont { get; set; }
        /// <summary>
        /// 文字颜色
        /// </summary>
        public SolidBrush TextColor { get; set; }

        /// <summary>
        /// 需要刻画的区域
        /// </summary>
        public Dictionary<int, RectangleF> OutRectangle
        {
            get;
            set;
        }

        /// <summary>
        /// 输入数值
        /// </summary>
        public Dictionary<int, int> InFoaltDictionary
        {
            get;
            set;
        }
        /// <summary>
        /// 背景颜色
        /// </summary>
        public SolidBrush BackgroundColor { get; set; }
        /// <summary>
        /// 线条颜色
        /// </summary>
        public Pen LinePen { get; set; }
        /// <summary>
        /// 是否需要画外边框
        /// </summary>
        public bool NeedOutLine { get; set; }
        /// <summary>
        /// 当前处于哪个视图
        /// </summary>
        public int CurrenView { get; private set; }

        public void NavigationTo(int currentview)
        {
            CurrenView = currentview;
        }

        /// <summary>
        /// 画当前视图方框
        /// </summary>
        /// <param name="g"></param>
        private void DrawRectangle(Graphics g)
        {
            if (NeedOutLine)
            {
                g.DrawRectangle(LinePen, Rectangle.Round(OutRectangle[CurrenView]));
            }
        }

        /// <summary>
        /// 填充区域
        /// </summary>
        /// <param name="g"></param>
        private void FillRectangles(Graphics g)
        {
            g.FillRectangle(BackgroundColor, OutRectangle[CurrenView]);
        }

        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="g"></param>
        public void OnDraw(Graphics g)
        {
            if (!InFoaltDictionary.ContainsKey(CurrenView))
            {
                return;
            }
            FillRectangles(g);
            DrawRectangle(g);
            DrawValue(g);
        }
        /// <summary>
        /// 画温度值
        /// </summary>
        /// <param name="g"></param>
        public void DrawValue(Graphics g)
        {
            g.DrawString(m_BaseClass.FloatList[InFoaltDictionary[CurrenView]].ToString(CultureInfo.InvariantCulture), DrawFont, TextColor, OutRectangle[CurrenView], TextFormat);
        }
    }
}