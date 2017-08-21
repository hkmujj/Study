using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HD_HXD2_TMS.V5过程数据
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V5_Data_DriveB : baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源

        public override string GetInfo()
        {
            return "过车数据试图-B驱动概况";
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
            dcGs.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            dcGs.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            dcGs.DrawImage(_images[0], new Rectangle(0, 118, 800, 432));

            //第一块
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    float xOffset = i < 2 ? 264 : 258;
                    float value1 = FloatList[UIObj.InFloatList[i*3 + j]];
                    if (i == 0 && j == 0) value1 *= 1000;
                    dcGs.DrawString(
                       value1.ToString("0"),
                       new Font("宋体", 11),
                       Brushes.Yellow,
                       new RectangleF(206 + xOffset * i, 3 + 118 + 25 * j, 68, 22),
                       new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                       );
                }
            }

            bool isBreaking = BoolList[UIObj.InBoolList[0]];
            String value = FloatList[UIObj.InFloatList[9]].ToString("0");
            if (isBreaking) value = "-" + value;
            dcGs.DrawString(
                value,
                new Font("宋体", 11),
                Brushes.Yellow,
                new RectangleF(206, 3 + 118 + 25 * 3, 68, 22),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                );

            //四象限
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    dcGs.DrawString(
                       FloatList[UIObj.InFloatList[10+i * 3 + j]].ToString("0"),
                       new Font("宋体", 11),
                       Brushes.Yellow,
                       new RectangleF(335 + 116 * i, 127 + 118 + 25 * j, 115, 23),
                       new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                       );
                }
            }

            //变流器
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    dcGs.DrawString(
                       FloatList[UIObj.InFloatList[22 + i * 2 + j]].ToString("0"),
                       new Font("宋体", 11),
                       Brushes.Yellow,
                       new RectangleF(335 + 116 * i, 230 + 118 + 25 * j, 115, 23),
                       new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                       );
                }
            }

            //实际牵引力
            for (int i = 0; i < 6; i++)
            {
                dcGs.DrawString(
                    FloatList[UIObj.InFloatList[30 + i]].ToString("0"),
                    new Font("宋体", 11),
                    Brushes.Yellow,
                    new RectangleF(206 + 99 * i, 305 + 118, 97, 24),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                    );
            }
            //电机电流
            for (int i = 0; i < 6; i++)
            {
                dcGs.DrawString(
                    FloatList[UIObj.InFloatList[36 + i]].ToString("0"),
                    new Font("宋体", 11),
                    Brushes.Yellow,
                    new RectangleF(206 + 99 * i, 330 + 118, 97, 24),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                    );
            }
            //电机温度
            for (int i = 0; i < 4; i++)
            {
                dcGs.DrawString(
                    FloatList[UIObj.InFloatList[42 + i]].ToString("0"),
                    new Font("宋体", 11),
                    Brushes.Yellow,
                    new RectangleF(206 + 99 * (i+2), 355 + 118, 97, 24),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                    );
            }

            base.paint(dcGs);
        }
    }
}
