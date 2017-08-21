using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms.VisualStyles;
using CommonUtil.Controls;

namespace Motor.HMI.CRH1A.Roller
{
    public class TrainRoller : IInnerControl
    {
        /// <summary>
        /// 轮廓大小 
        /// </summary>
        public Rectangle OutLineRectangle { private set; get; }

        private readonly GT_HotBoxStatus m_GtHotBoxStatus;

        /// <summary>
        /// 默认的其它7个轮子的温度.
        /// </summary>
        private const int DefalutTemperatur = 29;

        public float[] Tempearaturs
        {
            set
            {
                m_Tempearaturs = value;
                if (value.Length != 8)
                {
                    return;
                }
                m_RollerT[0].Temperature = value[0];
                m_RollerT[0].TemperatureState = TemperatureStateAdpt.GeTemperatureState(m_GtHotBoxStatus.Valuef,
                    value[0]);

                for (int i = 1; i < m_RollerT.Length; i++)
                {
                    m_RollerT[i].Temperature = DefalutTemperatur;
                    m_RollerT[i].TemperatureState = TemperatureStateAdpt.GeTemperatureState(m_GtHotBoxStatus.Valuef, DefalutTemperatur);
                }
            }
            get { return m_Tempearaturs; }
        }

        public bool Visible { set; get; }

        /// <summary>
        /// 车身ID
        /// </summary>
        public string TrainBodyId
        {
            set
            {
                m_TrainBodyId = value; 
                m_TrainBodyIdControl.SetText(value);
            }
            get { return m_TrainBodyId; }
        }

        private readonly GDIRectText m_TrainBodyIdControl;

        /// <summary>
        /// 8 个轴的温度, 从左上到右下,依次编号.
        /// </summary>
        private readonly RollerTemperatureControl[] m_RollerT = new RollerTemperatureControl[8];

        /// <summary>
        /// 第一个轴温的大小 , 相对于 OutLineRectangle
        /// </summary>
        private readonly Rectangle m_TemperatuRectangle = new Rectangle(0, 0, 25, 15);

        /// <summary>
        /// 车身编号大小 , 相对于 m_TrainBodyRelativeRectangle ,x,y 忽略
        /// </summary>
        private readonly Rectangle m_TrainBodyIdRectangle = new Rectangle(0, 0, 25, 15);

        /// <summary>
        /// 车身的大小, 绝对位置
        /// </summary>
        private readonly Rectangle m_TrainBodyRelativeRectangle;

        private float[] m_Tempearaturs;
        private string m_TrainBodyId;

        public TrainRoller(Rectangle outLineRectangle, GT_HotBoxStatus gtHotBoxStatus)
        {
            Visible = true;

            OutLineRectangle = outLineRectangle;
            m_GtHotBoxStatus = gtHotBoxStatus;
            m_MaxInterval = outLineRectangle.Width - m_TemperatuRectangle.Width*4 - MinInterval*2;
            m_TrainBodyRelativeRectangle = new Rectangle(outLineRectangle.X,
                outLineRectangle.Y + m_TemperatuRectangle.Height + MinInterval,
                outLineRectangle.Width,
                OutLineRectangle.Height - m_TemperatuRectangle.Height * 2 - MinInterval * 2);

            m_TrainBodyIdControl = new GDIRectText();
            m_TrainBodyIdControl.SetTextRect(m_TrainBodyRelativeRectangle.X + m_TrainBodyRelativeRectangle.Width / 2 - m_TrainBodyIdRectangle.Width / 2,
                m_TrainBodyRelativeRectangle.Y + m_TrainBodyRelativeRectangle.Height / 2 - m_TrainBodyIdRectangle.Height / 2,
                m_TrainBodyIdRectangle.Width,
                m_TrainBodyIdRectangle.Height);
            m_TrainBodyIdControl.SetTextColor(0, 0, 0);
            m_TrainBodyIdControl.BkColor = Color.FromArgb(192, 192, 192);
            m_TrainBodyIdControl.SetTextStyle(10, FormatStyle.Center, true, "Arial");
            m_TrainBodyIdControl.SetLinePen(43, 43, 43, 2);

            Init();

            //TODO delete
            //var tp = new float[8];
            //tp[0] = 25;
            //Tempearaturs = tp;
        }

        private const int MinInterval = 4;
        private int m_MaxInterval = 30;

        private static readonly Pen TrainBodyOutLinePen = new Pen(Color.FromArgb(70, 71, 71), 2);

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            m_RollerT[0] = new RollerTemperatureControl(new Rectangle(OutLineRectangle.X + m_TemperatuRectangle.X, OutLineRectangle.Y + m_TemperatuRectangle.Y, m_TemperatuRectangle.Width, m_TemperatuRectangle.Height));
            m_RollerT[1] = new RollerTemperatureControl(new Rectangle(m_RollerT[0].OutLineRectangle.X + m_TemperatuRectangle.Width + MinInterval, OutLineRectangle.Y + m_TemperatuRectangle.Y, m_TemperatuRectangle.Width, m_TemperatuRectangle.Height));
            m_RollerT[2] = new RollerTemperatureControl(new Rectangle(m_RollerT[1].OutLineRectangle.X + m_TemperatuRectangle.Width + m_MaxInterval, OutLineRectangle.Y + m_TemperatuRectangle.Y, m_TemperatuRectangle.Width, m_TemperatuRectangle.Height));
            m_RollerT[3] = new RollerTemperatureControl(new Rectangle(m_RollerT[2].OutLineRectangle.X + m_TemperatuRectangle.Width + MinInterval, OutLineRectangle.Y + m_TemperatuRectangle.Y, m_TemperatuRectangle.Width, m_TemperatuRectangle.Height));

            m_RollerT[4] = new RollerTemperatureControl(new Rectangle(OutLineRectangle.X + m_TemperatuRectangle.X, m_RollerT[0].OutLineRectangle.Y + m_TemperatuRectangle.Height + m_TrainBodyRelativeRectangle.Height + MinInterval*2, m_TemperatuRectangle.Width, m_TemperatuRectangle.Height));
            m_RollerT[5] = new RollerTemperatureControl(new Rectangle(m_RollerT[4].OutLineRectangle.X + m_TemperatuRectangle.Width + MinInterval, m_RollerT[4].OutLineRectangle.Y, m_TemperatuRectangle.Width, m_TemperatuRectangle.Height));
            m_RollerT[6] = new RollerTemperatureControl(new Rectangle(m_RollerT[5].OutLineRectangle.X + m_TemperatuRectangle.Width + m_MaxInterval, m_RollerT[4].OutLineRectangle.Y, m_TemperatuRectangle.Width, m_TemperatuRectangle.Height));
            m_RollerT[7] = new RollerTemperatureControl(new Rectangle(m_RollerT[6].OutLineRectangle.X + m_TemperatuRectangle.Width + MinInterval, m_RollerT[4].OutLineRectangle.Y, m_TemperatuRectangle.Width, m_TemperatuRectangle.Height));

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
            if (!Visible)
            {
                return;
            }

            //轴
            foreach (var control in m_RollerT)
            {
                control.OnDraw(g);
            }
            // 车身
            m_TrainBodyIdControl.OnDraw(g);
            g.DrawRectangle(TrainBodyOutLinePen, m_TrainBodyRelativeRectangle);

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
