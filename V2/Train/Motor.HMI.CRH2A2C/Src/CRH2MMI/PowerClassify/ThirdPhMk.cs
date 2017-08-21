using System;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Model;
using CRH2MMI.Common.Util;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.PowerClassify
{
    /// <summary>
    /// 3ph mk
    /// </summary>
    class ThirdPhMk : UnitBase
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
        public StraightWire StraightWire { private set; get; }

        //public Color PowerOnColor { set; get; }

        /// <summary>
        /// 实际的轮廓
        /// </summary>
        public override Rectangle ActureOutLine { get; protected set; }

        public override void RefreshByState(PowerFrom powerFrom)
        {
            m_RectBrush.Color = PowerFromColorAdaptor.GetColor(powerFrom);
        }

        public static readonly Size TextSize = new Size(50, 15);

        public static readonly Size RectSize = new Size(10, 15);

        public static readonly Size WireSize = new Size(2, 10);

        public const int DefalutWireLeght = 10;

        public const int RectAndTextInterval = 10;

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

        public ThirdPhMk()
        {
            TextDirection = Direction.Right;
            m_Text = new GDIRectText()
            {
                Text = "3PhMK",
                TextColor = Color.White,
                NeedDarwOutline = false,
                DrawFont = new Font("Arial", 10),
                Size = TextSize
            };
            StraightWire = new StraightWire();
            m_RectBrush = new SolidBrush(PowerFromColorAdaptor.GetColor(PowerFrom.Null));
            m_Rectangle.Size = RectSize;
            m_WireLengh = DefalutWireLeght;
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
            ActureOutLine =
                new Rectangle(m_Rectangle.X < m_Text.OutLineRectangle.X ? m_Rectangle.X : m_Text.OutLineRectangle.X,
                    m_Rectangle.Top,
                    m_Rectangle.Right < m_Text.OutLineRectangle.Right
                        ? m_Text.OutLineRectangle.Right - m_Rectangle.X
                        : m_Rectangle.Right - m_Text.OutLineRectangle.X,
                    StraightWire.EndPoint.Y - Location.Y);
        }

        private void InitTextLeft()
        {
            m_Rectangle.Location = new Point(Location.X + TextSize.Width + RectAndTextInterval, Location.Y);
            m_Text.Location = Location;
            StraightWire.StartPoint = new Point(m_Rectangle.X + m_Rectangle.Width / 2, m_Rectangle.Bottom);
            StraightWire.EndPoint = new Point(m_Rectangle.X + m_Rectangle.Width / 2, m_Rectangle.Bottom + WireLengh);
        }

        private void InitTextRight()
        {
            m_Rectangle.Location = Location;
            m_Text.Location = new Point(Location.X + RectSize.Width + RectAndTextInterval, Location.Y);
            StraightWire.StartPoint = new Point(m_Rectangle.X + m_Rectangle.Width / 2, m_Rectangle.Bottom);
            StraightWire.EndPoint = new Point(m_Rectangle.X + m_Rectangle.Width / 2, m_Rectangle.Bottom + WireLengh);
        }

        public override void OnDraw(Graphics g)
        {
            g.FillRectangle(m_RectBrush, m_Rectangle);

            if (TextVisible)
            {
                m_Text.OnDraw(g);
            }

            StraightWire.OnDraw(g);
        }

        public override void Reverse()
        {
            TextDirection = ~TextDirection;
            OnOutLineChanged(null, null);
        }
    }
}
