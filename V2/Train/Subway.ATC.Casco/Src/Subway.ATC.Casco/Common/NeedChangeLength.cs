using System.Drawing;

namespace Subway.ATC.Casco.Common
{
    /// <summary>
    /// �仯������
    /// </summary>
    public class NeedChangeLength
    {
        /// <summary>
        /// ��ǰҪ���ĸ߶�ֵ
        /// </summary>
        private float ViewValue = 0;

        /// <summary>
        /// ��Ҫ���ӵĸ߶���
        /// </summary>
        private float tmpNeedChangeLength = 0;

        /// <summary>
        /// ������
        /// </summary>
        private float tmpStepLength = 5;

        /// <summary>
        /// ��ɫ����
        /// </summary>
        public static SolidBrush yellowBrush = new SolidBrush(Color.Yellow);

        /// <summary>
        /// ��ͼ���
        /// </summary>
        PointF drawPoint;

        /// <summary>
        /// ���������
        /// </summary>
        private int rectWidth;

        /// <summary>
        /// ��Ӧ����
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
        /// ��������
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
                case 1://����
                    e.FillRectangle(yellowBrush, new RectangleF(new PointF(drawPoint.X - ViewValue * rectScale, drawPoint.Y), new SizeF(ViewValue * rectScale, rectWidth)));
                    break;
                case 2://����
                    e.FillRectangle(yellowBrush, new RectangleF(drawPoint, new SizeF(ViewValue * rectScale, rectWidth)));
                    break;
                case 3://����
                    e.FillRectangle(yellowBrush, new RectangleF(new PointF(drawPoint.X, drawPoint.Y - ViewValue * rectScale), new SizeF(rectWidth, ViewValue * rectScale)));
                    break;
                case 4://����
                    e.FillRectangle(yellowBrush, new RectangleF(drawPoint, new SizeF(rectWidth, ViewValue * rectScale)));
                    break;
                default:
                    break;
            }

        }
    }
}