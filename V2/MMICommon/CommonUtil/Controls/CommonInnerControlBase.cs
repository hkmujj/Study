using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using CommonUtil.Model;
using CommonUtil.Util;

namespace CommonUtil.Controls
{
    /// <summary>
    /// ICommonInnerControl �Ļ���, �ṩ��С�����÷���
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
        /// ˢ�µ�,
        /// object = this
        /// </summary>
        public Action<object> RefreshAction { set; get; }

        /// <summary>
        /// Location �仯�ľ���
        /// </summary>
        public Matrix LocationChangeMatrix { private set; get; }

        /// <summary>
        /// location ��ƽ����
        /// </summary>
        public PointF LocationOffset { private set; get; }

        /// <summary>
        /// ��С �仯�ľ���
        /// </summary>
        public Matrix SizeChangeMatrix { private set; get; }

        /// <summary>
        /// ��С�仯�ı���
        /// </summary>
        public SizeF SizeChangedMultiple { private set; get; }

        /// <summary>
        /// �Ƿ�ɼ�
        /// </summary>
        public virtual bool Visible { set; get; }

        /// <summary>
        /// �Ƿ����
        /// </summary>
        public virtual bool IsEnable { set; get; }

        /// <summary>
        /// ��ȡ�����ð����йؿؼ������ݵĶ���
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// ��ʼ��, ����λ�� 
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
        /// ��С 
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
        /// ��ת����
        /// </summary>
        public virtual void Reverse()
        {
            
        }

        /// <summary>
        /// ƽ��
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public virtual void Translate(float offsetX, float offsetY)
        {
            m_OutLineRectangle.Offset((int) Math.Ceiling(offsetX), (int) Math.Ceiling(offsetY));

            //LogMgr.Warn(string.Format("Can not Translate by offsetX and offsetY, the class {0} is NotImplemented the method !!!", GetType().FullName));
        }

        /// <summary>
        /// ƽ��
        /// </summary>
        /// <param name="offset"></param>
        public void Translate(PointF offset)
        {
            Translate(offset.X, offset.Y);
        }

        /// <summary>
        /// ����  �Ŵ�ָ������
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public virtual void Inflate(float width, float height)
        {
            m_OutLineRectangle.Inflate((int)Math.Ceiling(width), (int)Math.Ceiling(height));

            //LogMgr.Warn(string.Format("Can not Transform by width and height, the class {0} is NotImplemented the method !!!", GetType().FullName));
        }

        /// <summary>
        /// ���� �Ŵ�ָ������
        /// </summary>
        /// <param name="size"></param>
        public void Inflate(Size size)
        {
            Inflate(size.Width, size.Height);
            //LogMgr.Warn(string.Format("Can not Inflate by Size, the class {0} is NotImplemented the method !!!", GetType().FullName));
        }

        /// <summary>
        /// ˢ��, ���� RefreshAction
        /// </summary>
        public virtual void Refresh()
        {
            if (RefreshAction != null)
            {
                RefreshAction(this);
            }
        }



        /// <summary>
        /// ��ʼ��
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        /// ��갴��
        /// </summary>
        /// <param name="point"></param>
        public virtual bool OnMouseDown(Point point)
        {
            return false;
        }

        /// <summary>
        /// ��갴�º���
        /// </summary>
        /// <param name="point"></param>
        public virtual bool OnMouseUp(Point point)
        {
            return false;
        }

        /// <summary>
        /// ��ͼ
        /// </summary>
        /// <param name="g"></param>
        public virtual void OnDraw(Graphics g)
        {
        }

        /// <summary>
        /// ˢ�²���ͼ, ����� Refresh
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
        /// �Ƿ����һ����
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual bool Contains(Point point)
        {
            return m_OutLineRectangle.Contains(point);
        }

        /// <summary>
        /// �Ƿ����һ����
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public virtual bool Contains(int x, int y)
        {
            return m_OutLineRectangle.Contains(x, y);
        }

        /// <summary>
        /// ������С
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
        /// OutLineRectangle �仯 
        /// </summary>
        protected EventHandler OutLineChanged;

        /// <summary>
        /// ��������, ������ OutLineChanged �¼�
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