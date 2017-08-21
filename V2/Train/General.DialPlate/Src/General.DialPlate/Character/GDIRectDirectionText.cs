using System.Drawing;
using System.Drawing.Drawing2D;
using CommonUtil.Controls;

namespace General.DialPlate.Character
{
    public class GDIRectDirectionText : GDIRectText
    {
        private float m_RotateAngle;
        private PointF m_RotateCenterPoint;
        private Matrix m_RotateMatrix;

    

        /// <summary>
        /// 旋转角度
        /// </summary>
        public float RotateAngle
        {
            set { m_RotateAngle = value; UpdateRotateMatrix();}
            get { return m_RotateAngle; }
        }

        /// <summary>
        /// 旋转中心
        /// </summary>
        public PointF RotateCenterPoint
        {
            set { m_RotateCenterPoint = value; UpdateRotateMatrix(); }
            get { return m_RotateCenterPoint; }
        }

        public GDIRectDirectionText()
        {
            UpdateRotateMatrix();
        }

        private void UpdateRotateMatrix()
        {
            m_RotateMatrix = new Matrix();
            m_RotateMatrix.RotateAt(RotateAngle, RotateCenterPoint);
        }

        public override void OnDraw(Graphics g)
        {
            var old = g.Transform.Clone();

            g.Transform.Multiply(m_RotateMatrix, MatrixOrder.Append);

            g.Transform = m_RotateMatrix;

            base.OnDraw(g);

            g.Transform = old;
        }
    }
}