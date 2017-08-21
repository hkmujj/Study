using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using CommonUtil.Model;
using CommonUtil.Util;

namespace CommonUtil.Controls
{
    /// <summary>
    /// ICommonInnerControl 的基类, 提供大小的设置方法
    /// </summary>
    [Serializable]
    public abstract class CommonInnerControlBase : ICommonInnerControl
    {
        /// <summary>
        /// m_OutLineRectangle
        /// </summary>
        // ReSharper disable once InconsistentNaming
        protected Rectangle m_OutLineRectangle;

        /// <summary>
        /// 刷新的,
        /// object = this
        /// </summary>
        public Action<object> RefreshAction { set; get; }

        /// <summary>
        /// Location 变化的矩阵
        /// </summary>
        public Matrix LocationChangeMatrix { private set; get; }

        /// <summary>
        /// location 的平移量
        /// </summary>
        public PointF LocationOffset { private set; get; }

        /// <summary>
        /// 大小 变化的矩阵
        /// </summary>
        public Matrix SizeChangeMatrix { private set; get; }

        /// <summary>
        /// 大小变化的倍数
        /// </summary>
        public SizeF SizeChangedMultiple { private set; get; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public virtual bool Visible { set; get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public virtual bool IsEnable { set; get; }

        /// <summary>
        /// 获取或设置包含有关控件的数据的对象。
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// 起始点, 绝对位置 
        /// </summary>
        public virtual Point Location
        {
            get
            {
                return m_OutLineRectangle.Location;

            }
            set
            {
                var old = new Rectangle(m_OutLineRectangle.Location, m_OutLineRectangle.Size);
                LocationChangeMatrix = new Matrix();
                LocationOffset = new PointF(value.X - old.X, value.Y - old.Y);
                LocationChangeMatrix.Translate(LocationOffset.X, LocationOffset.Y);
                m_OutLineRectangle.Location = value;
                HandleUtil.OnHandle(OutLineChanged, this, new OutLineChangedEventArgs<Rectangle>(OutLineChangedType.Location, old, new Rectangle(m_OutLineRectangle.Location, m_OutLineRectangle.Size)));
            }
        }

        /// <summary>
        /// 大小 
        /// </summary>
        public virtual Size Size
        {
            get { return m_OutLineRectangle.Size; }
            set
            {
                var old = new Rectangle(m_OutLineRectangle.Location, m_OutLineRectangle.Size);
                SizeChangeMatrix = new Matrix();
                SizeChangedMultiple = new SizeF((float)value.Width / old.Width, (float)value.Height / old.Height);
                if (old.Width != 0 && old.Height != 0)
                {
                    SizeChangeMatrix.Scale(SizeChangedMultiple.Width, SizeChangedMultiple.Height);
                }
                m_OutLineRectangle.Size = value;
                HandleUtil.OnHandle(OutLineChanged, this, new OutLineChangedEventArgs<Rectangle>(OutLineChangedType.Size, old, new Rectangle(m_OutLineRectangle.Location, m_OutLineRectangle.Size)));

            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected CommonInnerControlBase()
        {
            m_OutLineRectangle = new Rectangle();
            Visible = true;
        }

        /// <summary>
        /// 反转自身
        /// </summary>
        public virtual void Reverse()
        {
            
        }

        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public virtual void Translate(float offsetX, float offsetY)
        {
            m_OutLineRectangle.Offset((int) Math.Ceiling(offsetX), (int) Math.Ceiling(offsetY));

            //LogMgr.Warn(string.Format("Can not Translate by offsetX and offsetY, the class {0} is NotImplemented the method !!!", GetType().FullName));
        }

        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="offset"></param>
        public void Translate(PointF offset)
        {
            Translate(offset.X, offset.Y);
        }

        /// <summary>
        /// 将此  放大指定量。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public virtual void Inflate(float width, float height)
        {
            m_OutLineRectangle.Inflate((int)Math.Ceiling(width), (int)Math.Ceiling(height));

            //LogMgr.Warn(string.Format("Can not Transform by width and height, the class {0} is NotImplemented the method !!!", GetType().FullName));
        }

        /// <summary>
        /// 将此 放大指定量。
        /// </summary>
        /// <param name="size"></param>
        public void Inflate(Size size)
        {
            Inflate(size.Width, size.Height);
            //LogMgr.Warn(string.Format("Can not Inflate by Size, the class {0} is NotImplemented the method !!!", GetType().FullName));
        }

        /// <summary>
        /// 刷新, 调用 RefreshAction
        /// </summary>
        public virtual void Refresh()
        {
            if (RefreshAction != null)
            {
                RefreshAction(this);
            }
        }



        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="point"></param>
        public virtual bool OnMouseDown(Point point)
        {
            return false;
        }

        /// <summary>
        /// 鼠标按下后弹起
        /// </summary>
        /// <param name="point"></param>
        public virtual bool OnMouseUp(Point point)
        {
            return false;
        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="g"></param>
        public virtual void OnDraw(Graphics g)
        {
        }

        /// <summary>
        /// 刷新并绘图, 会调用 Refresh
        /// </summary>
        /// <param name="g"></param>
        public virtual void OnPaint(Graphics g)
        {
            Refresh();

            if (Visible)
            {
                OnDraw(g);
            }
        }

        /// <summary>
        /// 是否包含一个点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual bool Contains(Point point)
        {
            return m_OutLineRectangle.Contains(point);
        }

        /// <summary>
        /// 是否包含一个点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public virtual bool Contains(int x, int y)
        {
            return m_OutLineRectangle.Contains(x, y);
        }

        /// <summary>
        /// 轮廓大小
        /// </summary>
        public virtual Rectangle OutLineRectangle
        {
            get { return m_OutLineRectangle; }
            set
            {
                var old = new Rectangle(m_OutLineRectangle.Location, m_OutLineRectangle.Size);
                LocationChangeMatrix = new Matrix();
                LocationOffset = new PointF(value.X - old.X, value.Y - old.Y);
                LocationChangeMatrix.Translate(LocationOffset.X, LocationOffset.Y);
                SizeChangeMatrix = new Matrix();
                if (old.Width != 0 && old.Height != 0)
                {
                    SizeChangeMatrix.Scale((float)value.Width / old.Width, (float)value.Height / old.Height);
                }
                m_OutLineRectangle = value;
                HandleUtil.OnHandle(OutLineChanged, this, new OutLineChangedEventArgs<Rectangle>(OutLineChangedType.Outline, old, new Rectangle(m_OutLineRectangle.Location, m_OutLineRectangle.Size)));

            }
        }

        /// <summary>
        /// OutLineRectangle 变化 
        /// </summary>
        protected EventHandler OutLineChanged;

        /// <summary>
        /// 设置轮廓, 不产生 OutLineChanged 事件
        /// </summary>
        protected void SetOutLine(Rectangle outline)
        {
            m_OutLineRectangle.X = outline.X;
            m_OutLineRectangle.Y = outline.Y;
            m_OutLineRectangle.Width = outline.Width;
            m_OutLineRectangle.Height = outline.Height;
        }
    }
}