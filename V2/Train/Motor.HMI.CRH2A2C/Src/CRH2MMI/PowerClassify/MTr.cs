using System;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Model;
using CRH2MMI.Common.Util;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.PowerClassify
{
    internal class MTr : UnitBase
    {
        private readonly GDIRectText m_Text;
        private readonly StraightWire m_StraightWire;
        private bool m_IsPowerOn;

        /// <summary>
        /// 实际的轮廓
        /// </summary>
        public override Rectangle ActureOutLine { get; protected set; }

        /// <summary>
        /// 默认大小 包含导线 
        /// </summary>
        public static readonly Size DefaultSize = new Size(35, 40);

        /// <summary>
        /// 文本占的比例
        /// </summary>
        private const float TextRaite = 2f / 3f;

        /// <summary>
        /// 输出的颜色
        /// </summary>
        public Color OutColor
        {
            get { return m_StraightWire.Color; }
        }

        public StraightWire OutputWire
        {
            get { return m_StraightWire; }
        }

        /// <summary>
        /// 有电时的颜色
        /// </summary>
        public Color HasVoltageColor { get; set; }

        private PowerFrom m_PowerType;

        ///// <summary>
        ///// 是否有电压
        ///// </summary>
        //public bool HasVoltage
        //{
        //    set
        //    {
        //        m_HasVoltage = value;
        //        m_StraightWire.IsPowerOn = value;
        //        RefreshColor();
        //    }
        //    get { return m_HasVoltage; }
        //}

        //private void RefreshColor()
        //{
        //    if (m_HasVoltage)
        //    {
        //        m_Text.TextColor = Color.Black;
        //        m_Text.BkColor = HasVoltageColor;
        //    }
        //    else
        //    {
        //        m_Text.TextColor = Color.White;
        //        m_Text.BkColor = Color.Black;
        //    }
        //}

        /// <summary>
        /// 线的方向 
        /// </summary>
        public Direction Direction { set; get; }

        public override bool IsPowerOn
        {
            get { return m_IsPowerOn; }
            set
            {
                m_IsPowerOn = value;
                RefreshByState(value ? m_PowerType : PowerFrom.Null);
            }
        }

        public MTr(PowerFrom type)
        {
            m_StraightWire = new StraightWire()
            {
                //IsPowerOn = true,
            };

            PowerFrom = type;

            SetMtrType(type);

            m_Text = new GDIRectText()
            {
                Text = "MTr",
                NeedDarwOutline = true
            };

            //Color = powerOnColor;

            Direction = Direction.Down;
            HasVoltageColor = Color.Yellow;
            //HasVoltage = false;

            OutLineChanged = OnOutLineChanged;
        }

        public void SetMtrType(PowerFrom type)
        {
            m_PowerType = type;
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            ActureOutLine = new Rectangle(Location, DefaultSize);
            m_Text.OutLineRectangle = new Rectangle(Location, new Size(DefaultSize.Width, (int)(DefaultSize.Height * TextRaite)));
            switch (Direction)
            {
                case Direction.Up:
                    break;
                case Direction.Down:
                    m_StraightWire.StartPoint = new Point(Location.X + DefaultSize.Width / 2, m_Text.OutLineRectangle.Bottom);
                    m_StraightWire.EndPoint = new Point(Location.X + DefaultSize.Width / 2, Location.Y + DefaultSize.Height);
                    break;
                case Direction.Left:
                    break;
                case Direction.Right:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void OnDraw(Graphics g)
        {
            m_Text.OnDraw(g);

            m_StraightWire.OnDraw(g);
        }

        public override void RefreshByState(PowerFrom powerFrom)
        {
            PowerFrom = powerFrom;
            var color = PowerFromColorAdaptor.GetColor(powerFrom);

            m_StraightWire.RefreshByState(powerFrom);

            if (powerFrom != PowerFrom.Null)
            {
                m_Text.TextColor = Color.Black;
                m_Text.BkColor = color;
            }
            else
            {
                m_Text.TextColor = Color.White;
                m_Text.BkColor = Color.Black;
            }
        }
    }
}
