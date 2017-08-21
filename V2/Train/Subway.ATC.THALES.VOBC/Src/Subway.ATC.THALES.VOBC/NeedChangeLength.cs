using System.Drawing;

namespace Subway.ATC.THALES.VOBC
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
        public static SolidBrush yellowBrush = new SolidBrush(Color.FromArgb(0, 204, 255));

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

        private float[] scales;
        private float[] values;

        private int index = 0;
        private PointF currentPoint;
        private float currentHigh;
        private Pen p;

        public NeedChangeLength(PointF point, int width, float dizeng, float scale)
        {
            drawPoint = point;
            rectWidth = width;
            tmpStepLength = dizeng;
            rectScale = scale;
        }

        public NeedChangeLength(PointF point, int width, float dizeng, float[] scale, float[] values)
        {
            drawPoint = point;
            currentPoint = point;
            //currentHigh = point.Y;
            rectWidth = width;
            tmpStepLength = dizeng;
            scales = scale;
            this.values = values;

            p = new Pen(new SolidBrush(Color.FromArgb(255, 0, 255)), 5);
        }

        public float CurrentValue = 0;
        public void Draw(Graphics e, ref float currentValue)
        {
            if (ViewValue < currentValue)
            {
                if (ViewValue + scales[index] <= currentValue)
                {
                    ViewValue += scales[index];
                    //ViewValue = this.values[index+1];
                    index++;
                    currentHigh += tmpStepLength;
                    e.FillRectangle(yellowBrush, new RectangleF(new PointF(drawPoint.X, drawPoint.Y - currentHigh), new SizeF(rectWidth, currentHigh)));
                }
                else
                {
                    tmpNeedChangeLength = (currentValue - ViewValue) * (tmpStepLength / scales[index]);
                    ViewValue = currentValue;
                }
            }
        }

        /// <summary>
        /// 绘制纵条
        /// </summary>
        /// <param name="e"></param>
        /// <param name="currentValue"></param>
        /// <param name="drawType"></param>
        public void DrawRectangle(Graphics e, ref float currentValue, int drawType)
        {
            if (currentValue > 1000)
            {
                currentValue = 1000;
            }

            if (currentValue < 0)
            {
                return;
            }

            while (ViewValue != currentValue)
            {
                if (ViewValue > currentValue)
                {
                    if (currentValue >= values[index])
                    {
                        tmpNeedChangeLength = -(ViewValue - currentValue) * (tmpStepLength / scales[index]);
                        ViewValue = currentValue;
                    }
                    else
                    {
                        tmpNeedChangeLength = -(ViewValue - values[index]) * (tmpStepLength / scales[index]);
                        ViewValue = values[index];
                        if (index != 0)
                        {
                            index--;
                        }
                    }
                }
                else if (ViewValue < currentValue)//上升
                {
                    if (index == values.Length - 1)
                    {
                        break;
                    }

                    if (currentValue < values[index + 1])
                    {
                        tmpNeedChangeLength = (currentValue - ViewValue) * (tmpStepLength / scales[index]);
                        ViewValue = currentValue;
                    }
                    else
                    {
                        tmpNeedChangeLength = (values[index + 1] - ViewValue) * (tmpStepLength / scales[index]); ;
                        ViewValue = values[index + 1];
                        if (index != 9)
                        {
                            index++;
                        }
                    }
                }
                else
                {
                    tmpNeedChangeLength = 0;
                }
                currentHigh += tmpNeedChangeLength;
            }

            switch (drawType)
            {
                case 1://横左
                    e.FillRectangle(yellowBrush, new RectangleF(new PointF(drawPoint.X - ViewValue * rectScale, drawPoint.Y), new SizeF(ViewValue * rectScale, rectWidth)));
                    break;
                case 2://横右
                    e.FillRectangle(yellowBrush, new RectangleF(drawPoint, new SizeF(ViewValue * rectScale, rectWidth)));
                    break;
                case 3://纵上
                    e.FillRectangle(yellowBrush, new RectangleF(new PointF(drawPoint.X, drawPoint.Y - currentHigh), new SizeF(rectWidth, currentHigh)));
                    e.DrawLine(
                        p,
                        new PointF(drawPoint.X - 6, drawPoint.Y - currentHigh - 2),
                        new PointF(drawPoint.X - 4 + 50, drawPoint.Y - currentHigh - 2)
                        );
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