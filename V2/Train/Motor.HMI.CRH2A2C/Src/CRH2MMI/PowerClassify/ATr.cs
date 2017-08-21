using System;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Model;
using CRH2MMI.Common.Util;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.PowerClassify
{
    class ATr : UnitBase
    {
        public Direction TextDirection { set; get; }

        private Rectangle m_Rectangle;
        /// <summary>
        /// 文本
        /// </summary>
        private readonly GDIRectText m_Text;

        /// <summary>
        /// 矩形的
        /// </summary>
        private readonly SolidBrush m_RectBrush;

        private int m_WireLengh;

        /// <summary>
        /// 导线 
        /// </summary>
        public StraightWire InputWire { private set; get; }

        public override Rectangle ActureOutLine { get; protected set; }
        public override void RefreshByState(PowerFrom powerFrom)
        {
            var color = PowerFromColorAdaptor.GetColor(powerFrom);
            m_RectBrush.Color = color;

            //InputWire.RefreshByState(powerFrom);
        }

        /// <summary>
        /// 输出的导线
        /// </summary>
        public StraightWire OutPutWire { private set; get; }

        public static readonly Size TextSize = new Size(30, 10);

        public static readonly Size RectSize = new Size(10, 15);

        public static readonly Size WireSize = new Size(2, 10);

        public const int DefaultWireLeght = 10;

        public const int RectAndTextInterval = 1;

        /// <summary>
        /// 导线的长度
        /// </summary>
        public int WireLengh
        {
            set
            {
                m_WireLengh = value;
                OnOutLineChanged(null, null);
            }
            get { return m_WireLengh; }
        }

        public ATr()
        {
            TextDirection = Direction.Right;
            m_Text = new GDIRectText()
            {
                Text = "ATr",
                TextColor = Color.White,
                NeedDarwOutline = false,
                DrawFont = new Font("Arial", 10),
                Size = TextSize
            };
            InputWire = new StraightWire();
            OutPutWire = new StraightWire();
            m_RectBrush = new SolidBrush(Color.White);
            m_Rectangle.Size = RectSize;
            m_WireLengh = DefaultWireLeght;
            OutLineChanged = OnOutLineChanged;
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            switch (TextDirection)
            {
                case Direction.Up:
                    break;
                case Direction.Down:
                    break;
                case Direction.Left:
                    InitTextLeft();
                    break;
                case Direction.Right:
                    InitTextRight();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ActureOutLine = new Rectangle(m_Rectangle.X, InputWire.StartPoint.Y, m_Rectangle.Width + TextSize.Width,
                OutPutWire.EndPoint.Y - InputWire.StartPoint.Y);
        }

        private void InitTextLeft()
        {
            var loca = Location;
            m_Rectangle.Location = new Point(loca.X + TextSize.Width + RectAndTextInterval, loca.Y + WireLengh);
            m_Text.Location = new Point(loca.X, loca.Y + WireLengh);
            OutPutWire.StartPoint = new Point(m_Rectangle.X + m_Rectangle.Width / 2, m_Rectangle.Bottom);
            OutPutWire.EndPoint = new Point(m_Rectangle.X + m_Rectangle.Width / 2, m_Rectangle.Bottom + WireLengh);
            InputWire.EndPoint = new Point(m_Rectangle.X + m_Rectangle.Width / 2, m_Rectangle.Top - WireLengh);
            InputWire.StartPoint = new Point(m_Rectangle.X + m_Rectangle.Width / 2, m_Rectangle.Top);

        }

        private void InitTextRight()
        {
            var loca = Location;
            m_Rectangle.Location = new Point(loca.X , loca.Y + WireLengh); 
            m_Text.Location = new Point(loca.X + RectSize.Width + RectAndTextInterval, loca.Y + WireLengh);
            OutPutWire.StartPoint = new Point(m_Rectangle.X + m_Rectangle.Width / 2, m_Rectangle.Bottom);
            OutPutWire.EndPoint = new Point(m_Rectangle.X + m_Rectangle.Width / 2, m_Rectangle.Bottom + WireLengh);
            InputWire.EndPoint = new Point(m_Rectangle.X + m_Rectangle.Width / 2, m_Rectangle.Top - WireLengh);
            InputWire.StartPoint = new Point(m_Rectangle.X + m_Rectangle.Width / 2, m_Rectangle.Top);
        }

        public override void OnDraw(Graphics g)
        {
            //m_RectBrush.Color = InputWire.GetCurrentColor();
            g.FillRectangle(m_RectBrush, m_Rectangle);

            if (TextVisible)
            {
                m_Text.OnDraw(g);
            }

            //InputWire.OnDraw(g);
            OutPutWire.OnDraw(g);
        }

        public override void Reverse()
        {
            TextDirection = ~TextDirection;
            OnOutLineChanged(null, null);
        }
    }
}
