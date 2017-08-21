using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Motor.ATP.CommonResource;
using Motor.ATP.CommonView.Model;
using Motor.ATP.CommonView.Utils.Extensions;

namespace Motor.ATP.CommonView.View.RegionE
{
    public partial class ArrowControl : PictureBox
    {
        private ArrowDirection m_ArrowDirection;
        private ArrowState m_ArrowState;

        private Matrix m_Matrix;
        private Point m_MidlePoint;
        private readonly Action<Image> m_UpdateImageAction;

        public ArrowDirection ArrowDirection
        {
            set
            {
                m_ArrowDirection = value;
                UpdateMatrix();
                Invalidate();
            }
            get { return m_ArrowDirection; }
        }

        public ArrowState ArrowState
        {
            set
            {
                m_ArrowState = value;
                UpdateImage();
                Invalidate();
            }
            get { return m_ArrowState; }
        }

        public ArrowControl()
        {
            InitializeComponent();
            m_Matrix = new Matrix();
            m_MidlePoint = new Point(Width / 2, Height / 2);
            SizeChanged += OnSizeChanged;
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            m_UpdateImageAction = new Action<Image>((image) => Image = image);
        }

        private void OnSizeChanged(object sender, EventArgs eventArgs)
        {
            m_MidlePoint = new Point( Width / 2,Height / 2);
            UpdateMatrix();
        }

        private void UpdateImage()
        {
            Image img = null;
            switch (ArrowState)
            {
                case ArrowState.Useable:
                    img = ATPCommonResource.箭头_上_亮;
                    break;
                case ArrowState.Unuseable:
                    img = ATPCommonResource.箭头_上_暗;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Image = img;
            this.InvokeIfNeed(m_UpdateImageAction, img);
        }

        private void UpdateMatrix()
        {
            switch (ArrowDirection)
            {
                case ArrowDirection.Up:
                    break;
                case ArrowDirection.Down:
                    m_Matrix = new Matrix();
                    m_Matrix.RotateAt(180, m_MidlePoint);
                    break;
                case ArrowDirection.Left:
                    break;
                case ArrowDirection.Right:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            var old = pe.Graphics.Transform;
            pe.Graphics.Transform = m_Matrix;
            base.OnPaint(pe);
            pe.Graphics.Transform = old;
        }
    }
}
