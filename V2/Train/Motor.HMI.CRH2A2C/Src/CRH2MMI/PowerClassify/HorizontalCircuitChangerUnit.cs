using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CommonUtil.Controls;

namespace CRH2MMI.PowerClassify.NewLogic
{
    class HorizontalCircuitChangerUnit : CircuitChangerUnit
    {
        private static readonly Size DefaultSize = new Size(70, 40);
        private static readonly Size TextSize = new Size(50, 12);
        protected const float TextSizeRatio = 1 / 2;


        // ReSharper disable once InconsistentNaming
        protected int m_WireLeght;

        /// <summary>
        ///  中间的宽度
        /// </summary>
        protected static readonly int MidWidth = DefaultSize.Width - TextSize.Width;

        /// <summary>
        /// 导线相对 Location 的x 偏移
        /// </summary>
        public static readonly int WireXOffset = MidWidth / 2;

        private const int MidInterval = 3;

        /// <summary>
        /// 中间的高度
        /// </summary>
        protected static readonly int MidHight = 10;


        public string Text
        {
            protected set { m_Text.Text = value; }
            get { return m_Text.Text; }
        }

        public HorizontalCircuitChangerUnit()
        {
            m_WireLeght = 10;
            this.UnitOrientation = Orientation.Vertical;
            m_Text = new GDIRectText()
            {
                TextColor = Color.White,
                Size = TextSize,
                NeedDarwOutline = false,
                BackColorVisible = false,
                DrawFont = new Font("Arial", 10)
            };
        }

        protected override void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            ActureOutLine = new Rectangle(Location, DefaultSize);
            m_Text.Location = new Point(Location.X + MidWidth, Location.Y + DefaultSize.Height / 2 - TextSize.Height / 2);
            m_MidRectangle = new Rectangle(Location.X + MidInterval,
                Location.Y + DefaultSize.Height / 2 - MidHight / 2, MidWidth - MidInterval * 2, MidHight);

            m_Lines = new List<List<Point>>()
            {
                new List<Point>()
                {
                    new Point(Location.X , Location.Y + DefaultSize.Height/2 - MidHight/2),
                    new Point(Location.X + MidWidth, Location.Y + DefaultSize.Height/2 - MidHight/2)
                },
                new List<Point>()
                {
                    new Point(Location.X ,Location.Y+DefaultSize.Height/2 + MidHight/2),
                    new Point(Location.X + MidWidth,Location.Y+DefaultSize.Height/2 + MidHight/2)
                },
            };

            var midHight = MidHight + MidInterval * 2;
            var midY = Location.Y + DefaultSize.Height / 2;
            m_StraightWires = new List<StraightWire>()
            {
                new StraightWire()
                {
                    EndPoint = new Point(Location.X + MidWidth/2, midY - midHight/2 - m_WireLeght ),
                    StartPoint = new Point(Location.X + MidWidth/2, midY - midHight/2),
                },
                new StraightWire
                {
                    StartPoint =
                        new Point(Location.X + MidWidth/2,  midY + midHight/2),
                    EndPoint =
                        new Point(Location.X + MidWidth/2, midY + midHight/2 + m_WireLeght ),
                },
            };

        }
    }
}
