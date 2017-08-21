using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using CommonUtil.Controls;

namespace Motor.HMI.CRH1A.TractionBrake
{
    /// <summary>
    /// 加速度
    /// </summary>
    public class AccelerationCtrol : IInnerControl
    {
        /// <summary>
        /// 缩放比例 , 默认 1
        /// </summary>
        public float ZoomRate
        {
            set
            {
                m_ZoomRate = value;
                ActureSize = new Size((int)(DefaulteSize.Width * value), (int)(DefaulteSize.Height * value));
            }
            get { return m_ZoomRate; }
        }

        public static Size DefaulteSize = new Size(100, 80);
        private float m_ZoomRate;

        /// <summary>
        /// 实际大小 
        /// </summary>
        public Size ActureSize { private set; get; }

        /// <summary>
        /// 左上角的点
        /// </summary>
        public Point StartPoint { private set; get; }


        /// <summary>
        /// 
        /// </summary>
        private GDIRectText m_TextCtrol = new GDIRectText();

        /// <summary>
        /// 加速度值
        /// </summary>
        private GDIRectText m_AccValueTextCtrol = new GDIRectText();

        private float m_Acceleration;
        private string m_AccelerationInfo;

        /// <summary>
        /// 加速度
        /// </summary>
        public float Acceleration
        {
            set
            {
                m_Acceleration = value;
                m_AccelerationInfo = string.Format("{0} m/s^2", value.ToString("F1"));
            }
            get { return m_Acceleration; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startPoint">左上角点</param>
        public AccelerationCtrol(Point startPoint)
        {
            StartPoint = startPoint;
            ZoomRate = 1;
            Acceleration = 0;
            Init();
        }


        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            var textColor = Color.Black;
            m_TextCtrol.SetText("加速 / 减速");
            m_TextCtrol.SetTextColor(textColor.R, textColor.G, textColor.B);
            m_TextCtrol.SetTextStyle(10, FormatStyle.Center, true, "Arial");
            m_TextCtrol.SetLinePen(121, 121, 121, 1);
            m_TextCtrol.SetBkColor(192, 192, 192);

            m_AccValueTextCtrol.SetText(m_AccelerationInfo);
            m_AccValueTextCtrol.SetTextColor(textColor.R, textColor.G, textColor.B);
            var bkColor = Color.WhiteSmoke;
            m_AccValueTextCtrol.SetBkColor(bkColor.R, bkColor.G, bkColor.B);
            m_AccValueTextCtrol.SetTextStyle(10, FormatStyle.Center, true, "Arial");
            m_AccValueTextCtrol.SetLinePen(121, 121, 121, 1); 

            SetPosition();

        }

        /// <summary>
        /// 设置大小 ,位置等
        /// </summary>
        private void SetPosition()
        {
            // 占1/3
            m_TextCtrol.SetTextRect(StartPoint.X, StartPoint.Y, ActureSize.Width, ActureSize.Height / 3);
            m_AccValueTextCtrol.SetTextRect(StartPoint.X, StartPoint.Y + ActureSize.Height / 3, ActureSize.Width, ActureSize.Height / 3 * 2);
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
            m_TextCtrol.OnDraw(g);
            m_AccValueTextCtrol.SetText(m_AccelerationInfo);
            m_AccValueTextCtrol.OnDraw(g);
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
