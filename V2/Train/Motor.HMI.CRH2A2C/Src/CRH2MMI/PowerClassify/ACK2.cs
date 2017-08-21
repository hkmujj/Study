using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CommonUtil.Controls;

namespace CRH2MMI.PowerClassify
{
    class ACK2 : CircuitChangerUnit
    {
        private static readonly Size DefaultSize = new Size(50, 40);
        private static readonly Size TextSize = new Size(50, 15);
        protected const float TextSizeRatio = 1 / 2;

        /// <summary>
        ///  中间的宽度
        /// </summary>
        protected const int MidWidth = 10;

        private const int MidInterval = 3;

        /// <summary>
        /// 中间的高度
        /// </summary>
        protected static readonly int MidHight = DefaultSize.Height - TextSize.Height - MidInterval * 2;


        public ACK2()
        {
            this.UnitOrientation = Orientation.Horizontal;
            m_Text = new GDIRectText()
            {
                Text = "ACK2",
                TextColor = Color.White,
                Size = TextSize,
                NeedDarwOutline = false,
                DrawFont = new Font("Arial", 10)
            };
        }

        protected override void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            ActureOutLine = new Rectangle(Location, DefaultSize);
            m_Text.Location = new Point(Location.X , Location.Y + MidHight + 2 * MidInterval);
            m_MidRectangle = new Rectangle(Location.X + DefaultSize.Width / 2 - MidWidth / 2,
                Location.Y + MidInterval, MidWidth, MidHight);

            m_Lines = new List<List<Point>>()
            {
                new List<Point>()
                {
                    new Point(Location.X + DefaultSize.Width/2 - MidWidth/2, Location.Y),
                    new Point(Location.X + DefaultSize.Width/2 - MidWidth/2, m_Text.OutLineRectangle.Top)
                },
                new List<Point>()
                {
                    new Point(Location.X + DefaultSize.Width/2 + MidWidth/2,Location.Y),
                    new Point(Location.X + DefaultSize.Width/2 + MidWidth/2, m_Text.OutLineRectangle.Top)
                },
            };

            var midHight = MidHight + MidInterval * 2;
            m_StraightWires = new List<StraightWire>()
            {
                new StraightWire()
                {
                    StartPoint = new Point(Location.X, Location.Y +  midHight/2),
                    EndPoint = new Point(Location.X + DefaultSize.Width/2, Location.Y +  midHight/2),
                },
                new StraightWire
                {
                    StartPoint =
                        new Point(Location.X + DefaultSize.Width/2 + MidWidth/2,
                            Location.Y +  midHight/2),
                    EndPoint =
                        new Point(Location.X + DefaultSize.Width,
                            Location.Y +  midHight/2),
                },
            };

        }
    }
}
