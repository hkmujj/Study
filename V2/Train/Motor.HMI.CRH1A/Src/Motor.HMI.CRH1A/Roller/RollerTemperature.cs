using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using CommonUtil.Controls;

namespace Motor.HMI.CRH1A.Roller
{
    /// <summary>
    /// 轴温
    /// </summary>
    class RollerTemperatureControl : IInnerControl
    {
        public RollerTemperatureControl(Rectangle outLineRectangle)
        {
            OutLineRectangle = outLineRectangle;
            m_RectText = new GDIRectText();
            m_RectText.SetTextColor(0, 0, 0);
            m_RectText.SetTextStyle(8, FormatStyle.Center, true, "Arial");
            m_RectText.SetLinePen(43, 43, 43, 2);
            m_RectText.SetTextRect(outLineRectangle.X, outLineRectangle.Y, OutLineRectangle.Width, outLineRectangle.Height);
            m_RectText.Isdrawrectfrm = true;
            m_RectText.SetBkColor(NomarlTempColor.R, NomarlTempColor.G, NomarlTempColor.B);
        }

        private static readonly Color HighTempColor = Color.Red;
        private static readonly Color NomarlTempColor = Color.FromArgb(192, 192, 192);

        /// <summary>
        /// 温度
        /// </summary>
        public float Temperature
        {
            set
            {
                m_Temperature = value; 
                m_RectText.SetText(value.ToString("F0"));
            }
            get { return m_Temperature; }
        }

        /// <summary>
        /// 温度状态 
        /// </summary>
        public TemperatureState TemperatureState { set; get; }

        public Rectangle OutLineRectangle { private set; get; }

        /// <summary>
        /// 轮廓笔
        /// </summary>
        private readonly Pen m_OutLinePen = new Pen(Color.FromArgb(70, 71, 71), 2);

        private readonly GDIRectText m_RectText;
        private float m_Temperature;

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public bool OnMouseDown(Point point)
        {
            throw new NotImplementedException();
        }

        public bool OnMouseUp(Point point)
        {
            throw new NotImplementedException();
        }

        public void OnDraw(Graphics g)
        {
            m_RectText.OnDraw(g);

            //绘边框
            g.DrawLine(m_OutLinePen, OutLineRectangle.X, OutLineRectangle.Y + OutLineRectangle.Height, OutLineRectangle.X
                                                                                                  +
                                                                                                  OutLineRectangle.Width,
                OutLineRectangle.Y + OutLineRectangle.Height);
            g.DrawLine(m_OutLinePen, OutLineRectangle.X + OutLineRectangle.Width, OutLineRectangle.Y, OutLineRectangle.X
                                                                                                 +
                                                                                                 OutLineRectangle.Width,
                OutLineRectangle.Y + OutLineRectangle.Height);

            var bkColor = NomarlTempColor;
            if (TemperatureState == TemperatureState.High)
            {
                bkColor = HighTempColor;
            }
            m_RectText.SetBkColor(bkColor.R, bkColor.G, bkColor.B);
        }

        public void OnPaint(Graphics g)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Point point)
        {
            throw new NotImplementedException();
        }

        public Action<object> RefreshAction { get; set; }
        public object Tag { get; set; }
    }
}
