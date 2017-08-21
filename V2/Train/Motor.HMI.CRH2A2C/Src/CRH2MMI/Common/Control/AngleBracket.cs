using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using CommonUtil.Controls;

namespace CRH2MMI.Common.Control
{
    /// <summary>
    /// 尖括号
    /// </summary>
    public class AngleBracket : CommonInnerControlBase
    {
        /// <summary>
        /// 尖角方向
        /// </summary>
        public double AngleOrientation { get; set; }


        /// <summary>
        /// 张开角度
        /// </summary>
        public double AngleSplay { get; set; }

        public Pen Pen { set; get; }

        private readonly GraphicsPath m_GraphicsPath;

        public AngleBracket()
        {
            Pen = new Pen(Color.White, 2);

            m_GraphicsPath = new GraphicsPath();

            AngleOrientation = -Math.PI;

        }

        public void Generate()
        {
            var points = new PointF[3];
            points[0] = new PointF(-Size.Width/ 2f, (float)(Math.Sin(AngleSplay / 2) * Size.Height / 2));
            points[1] = new PointF(Size.Width/2f, 0);
            points[2] = new PointF(-Size.Width / 2f, -(float)(Math.Sin(AngleSplay / 2) * Size.Height / 2));

            var mat = new Matrix();
            mat.Rotate((float)(180 * AngleOrientation / Math.PI));
            mat.Translate(Size.Width / 2f + Location.X, Size.Height / 2f + Location.Y, MatrixOrder.Append);

            m_GraphicsPath.Reset();
            m_GraphicsPath.AddLine(points[0], points[1]);
            if (Math.Abs(AngleSplay) > double.Epsilon)
            {
                m_GraphicsPath.AddLine(points[1], points[2]);
            }
            else
            {
                //  偏下
                mat.Translate(0, Size.Height/3f, MatrixOrder.Append);
            }


            m_GraphicsPath.Transform(mat);
        }

        public override void OnDraw(Graphics g)
        {
            g.DrawPath(Pen, m_GraphicsPath);
        }
    }
}
