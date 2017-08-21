using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using CRH2MMI.Common.Util;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.PowerClassify
{
    class PowerLine : UnitBase
    {
        public StraightWire LeftWire { private set; get; }

        public StraightWire RightWire { private set; get; }

        /// <summary>
        /// 所有导线 
        /// </summary>
        public List<StraightWire> Wires { private set; get; }

        //public static readonly Size DefaultSize = new Size(200,15);

        public static readonly Size TextSize = new Size(90, 15);

        public const int LineLenght = 170;

        /// <summary>
        /// 横向的间隙 
        /// </summary>
        public const int LineHInterval = 20;

        /// <summary>
        /// 文本和线间的间隙
        /// </summary>
        public const int TextLineInterval = 115;

        private readonly GDIRectText m_TitleText;

        public PowerLine(string name, Point location, int height)
        {
            Location = location;
            m_TitleText = new GDIRectText()
                          {
                              Text = name,
                              OutLineRectangle = new Rectangle(location, new Size(TextSize.Width, height))
                          };

            var lineY = location.Y + height / 2;
            LeftWire = new StraightWire()
                       {
                           StartPoint = new Point(location.X + TextSize.Width + TextLineInterval, lineY),
                           EndPoint = new Point(location.X + TextSize.Width + TextLineInterval + LineLenght, lineY),
                       };

            var rLoca = LeftWire.EndPoint;
            RightWire = new StraightWire()
                        {
                            StartPoint = new Point(rLoca.X + LineHInterval, lineY),
                            EndPoint = new Point(rLoca.X + LineHInterval + LineLenght + 10, lineY),
                        };

            Wires = new List<StraightWire>()
                    {
                        LeftWire,
                        RightWire
                    };
        }

        public PowerLine(string name, Point location)
            : this(name, location, TextSize.Height)
        {
            
        }

        public override void OnDraw(Graphics g)
        {
            m_TitleText.OnDraw(g);

            Wires.ForEach(e => e.OnDraw(g));

            //LeftWire.OnDraw(g);

            //RightWire.OnDraw(g);
        }

        public override void Reverse()
        {
            var start = LeftWire.StartPoint;
            var end = LeftWire.EndPoint;
            LeftWire.StartPoint = RightWire.StartPoint;
            LeftWire.EndPoint = RightWire.EndPoint;
            RightWire.StartPoint = start;
            RightWire.EndPoint = end;
        }

        public override void RefreshByState(PowerFrom powerFrom)
        {
            
        }
    }
}
