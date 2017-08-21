using System.Drawing;
using System.Drawing.Drawing2D;

namespace Subway.ATC.Casco.Common
{
    /// <summary>
    /// 速度指针
    /// </summary>
    public class SpeedPointer
    {
        /// <summary>
        /// 原始图片
        /// </summary>
        private Image originalPic;

        /// <summary>
        /// 需要绘制的图片
        /// </summary>
        private Image drawPic;

        /// <summary>
        /// 需要画的最大角度
        /// </summary>
        private float dialAnglev;

        /// <summary>
        /// 指针初始角度
        /// </summary>
        private float initalAnglev;

        /// <summary>
        /// 指针最大值
        /// </summary>
        private float maxSpeed;

        /// <summary>
        /// 绘图顶点
        /// </summary>
        private PointF topPoint;

        /// <summary>
        /// 绘图中心点
        /// </summary>
        private PointF centralPoint;

        private Matrix matrix;

        /// <summary>
        /// 转动角度
        /// </summary>
        private float anglev = 0;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="maxAnglev"></param>
        /// <param name="initAnglev"></param>
        /// <param name="maxValue"></param>
        /// <param name="apexPoint"></param>
        /// <param name="centrePoint"></param>
        public SpeedPointer(float maxAnglev, float initAnglev, float maxValue, PointF apexPoint, PointF centrePoint)
        {
            dialAnglev = maxAnglev;
            initalAnglev = initAnglev;
            maxSpeed = maxValue;
            topPoint = apexPoint;
            centralPoint = centrePoint;
        }

        /// <summary>
        /// 绘指针
        /// </summary>
        /// <param name="g"></param>
        /// <param name="speed"></param>
        public void PaintPointer(Graphics g, Image tmpPic, float speed)
        {
            if (speed <= maxSpeed)
            {
                anglev = speed * dialAnglev / maxSpeed + initalAnglev;
            }
            var old = g.Transform.Clone();

            matrix = g.Transform;
            matrix.RotateAt(anglev, centralPoint);
            g.Transform = matrix;
            g.DrawImage(tmpPic, topPoint);
            matrix.Reset();
            g.Transform = old;
        }
    }
}