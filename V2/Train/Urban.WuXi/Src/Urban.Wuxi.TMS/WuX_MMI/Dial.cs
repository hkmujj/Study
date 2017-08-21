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
        public Dial(PointF 中心点, string[] 刻度, int 所有线段数, 
            double 每点代表的角度, double 初始角度, int 半径, 
            int 长刻度, int 短刻度, int 长短刻度比,
            Pen 刻度宽短, Pen 刻度宽长, SolidBrush 刻度字体颜色)
        {
            drawFormat.Alignment = (StringAlignment)1;
            drawFormat.LineAlignment = (StringAlignment)1;

            point = 中心点;

            scaleStr = 刻度;
            rectKedu = new PointF[刻度.Length];

            pointKedu1 = new PointF[所有线段数];
            pointKedu2 = new PointF[所有线段数];
            numb = 所有线段数;

            minRadian = 每点代表的角度;

            initRadian = 初始角度;

            bili = 长短刻度比;

            penKeduShort = 刻度宽短;
            penKeduLong = 刻度宽长;

            textbrush = 刻度字体颜色;

            DialInit(半径, 长刻度, 短刻度);

        }

        /// <summary>
        /// 刻度算法
        /// </summary>
        private void DialInit(int 半径, int 长刻度, int 短刻度)
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
                    pointKedu1[i].X = point.X + 半径 * scale * cosAngle;
                    pointKedu1[i].Y = point.Y + 半径 * scale * sinAngle;

                    pointKedu2[i].X = point.X + (半径 - 长刻度) * scale * cosAngle;
                    pointKedu2[i].Y = point.Y + (半径 - 长刻度) * scale * sinAngle;

                    rectKedu[j].X = point.X + (半径 - 长刻度 - 15) * scale * cosAngle;
                    rectKedu[j].Y = point.Y + (半径 - 长刻度 - 15) * scale * sinAngle;
                    j++;
                }
                else
                {
                    pointKedu1[i].X = point.X + 半径 * scale * cosAngle;
                    pointKedu1[i].Y = point.Y + 半径 * scale * sinAngle;

                    pointKedu2[i].X = point.X + (半径 - 短刻度) * scale * cosAngle;
                    pointKedu2[i].Y = point.Y + (半径 - 短刻度) * scale * sinAngle;
                }
            }
        }

        /// <summary>
        /// 表盘绘制
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
        /// 放大缩小系数
        /// </summary>
        internal const float scale = 1.0f;

        /// <summary>
        /// 刻度点1
        /// </summary>
        public PointF[] pointKedu1;

        /// <summary>
        /// 刻度点2
        /// </summary>
        public PointF[] pointKedu2;

        /// <summary>
        /// 1点速度代表的弧度
        /// </summary>
        internal double minRadian;

        /// <summary>
        /// 初始弧度
        /// </summary>
        internal double initRadian;

        /// <summary>
        /// 表盘线段总数
        /// </summary>
        int numb = 0;

        /// <summary>
        /// 线段代表的数量
        /// </summary>
        private string[] scaleStr;

        /// <summary>
        /// 表盘上的
        /// </summary>
        public string numbstr;

        /// <summary>
        /// 表盘中心点
        /// </summary>
        public PointF point = new PointF();

        /// <summary>
        /// 刻度位置
        /// </summary>
        public PointF[] rectKedu;

        /// <summary>
        /// 刻度宽短
        /// </summary>
        internal Pen penKeduShort;

        /// <summary>
        /// 刻度宽长
        /// </summary>
        internal Pen penKeduLong;

        /// <summary>
        /// 字体颜色
        /// </summary>
        public SolidBrush textbrush;

        /// <summary>
        /// 刻度长短比例，短：长
        /// </summary>
        private int bili;

        StringFormat drawFormat = new StringFormat();
        #endregion
    }


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
