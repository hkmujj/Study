using System;
using System.Collections.Generic;
using System.Drawing;
using CRH2MMI.Common.Util;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.PowerClassify
{
    class PowerLines : UnitBase
    {
        private readonly Dictionary<PowerLineType, PowerLine> m_Lines;

        public static readonly Size DefaultSize = new Size();

        public PowerLines(Point location)
        {
            Location = location;
            m_Lines = new Dictionary<PowerLineType, PowerLine>()
            {
                {
                    PowerLineType.AC4001, new PowerLine("AC400", location)
                },
                {
                    PowerLineType.AC4002, new PowerLine("AC400", new Point(location.X, location.Y + 80))
                },
                {
                    PowerLineType.DC100, new PowerLine("DC100", new Point(location.X, location.Y + 80 + 20))
                },
                {
                    PowerLineType.AC220, new PowerLine("AC220", new Point(location.X, location.Y + 80 + 20*2))
                },
                {
                    PowerLineType.AC1001,
                    new PowerLine("AC100\r\n(稳压)", new Point(location.X, location.Y + 80 + 20*3),
                        PowerLine.TextSize.Height*2)
                },
                {
                    PowerLineType.AC1002, new PowerLine("AC100", new Point(location.X, location.Y + 80 + 20*5))
                },
            };
        }

        public PowerLine this[PowerLineType lineType]
        {
            get
            {
                return m_Lines[lineType];
            }
        }

        public override void OnDraw(Graphics g)
        {
            foreach (var line in m_Lines.Values)
            {
                line.OnDraw(g);
            }
        }

        [Obsolete]
        public override void Reverse()
        {
            foreach (var line in m_Lines.Values)
            {
                line.Reverse();
            }
        }

        public override void RefreshByState(PowerFrom powerFrom)
        {
            
        }
    }
}
