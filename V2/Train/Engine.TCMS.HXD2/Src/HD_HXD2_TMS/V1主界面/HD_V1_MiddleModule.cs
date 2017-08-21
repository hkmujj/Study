using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HD_HXD2_TMS.V1主界面
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V1_MiddleModule : baseClass
    {
        public class Condition
        {
            public float Value { get; set; }

            public Pen Pen { get; set; }

            public Pen RealPen { get; set; }
        }

        private List<Image> _images = new List<Image>();    //图片资源
        public override string GetInfo()
        {
            return "主界面-中间柱形图";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    _images.Add(Image.FromStream(fs));
                }
            });

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            //网压
            columnChart(
                dcGs,
                new Rectangle(130, 137, 84, 329),
                32,
                1,
                "kV",
                FloatList[UIObj.InFloatList[0]],
                Brushes.Yellow,
                new Condition() { Pen = Pens.Red, Value = 0, RealPen = Pens.Red },
                new Condition() { Pen = Pens.Red, Value = 17.5F, RealPen = Pens.Yellow },
                new Condition() { Pen = Pens.Blue, Value = 19F, RealPen = Pens.Yellow },
                new Condition() { Pen = Pens.LimeGreen, Value = 22.5F, RealPen = Pens.LimeGreen },
                new Condition() { Pen = Pens.LimeGreen, Value = 30F, RealPen = Pens.Yellow },
                new Condition() { Pen = Pens.Red, Value = 31F, RealPen = Pens.Red }
                );
            dcGs.DrawRectangle(Pens.White, new Rectangle(130+42,137+329+2,42,42));
            dcGs.DrawString(
                "网压",
                new Font("宋体", 10),
                Brushes.White,
                new RectangleF(130 + 42, 137 + 329 + 2+42, 42, 30),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );
            dcGs.DrawImage(_images[0], new Rectangle(130 + 42, 137 + 329 + 2, 42, 42));

            //原边电流
            columnChart(
                dcGs,
                new Rectangle(130+84+20, 137, 84, 329),
                50,
                10,
                "A",
                FloatList[UIObj.InFloatList[1]],
                new SolidBrush(Color.FromArgb(0,154,0)),
                new Condition() { Pen = new Pen(Color.FromArgb(0, 154, 0)), Value = 0, RealPen = new Pen(Color.FromArgb(0, 154, 0)) }
                );
            dcGs.DrawRectangle(Pens.White, new Rectangle(130 + 84 + 20 + 42, 137 + 329 + 2, 42, 42));
            dcGs.DrawString(
                "原边电流",
                new Font("宋体", 10),
                Brushes.White,
                new RectangleF(130 + 84 + 20 + 32, 137 + 329 + 2 + 42, 60, 30),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );
            dcGs.DrawImage(_images[1], new Rectangle(130 + 84 + 20 + 42, 137 + 329 + 2, 42, 42));

            //蓄电池电压
            columnChart(
                dcGs,
                new Rectangle(130 + 84 + 20 + 100, 137, 84, 329),
                30,
                5,
                "V",
                FloatList[UIObj.InFloatList[2]],
                new SolidBrush(Color.FromArgb(0, 154, 0)),
                new Condition() { Pen = Pens.Transparent, Value = 0F, RealPen = Pens.Red },
                new Condition() { Pen = Pens.Transparent, Value = 88F, RealPen = new Pen(Color.FromArgb(0, 154, 0)) },
                new Condition() { Pen = new Pen(Color.FromArgb(0, 154, 0)), Value = 137, RealPen = Pens.Red }
                );
            dcGs.DrawRectangle(Pens.White, new Rectangle(130 + 84 + 20 + 100 + 42, 137 + 329 + 2, 42, 42));
            dcGs.DrawString(
                "蓄电池电压",
                new Font("宋体", 10),
                Brushes.White,
                new RectangleF(130 + 84 + 20 + 100 + 22, 137 + 329 + 2 + 42, 75, 30),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );
            dcGs.DrawImage(_images[2], new Rectangle(130 + 84 + 20 + 100 + 42, 137 + 329 + 2, 42, 42));

            //牵引
            float value = (FloatList[UIObj.InFloatList[BoolList[UIObj.InBoolList[0]] ? 3 : 4]])/
                          (BoolList[UIObj.InBoolList[0]] ? 760 : 510)*100;
            columnChart(
                dcGs,
                new Rectangle(130 + 84 + 20 + 100+100, 137+30, 84, 329-30),
                50,
                2,
                "",
                value,
                BoolList[UIObj.InBoolList[0]] ? Brushes.Blue : Brushes.Red,
                new Condition()
                {
                    Pen = (BoolList[UIObj.InBoolList[0]] ? new Pen(Brushes.Blue) : Pens.Red),
                    Value = 0F,
                    RealPen = (BoolList[UIObj.InBoolList[0]] ? new Pen(Brushes.Blue) : Pens.Red)
                }
                );
            dcGs.DrawRectangle(Pens.White, new Rectangle(130 + 84 + 20 + 100 + 100 + 42, 137 + 329 + 2, 42, 42));
            dcGs.DrawString(
                BoolList[UIObj.InBoolList[0]] ? "牵引":"制动",
                new Font("宋体", 10),
                Brushes.White,
                new RectangleF(130 + 84 + 20 + 100 + 100 + 42, 137 + 329 + 2 + 42, 42, 30),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );
            dcGs.DrawRectangle(Pens.White, new Rectangle(130 + 84 + 20 + 100 + 100 + 42, 157, 42, 30));
            dcGs.DrawString(
                value.ToString("0"),
                new Font("宋体", 11),
                Brushes.White,
                new RectangleF(130 + 84 + 20 + 100 + 100 + 42, 157, 42, 30),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );
            dcGs.DrawString(
                "%",
                new Font("宋体", 11),
                Brushes.White,
                new RectangleF(130 + 84 + 20 + 100 + 100 + 42, 157-20, 42, 20),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );
            dcGs.DrawImage(_images[BoolList[UIObj.InBoolList[0]] ?3:4], new Rectangle(130 + 84 + 20 + 100 + 100 + 42, 137 + 329 + 2, 42, 42));

            base.paint(dcGs);
        }

        private void columnChart(Graphics dcGs, Rectangle rect, float count, float singleData, String unit,float value,Brush sb=null, params Condition[] conditions)
        {
            dcGs.DrawRectangle(Pens.White, new Rectangle(rect.X + rect.Width - 42, rect.Y + 20, 42, rect.Height - 20));
            dcGs.DrawString(
                unit,
                new Font("宋体", 13),
                Brushes.White,
                new RectangleF(rect.X + rect.Width - 42, rect.Y, 42, 20),
                new StringFormat() { LineAlignment= StringAlignment.Center, Alignment= StringAlignment.Center}
                );
            for (int i = 0; i < count+1; i++)
            {
                Int32 width = 2;
                if (i % 5 == 0)
                {
                    width = 5;
                    dcGs.DrawString(
                        (i*singleData).ToString(),
                        new Font("宋体", 11),
                        Brushes.White,
                        new RectangleF(rect.X, rect.Y + rect.Height - (rect.Height - 20) / count * i - (rect.Height - 20) / count, 42-5, (rect.Height - 20) / count*2),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Far }
                        );
                }
                if (i == count)
                {
                    width = 5;
                    dcGs.DrawString(
                        (i * singleData).ToString(),
                        new Font("宋体", 11),
                        Brushes.White,
                        new RectangleF(rect.X, rect.Y + rect.Height - (rect.Height - 20) / count * i - (rect.Height - 20) / count, 42-5, (rect.Height - 20) / count * 2),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Far }
                        );
                }
                dcGs.DrawLine(
                    Pens.White,
                    new PointF(rect.X + rect.Width - 42 - 2 - width, rect.Y + rect.Height - (rect.Height - 20) / count * i),
                    new PointF(rect.X + rect.Width - 42 - 2, rect.Y + rect.Height - (rect.Height - 20) / count * i)
                    );
            }

            if (conditions != null && conditions.Length != 0)
            {
                sb = (SolidBrush)conditions[0].RealPen.Brush;
                bool isFind = false;
                if (conditions.Length > 1)
                {
                    for (int j = 1; j < conditions.Length; j++)
                    {
                        if (value < conditions[j].Value && !isFind)
                        {
                            sb = (SolidBrush)conditions[j - 1].RealPen.Brush;
                            isFind = true;
                        }
                    }

                    if (!isFind)
                        sb = (SolidBrush)conditions[conditions.Length - 1].RealPen.Brush;
                }
            }
            float y = rect.Y + 20 + (count - value / singleData) * ((rect.Height - 20) / count);
            if (value >= count * singleData)
            {
                value = count * singleData;
                y = rect.Y + 20 + (count - value / singleData) * ((rect.Height - 20) / count) + 1;
            }
            dcGs.FillRectangle(sb, new RectangleF(rect.X + rect.Width - 42 + 1, y, 41, (value / singleData) * ((rect.Height - 20) / count)));

            if (conditions != null && conditions.Length != 0)
            {
                //sb = (SolidBrush)conditions[0].Pen.Brush;
                //bool isFind = false;
                if (conditions.Length > 1)
                {
                    for (int j = 1; j < conditions.Length; j++)
                    {
                        dcGs.DrawLine(
                            conditions[j].Pen,
                            new PointF(rect.X + rect.Width - 42 + 1, rect.Y + 20 + (count - conditions[j].Value / singleData) * ((rect.Height - 20) / count)),
                            new PointF(rect.X + rect.Width - 1, rect.Y + 20 + (count - conditions[j].Value / singleData) * ((rect.Height - 20) / count))
                            );
                        //if (value < conditions[j].Value && !isFind)
                        //{
                            //sb = (SolidBrush)conditions[j-1].Pen.Brush;
                            //isFind = true;
                        //}
                    }

                    //if(!isFind)
                        //sb = (SolidBrush)conditions[conditions.Length - 1].Pen.Brush;
                }
            }
        }
    }
}
