using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CommonUtil.Controls;
using CommonUtil.Model;

namespace CRH2MMI.PowerClassify
{
    class BKK : CircuitChangerUnit
    {
        private static readonly Size DefaultSize = new Size(50, 40);
        private static readonly Size TextSize = new Size(40, 15);
        protected const float TextSizeRatio = 1 / 2;

        public Direction Direction
        {
            get { return m_Direction; }
            set
            {
                if (value == m_Direction)
                {
                    return;
                }
                m_Direction = value;
                if (value == Direction.Up)
                {
                    m_Text.Size = new Size(TextSize.Height, TextSize.Width);
                    m_Text.Location = new Point(m_Text.Location.X + 20, m_Text.Location.Y - 25);
                }

            }
        }

        /// <summary>
        ///  中间的宽度
        /// </summary>
        protected const int MidWidth = 10;

        private const int MidInterval = 3;

        public static readonly int WireYOffset;

        /// <summary>
        /// 中间的高度
        /// </summary>
        protected static readonly int MidHight;

        private Direction m_Direction;

        static BKK()
        {
            MidHight = DefaultSize.Height - TextSize.Height - MidInterval * 2;
            WireYOffset = DefaultSize.Height - MidHight + MidInterval * 2;
        }

        public BKK()
        {
            this.UnitOrientation = Orientation.Horizontal;
            m_Text = new GDIRectText()
            {
                Text = "BKK",
                TextColor = Color.White,
                Size = TextSize,
                NeedDarwOutline = false,
                BackColorVisible = false,
                DrawFont = new Font("Arial", 10)
            };

            //RefreshMidRectColor();

            //IsPowerOnChanged += (sender, args) => RefreshMidRectColor();
        }

        protected override void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            ActureOutLine = new Rectangle(Location, DefaultSize);
            m_Text.Location = new Point(Location.X + 4, Location.Y);
            m_MidRectangle = new Rectangle(Location.X + DefaultSize.Width / 2 - MidWidth / 2,
                Location.Y + TextSize.Height + MidInterval, MidWidth, MidHight);

            m_Lines = new List<List<Point>>()
            {
                new List<Point>()
                {
                    new Point(Location.X + DefaultSize.Width/2 - MidWidth/2, m_Text.OutLineRectangle.Bottom),
                    new Point(Location.X + DefaultSize.Width/2 - MidWidth/2, Location.Y + DefaultSize.Height)
                },
                new List<Point>()
                {
                    new Point(Location.X + DefaultSize.Width/2 + MidWidth/2, m_Text.OutLineRectangle.Bottom),
                    new Point(Location.X + DefaultSize.Width/2 + MidWidth/2, Location.Y + DefaultSize.Height)
                },
            };

            m_StraightWires = new List<StraightWire>()
            {
                new StraightWire()
                {
                    StartPoint = new Point(Location.X, Location.Y + WireYOffset),
                    EndPoint = new Point(Location.X + DefaultSize.Width/2, Location.Y + WireYOffset),
                    //PowerOnChanged = OnInputPowerOnChanged
                },
                new StraightWire
                {
                    StartPoint =
                        new Point(Location.X + DefaultSize.Width/2 + MidWidth/2,
                            Location.Y + WireYOffset),
                    EndPoint =
                        new Point(Location.X + DefaultSize.Width,
                            Location.Y + WireYOffset),
                },
            };
        }

    }
}
