using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.PublicUI;
using System.IO;

namespace NJ_MMI
{
    /// <summary>
    /// 速度指针
    /// </summary>
    public class SpeedPointer
    {
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
        public void PaintPointer(Graphics g, Image tmpPic, ref float speed)
        {
            if (speed <= maxSpeed)
            {
                anglev = speed * dialAnglev / maxSpeed + initalAnglev;
            }
            matrix = g.Transform;
            matrix.RotateAt(anglev, centralPoint);
            g.Transform = matrix;
            g.DrawImage(tmpPic, topPoint);
            matrix.Reset();
            g.Transform = matrix;
        }
    }

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

        private Int32 index = 0;
        private PointF currentPoint;
        private float currentHigh;

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
                    this.index++;
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

        private Int32 checkValue(float value)
        {
            Int32 temp = -1;
            for (int i = 0; i < this.values.Length - 1; i++)
            {
                if (this.values[i] <= value && value < this.values[i + 1])
                {
                    temp = i;
                }
            }

            return temp;
        }

        private Int32 _checkValue(float value)
        {
            Int32 temp = -1;
            for (int i = 0; i < this.values.Length - 1; i++)
            {
                if (this.values[i] < value && value <= this.values[i + 1])
                {
                    temp = i;
                }
            }

            return temp;
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
                currentValue = 1000;

            if (currentValue < 0)
                return;

            while (ViewValue != currentValue)
            {
                if (ViewValue > currentValue)
                {
                    if (currentValue >= this.values[index])
                    {
                        tmpNeedChangeLength = -(ViewValue - currentValue) * (tmpStepLength / scales[index]);
                        ViewValue = currentValue;
                    }
                    else
                    {
                        tmpNeedChangeLength = -(ViewValue - this.values[index]) * (tmpStepLength / scales[index]);
                        ViewValue = this.values[index];
                        if(index!=0)
                        index--;
                    }
                }
                else if (ViewValue < currentValue)//上升
                {
                    if (index == this.values.Length - 1) break;
                    if (currentValue < this.values[index + 1])
                    {
                        tmpNeedChangeLength = (currentValue - ViewValue) * (tmpStepLength / scales[index]);
                        ViewValue = currentValue;
                    }
                    else
                    {
                        tmpNeedChangeLength = (this.values[index + 1] - ViewValue) * (tmpStepLength / scales[index]); ;
                        ViewValue = this.values[index+1];
                        if(index!=9)
                        index++;
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
                    e.FillRectangle(yellowBrush, new RectangleF(new PointF(drawPoint.X, drawPoint.Y - currentHigh), new SizeF(rectWidth, currentHigh)));//ViewValue * rectScale
                    e.DrawLine(new Pen(new SolidBrush(Color.FromArgb(255, 0, 255)), 5), new PointF(drawPoint.X - 6, drawPoint.Y - currentHigh - 2), new PointF(drawPoint.X - 4 + 38, drawPoint.Y - currentHigh - 2));
                    break;
                case 4://纵下
                    e.FillRectangle(yellowBrush, new RectangleF(drawPoint, new SizeF(rectWidth, ViewValue * rectScale)));
                    break;
                default:
                    break;
            }
        }
    }

    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V1_C7_SpeedDial : baseClass
    {
        private SpeedPointer _speedPointer;
        private SpeedPointer _speedPointer_Target;
        private SpeedPointer _speedPointer_Max;
        private Image _pointer;
        private Image _pointer_TargetSpeed;
        private Image _pointer_MaxSpeed;
        private NeedChangeLength _targetPoint;

        public override string GetInfo()
        {
            return "速度表盘";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            _speedPointer = new SpeedPointer(270f, -45f, 90f, new PointF(160f, 91f), new PointF(315.5f, 243f));//270f, -45f, 120f, new PointF(160f, 91f), new PointF(313f, 243f)
            _speedPointer_Target = new SpeedPointer(270f, -45f, 90f, new PointF(155f, 88.5f), new PointF(315.5f, 245f));
            _speedPointer_Max = new SpeedPointer(270f, -45f, 90f, new PointF(155f, 88.5f), new PointF(315.5f, 245f));
            using (FileStream fs = new FileStream(Path.Combine(RecPath , UIObj.ParaList[0]), FileMode.Open))
            {
                _pointer = Image.FromStream(fs);
            }
            using (FileStream fs = new FileStream(Path.Combine(RecPath, UIObj.ParaList[1]), FileMode.Open))
            {
                _pointer_TargetSpeed = Image.FromStream(fs);
            }
            using (FileStream fs = new FileStream(Path.Combine(RecPath, UIObj.ParaList[2]), FileMode.Open))
            {
                _pointer_MaxSpeed = Image.FromStream(fs);
            }

            _targetPoint = new NeedChangeLength(new PointF(28, 372), 10, 22f, new float[] { 1, 1f, 3f, 5f, 10f, 30, 50, 100, 300, 500 }, new float[] { 0, 1, 2, 5, 10, 20, 50, 100, 200, 500, 1000 });
            return true;
        }

        private Brush[] brushs_1 = new Brush[] { Brushes.Lime, Brushes.Red };
        private Brush[] brushs_2 = new Brush[] { Brushes.Red, Brushes.Lime, Brushes.Red };
        public override void paint(Graphics dcGs)
        {
            if (_pointer != null)
            {
                float realSpeed = FloatList[UIObj.InFloatList[0]];
                _speedPointer.PaintPointer(dcGs, _pointer, ref realSpeed);
            }

            if (_pointer_TargetSpeed != null)
            {
                float realSpeed = FloatList[UIObj.InFloatList[1]];
                _speedPointer_Target.PaintPointer(dcGs, _pointer_TargetSpeed, ref realSpeed);
            }

            if (_pointer_MaxSpeed != null)
            {
                float realSpeed = FloatList[UIObj.InFloatList[2]];
                _speedPointer_Max.PaintPointer(dcGs, _pointer_MaxSpeed, ref realSpeed);
            }

            float distance = FloatList[UIObj.InFloatList[3]] > 1000 ? 1000 : FloatList[UIObj.InFloatList[3]];
            _targetPoint.DrawRectangle(dcGs, ref distance, 3);
            dcGs.DrawString(distance
                .ToString("0"), new Font("Arial", 15, FontStyle.Bold), new SolidBrush(Color.FromArgb(255, 255, 0)), new RectangleF(5, 385, 73, 30), new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center });

            Font f = new Font("Arial", 15, FontStyle.Bold);
            dcGs.DrawString(FloatList[UIObj.InFloatList[0]].ToString("0"), f, new SolidBrush(Color.FromArgb(0, 255, 0)), new PointF(215, 375));
            dcGs.DrawString(FloatList[UIObj.InFloatList[1]].ToString("0"), f, new SolidBrush(Color.FromArgb(255, 255, 0)), new PointF(330, 375));
            dcGs.DrawString(FloatList[UIObj.InFloatList[2]].ToString("0"), f, new SolidBrush(Color.Red), new PointF(445, 375));

            String[] strs_1 = new[] { " 向前", "向后" };
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    dcGs.DrawString(
                        strs_1[i],
                        new Font("Arial", 15, FontStyle.Bold),
                        brushs_1[i],
                        new RectangleF(267, 282 + 30, 82, 30),
                        new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        );
                }
            }


            String[] strs_2 = new[] { " 未较准", "已校验", "记忆" };
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    dcGs.DrawString(
                        strs_2[i],
                        new Font("Arial", 15, FontStyle.Bold),
                        brushs_2[i],
                        new RectangleF(267, 282 + 30 + 30, 82, 30),
                        new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        );
                }
            }
            base.paint(dcGs);
        }
    }
}
