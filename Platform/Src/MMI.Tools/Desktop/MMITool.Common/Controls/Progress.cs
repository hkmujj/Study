using System;
using System.Drawing;
using MMITool.Common.Model;

namespace MMITool.Common.Controls
{
    /// <summary>
    /// 进度条, Size
    /// </summary>
    public class Progress : CommonInnerControlBase
    {
        /// <summary>
        /// 进度 百分比
        /// </summary>
        public float ProgressPercentage
        {
            set
            {
                m_ProgressPercentage = value;
                if (float.Epsilon < value - 1)
                {
                    m_ProgressPercentage = 1;
                }
                if (value - 0 < float.Epsilon)
                {
                    m_ProgressPercentage = 0;
                }
                RefreshDrawRectangle();
            }
            get { return m_ProgressPercentage; }
        }


        /// <summary>
        /// 最大的可能的值
        /// </summary>
        public float MaxValue { set; get; }

        /// <summary>
        /// 当前值
        /// </summary>
        public float CurrentValue
        {
            set
            {
                m_CurrentValue = value;
                ProgressPercentage = value / MaxValue;
            }
            get { return m_CurrentValue; }
        }

        /// <summary>
        /// 最大画图的值
        /// </summary>
        public float MaxDrawValue { set; get; }

        /// <summary>
        ///  方向
        /// </summary>
        public Direction Direction { private set; get; }

        /// <summary>
        /// 颜色
        /// </summary>
        public Color Color
        {
            set
            {
                m_Brush.Color = value;
            }
            get { return m_Brush.Color; }
        }

        /// <summary>
        /// 边框是否可见
        /// </summary>
        public bool IsOutlineVisible { set; get; }

        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color OutLineColor{ set { m_OutlinePen.Color = value; } get { return m_OutlinePen.Color; } }


        /// <summary>
        /// 绘图区域
        /// </summary>
        private Rectangle m_DrawRectangle;

        private float m_ProgressPercentage;
        private readonly SolidBrush m_Brush;
        private readonly Pen m_OutlinePen;
        private float m_CurrentValue;


        /// <summary>
        /// 
        /// </summary>
        public Progress(Direction direction)
        {
            Direction = direction;

            IsOutlineVisible = false;
            m_OutlinePen = new Pen(Color.White);

            m_Brush = new SolidBrush(Color.White);

            ProgressPercentage = 0;

            OutLineChanged = OnOutLineChanged;
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            m_DrawRectangle = new Rectangle(Location, Size);
        }


        private void RefreshDrawRectangle()
        {
            switch (Direction)
            {
                case Direction.Up:
                    m_DrawRectangle.Y = (int)(Location.Y - MaxDrawValue * m_ProgressPercentage);
                    m_DrawRectangle.Height = (int)(MaxDrawValue * m_ProgressPercentage);
                    break;
                case Direction.Down:
                    m_DrawRectangle.Y = (int)(Location.Y + MaxDrawValue * m_ProgressPercentage);
                    m_DrawRectangle.Height = (int)(MaxDrawValue * m_ProgressPercentage);
                    break;
                case Direction.Left:
                    m_DrawRectangle.X = (int)(Location.X - MaxDrawValue * m_ProgressPercentage);
                    m_DrawRectangle.Width = (int)(MaxDrawValue * m_ProgressPercentage);
                    break;
                case Direction.Right:
                    m_DrawRectangle.X = (int)(Location.X + MaxDrawValue * m_ProgressPercentage);
                    m_DrawRectangle.Width = (int)(MaxDrawValue * m_ProgressPercentage);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            //Refresh();

            g.FillRectangle(m_Brush, m_DrawRectangle);

            if (IsOutlineVisible)
            {
                g.DrawRectangle(m_OutlinePen, m_DrawRectangle);
            }
        }
    }
}
