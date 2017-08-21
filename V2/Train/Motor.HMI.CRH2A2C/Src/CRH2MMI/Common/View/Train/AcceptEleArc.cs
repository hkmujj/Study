using System;
using System.Drawing;
using CommonUtil.Model;
using CommonUtil.Util.Extension;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Util;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.Common.View.Train
{
    /// <summary>
    /// 受电弓, 只需要location size 固定
    /// </summary>
    class AcceptEleArc : UnitBase
    {
        private AcceptEleArcState m_State;
        private string m_StateContent;
        private Direction m_Direction;
        private readonly AngleBracket m_AngleBracket;
        private Direction m_AngleBracketLocation;

        public static readonly Size DefaultSize = new Size(10, 20);

        public override Size Size
        {
            get { return DefaultSize; }
            set { throw new InvalidOperationException("不能设置大小, 只能设置 Location"); }
        }

        /// <summary>
        /// 受电弓状态
        /// </summary>
        public AcceptEleArcState State
        {
            set
            {
                if (m_State != value)
                {
                    m_State = value;
                    RefreshContent();
                }
            }
            get { return m_State; }
        }

        public Direction AngleBracketLocation
        {
            set
            {
                if (m_AngleBracketLocation != value)
                {
                    m_AngleBracketLocation = value;
                    RefreshContent();
                }
            }
            get { return m_AngleBracketLocation; }
        }

        public Direction Direction
        {
            set
            {
                if (m_Direction != value)
                {
                    m_Direction = value;
                    RefreshContent();
                }
            }
            get { return m_Direction; }
        }

        private void RefreshContent()
        {
            if (m_State == AcceptEleArcState.Down)
            {
                m_AngleBracket.AngleSplay = 0;
            }
            else
            {
                m_AngleBracket.AngleSplay = Math.PI / 2f;
                m_AngleBracket.AngleOrientation = m_Direction == Direction.Up || m_Direction == Direction.Left ? -Math.PI : 0;
            }
            m_AngleBracket.OutLineRectangle = GetAngleBracketOutline();
            m_AngleBracket.Generate();
        }

        public AcceptEleArc()
        {
            m_State = AcceptEleArcState.Down;
            m_Direction = Direction.Left;
            m_AngleBracket = new AngleBracket();

            this.OutLineChanged += (sender, args) =>
            {
                m_AngleBracket.OutLineRectangle = GetAngleBracketOutline();
                m_AngleBracket.Generate();
            };

            RefreshContent();
        }

        private Rectangle GetAngleBracketOutline()
        {
            var loc = new Point(Location.X, Location.Y);
            loc = m_AngleBracketLocation == Direction.Up || m_AngleBracketLocation == Direction.Left ? loc : loc.Translate(Car.DefaultCarNameSize.Width + Size.Width/2, 0);
            return new Rectangle(loc, Size);
        }

        public override void OnDraw(Graphics g)
        {
            //g.DrawString(m_StateContent, CRH2Resource.Font16, CRH2Resource.WhiteBrush, Location);
            m_AngleBracket.OnDraw(g);
        }

        public override void Reverse()
        {
            Direction = ~Direction;
        }

        public override void RefreshByState(PowerFrom powerFrom)
        {
            throw new NotImplementedException();
        }
    }
}
