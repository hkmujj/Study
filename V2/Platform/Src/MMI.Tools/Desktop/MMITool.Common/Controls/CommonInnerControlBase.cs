using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using MMITool.Common.Model;
using MMITool.Common.Util;

namespace MMITool.Common.Controls
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
        /// ��С �仯�ľ���
        /// </summary>
        public Matrix SizeChangeMatrix { private set; get; }

        /// <summary>
        /// �Ƿ�ɼ�
        /// </summary>
        public bool Visible { set; get; }

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
                LocationChangeMatrix.Translate(value.X - old.X, value.Y - old.Y);
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
                if (old.Width != 0 && old.Height != 0)
                {
                    SizeChangeMatrix.Scale((float)value.Width / old.Width, (float)value.Height / old.Height);
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
        /// ˢ��, ���� RefreshAction
        /// </summary>
        public void Refresh()
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
                LocationChangeMatrix.Translate(value.X - old.X, value.Y - old.Y);
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