using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Subway.ATC.Casco.Common
{
    /// <summary>
    /// ����
    /// </summary>
    public class Dial
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="���ĵ�"></param>
        /// <param name="�̶�"></param>
        /// <param name="�����߶���"></param>
        /// <param name="ÿ�����ĽǶ�"></param>
        /// <param name="��ʼ�Ƕ�"></param>
        /// <param name="�뾶"></param>
        /// <param name="���̶�"></param>
        /// <param name="�̶̿�"></param>
        /// <param name="���̶̿ȱ�"></param>
        /// <param name="�̶ȿ��"></param>
        /// <param name="�̶ȿ�"></param>
        /// <param name="�̶�������ɫ"></param>
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

                    rectKedu[j].X = point.X + (�뾶 - ���̶� - 30) * scale * cosAngle;
                    rectKedu[j].Y = point.Y + (�뾶 - ���̶� - 30) * scale * sinAngle;
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
            e.SmoothingMode = SmoothingMode.AntiAlias;
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
}