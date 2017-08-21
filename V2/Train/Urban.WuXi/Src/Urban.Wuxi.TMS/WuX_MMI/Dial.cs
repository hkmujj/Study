using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.DataType;
using System.Threading;

using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace KumM_MMI
{
    public class Dial
    {
        public Dial(PointF ���ĵ�, string[] �̶�, int �����߶���, 
            double ÿ�����ĽǶ�, double ��ʼ�Ƕ�, int �뾶, 
            int ���̶�, int �̶̿�, int ���̶̿ȱ�,
            Pen �̶ȿ��, Pen �̶ȿ�, SolidBrush �̶�������ɫ)
        {
            drawFormat.Alignment = (StringAlignment)1;
            drawFormat.LineAlignment = (StringAlignment)1;

            point = ���ĵ�;

            scaleStr = �̶�;
            rectKedu = new PointF[�̶�.Length];

            pointKedu1 = new PointF[�����߶���];
            pointKedu2 = new PointF[�����߶���];
            numb = �����߶���;

            minRadian = ÿ�����ĽǶ�;

            initRadian = ��ʼ�Ƕ�;

            bili = ���̶̿ȱ�;

            penKeduShort = �̶ȿ��;
            penKeduLong = �̶ȿ�;

            textbrush = �̶�������ɫ;

            DialInit(�뾶, ���̶�, �̶̿�);

        }

        /// <summary>
        /// �̶��㷨
        /// </summary>
        private void DialInit(int �뾶, int ���̶�, int �̶̿�)
        {
            double angle;
            float sinAngle;
            float cosAngle;

            int j = 0;
            for (int i = 0; i < numb; i++)
            {
                angle = (i * minRadian + initRadian) * Math.PI / 180.0;
                sinAngle = (float)Math.Sin(angle);
                cosAngle = (float)Math.Cos(angle);
                if (i % bili == 0)
                {
                    pointKedu1[i].X = point.X + �뾶 * scale * cosAngle;
                    pointKedu1[i].Y = point.Y + �뾶 * scale * sinAngle;

                    pointKedu2[i].X = point.X + (�뾶 - ���̶�) * scale * cosAngle;
                    pointKedu2[i].Y = point.Y + (�뾶 - ���̶�) * scale * sinAngle;

                    rectKedu[j].X = point.X + (�뾶 - ���̶� - 15) * scale * cosAngle;
                    rectKedu[j].Y = point.Y + (�뾶 - ���̶� - 15) * scale * sinAngle;
                    j++;
                }
                else
                {
                    pointKedu1[i].X = point.X + �뾶 * scale * cosAngle;
                    pointKedu1[i].Y = point.Y + �뾶 * scale * sinAngle;

                    pointKedu2[i].X = point.X + (�뾶 - �̶̿�) * scale * cosAngle;
                    pointKedu2[i].Y = point.Y + (�뾶 - �̶̿�) * scale * sinAngle;
                }
            }
        }

        /// <summary>
        /// ���̻���
        /// </summary>
        /// <param name="e"></param>
        public void OnDraw(Graphics e, Font font)
        {
            e.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = 0; i < numb; i++)
            {
                if (i % bili == 0)
                {
                    e.DrawLine(penKeduLong, pointKedu1[i], pointKedu2[i]);
                }
                else
                {

                    e.DrawLine(penKeduShort, pointKedu1[i], pointKedu2[i]);
                }
            }
            for (int i = 0; i < scaleStr.Length; i++)
            {
                e.DrawString(scaleStr[i], font, textbrush, rectKedu[i], drawFormat);
            }

        }

        #region ::::::::::::::::::::::: init :::::::::::::::::::::::::::
        /// <summary>
        /// �Ŵ���Сϵ��
        /// </summary>
        internal const float scale = 1.0f;

        /// <summary>
        /// �̶ȵ�1
        /// </summary>
        public PointF[] pointKedu1;

        /// <summary>
        /// �̶ȵ�2
        /// </summary>
        public PointF[] pointKedu2;

        /// <summary>
        /// 1���ٶȴ���Ļ���
        /// </summary>
        internal double minRadian;

        /// <summary>
        /// ��ʼ����
        /// </summary>
        internal double initRadian;

        /// <summary>
        /// �����߶�����
        /// </summary>
        int numb = 0;

        /// <summary>
        /// �߶δ��������
        /// </summary>
        private string[] scaleStr;

        /// <summary>
        /// �����ϵ�
        /// </summary>
        public string numbstr;

        /// <summary>
        /// �������ĵ�
        /// </summary>
        public PointF point = new PointF();

        /// <summary>
        /// �̶�λ��
        /// </summary>
        public PointF[] rectKedu;

        /// <summary>
        /// �̶ȿ��
        /// </summary>
        internal Pen penKeduShort;

        /// <summary>
        /// �̶ȿ�
        /// </summary>
        internal Pen penKeduLong;

        /// <summary>
        /// ������ɫ
        /// </summary>
        public SolidBrush textbrush;

        /// <summary>
        /// �̶ȳ��̱������̣���
        /// </summary>
        private int bili;

        StringFormat drawFormat = new StringFormat();
        #endregion
    }


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
