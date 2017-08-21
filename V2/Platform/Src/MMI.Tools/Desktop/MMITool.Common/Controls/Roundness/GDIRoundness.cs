using System;
using System.Drawing;
using MMITool.Common.Util;

namespace MMITool.Common.Controls.Roundness
{
    /// <summary>
    /// 圆
    /// </summary>
    public class GDIRoundness : CommonInnerControlBase
    {
        private int m_R;
        private Point m_Center;

        
        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// m_ArcPen
        /// </summary>
        protected Pen m_ArcPen ;
        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// SolidBrush
        /// </summary>
        protected SolidBrush m_ContentBrush;

        /// <summary>
        /// NeedDrawArc 变化 
        /// </summary>
        public EventHandler NeedDrawArcChanged;

        /// <summary>
        /// ArcColor 变化
        /// </summary>
        public EventHandler ArcColorChanged;

        /// <summary>
        /// ContentColor Changed
        /// </summary>
        public EventHandler ContentColorChanged;

        /// <summary>
        /// NeedDrawContent 变化  
        /// </summary>
        public EventHandler NeedDrawContentChanged;

        private bool m_NeedDrawArc;
        private bool m_NeedDrawContent;
        private Color m_ArcColor;
        private Color m_ContentColor;

        /// <summary>
        /// 半径
        /// </summary>
        public int R
        {
            set
            {
                m_R = value;
                SetOutLine(new Rectangle(Center.X - m_R, Center.Y - m_R, m_R * 2, m_R * 2));
            }
            get { return m_R; }
        }

        /// <summary>
        /// 圆心
        /// </summary>
        public Point Center
        {
            set
            {
                m_Center = value;
                SetOutLine(new Rectangle(Center.X - m_R, Center.Y - m_R, m_R * 2, m_R * 2));
            }
            get { return m_Center; }
        }

        /// <summary>
        /// 弧线的颜色, 默认白色
        /// </summary>
        public Color ArcColor
        {
            set
            {
                m_ArcColor = value;
                if (!m_ArcColor.Equals(m_ArcPen.Color))
                {
                    HandleUtil.OnHandle(ArcColorChanged, this, new ColorChangedEventArgs(m_ArcPen.Color, value));
                }
                m_ArcPen.Color = value;
                
            }
            get { return m_ArcPen.Color; }
        }

        /// <summary>
        /// 中间的颜色, 默认黑色
        /// </summary>
        public Color ContentColor
        {
            set
            {
                m_ContentColor = value;
                if (!m_ContentColor.Equals(m_ContentBrush.Color))
                {
                    HandleUtil.OnHandle(ContentColorChanged, this, new ColorChangedEventArgs(m_ContentBrush.Color, value));
                }
                m_ContentBrush.Color = value;
            }
            get { return m_ContentBrush.Color; }
        }

        /// <summary>
        /// 是否需要绘制弧线
        /// </summary>
        public bool NeedDrawArc
        {
            set
            {
                m_NeedDrawArc = value;
                HandleUtil.OnHandle(NeedDrawArcChanged, this, null);
            }
            get { return m_NeedDrawArc; }
        }

        /// <summary>
        /// 是否需要绘制中间的
        /// </summary>
        public bool NeedDrawContent
        {
            set
            {
                m_NeedDrawContent = value;
                HandleUtil.OnHandle(NeedDrawContentChanged, this, null);
            }
            get { return m_NeedDrawContent; }
        }

        /// <summary>
        /// 控件的形为
        /// </summary>
        public IBehavierStratege<GDIRoundness> BehavierStratege { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public GDIRoundness()
        {
            m_ArcPen= new Pen(Color.White);
            m_ContentBrush=new SolidBrush(Color.Black);
            BehavierStratege = new GDIRoundnessNormalBehavier(this);
            OutLineChanged = OnOutLineChanged;
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            if (Size.Width != Size.Height)
            {
                throw new Exception("Can not set size where Size.Width != Size.Height");
            }

            R = Size.Width / 2;
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="point"></param>
        public override bool OnMouseDown(Point point)
        {
            return BehavierStratege.OnMouseDown(point);
        }

        /// <summary>
        /// 鼠标按下后弹起
        /// </summary>
        /// <param name="point"></param>
        public override bool OnMouseUp(Point point)
        {
            return BehavierStratege.OnMouseUp(point);
        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            BehavierStratege.OnDraw(g);
        }

        /// <summary>
        /// 是否包含一个点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool Contains(Point point)
        {
            return OutLineRectangle.Contains(point);
        }

        /// <summary>
        /// 是否包含一个点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override bool Contains(int x, int y)
        {
            return OutLineRectangle.Contains(new Point(x, y));
        }

        /// <summary>
        /// 绘圆弧
        /// </summary>
        /// <param name="g"></param>
        public void DrawArc(Graphics g)
        {
            g.DrawArc(m_ArcPen, OutLineRectangle, 0, 360);
        }

        /// <summary>
        /// 填充中间
        /// </summary>
        /// <param name="g"></param>
        public void DrawContent(Graphics g)
        {
            g.FillPie(m_ContentBrush, OutLineRectangle, 0, 360);
        }
    }
}
