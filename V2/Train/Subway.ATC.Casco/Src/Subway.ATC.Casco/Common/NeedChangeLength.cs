using System.Drawing;

namespace Subway.ATC.Casco.Common
{
    /// <summary>
    /// 变化矩形条
    /// </summary>
    public class NeedChangeLength
    {
        /// <summary>
        /// 当前要画的高度值
        /// </summary>
        private float ViewValue = 0;

        /// <summary>
        /// 需要增加的高度量
        /// </summary>
        private float tmpNeedChangeLength = 0;

        /// <summary>
        /// 递增量
        /// </summary>
        private float tmpStepLength = 5;

        /// <summary>
        /// 黄色画笔
        /// </summary>
        public static SolidBrush yellowBrush = new SolidBrush(Color.Yellow);

        /// <summary>
        /// 绘图起点
        /// </summary>
        PointF drawPoint;

        /// <summary>
        /// 进度条宽度
        /// </summary>
        private int rectWidth;

        /// <summary>
        /// 对应比例
        /// </summary>
        private float rectScale;

        public NeedChangeLength(PointF point, int width, float dizeng, float scale)
        {
            drawPoint = point;
            rectWidth = width;
            tmpStepLength = dizeng;
            rectScale = scale;
        }

        /// <summary>
        /// 绘制纵条
        /// </summary>
        /// <param name="e"></param>
        /// <param name="currentValue"></param>
        /// <param name="drawType"></param>
        public void DrawRectangle(Graphics e, ref float currentValue, int drawType)
        {
            if (ViewValue > currentValue)
            {
                if (ViewValue - tmpStepLength >= currentValue)
                {
                    tmpNeedChangeLength = -tmpStepLength;
                }
                else
                {
                    tmpNeedChangeLength = currentValue - ViewValue;
                }
            }
            else if (ViewValue < currentValue)
            {
                if (ViewValue + tmpStepLength <= currentValue)
                {
                    tmpNeedChangeLength = tmpStepLength;
                }
                else
                {
                    tmpNeedChangeLength = currentValue - ViewValue;
                }
            }
            else
            {
                tmpNeedChangeLength = 0;
            }
            ViewValue += tmpNeedChangeLength;

            switch (drawType)
            {
                case 1://横左
                    e.FillRectangle(yellowBrush, new RectangleF(new PointF(drawPoint.X - ViewValue * rectScale, drawPoint.Y), new SizeF(ViewValue * rectScale, rectWidth)));
                    break;
                case 2://横右
                    e.FillRectangle(yellowBrush, new RectangleF(drawPoint, new SizeF(ViewValue * rectScale, rectWidth)));
                    break;
                case 3://纵上
                    e.FillRectangle(yellowBrush, new RectangleF(new PointF(drawPoint.X, drawPoint.Y - ViewValue * rectScale), new SizeF(rectWidth, ViewValue * rectScale)));
                    break;
                case 4://纵下
                    e.FillRectangle(yellowBrush, new RectangleF(drawPoint, new SizeF(rectWidth, ViewValue * rectScale)));
                    break;
                default:
                    break;
            }

        }
    }
}