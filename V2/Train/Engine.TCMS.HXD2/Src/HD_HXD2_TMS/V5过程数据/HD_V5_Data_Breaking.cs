using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace HD_HXD2_TMS.V5过程数据
{
     [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V5_Data_Breaking : baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源

         public override string GetInfo()
        {
            return "过程数据界面-牵引制动";
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

             dcGs.DrawImage(_images[0], new Rectangle(0, 119, 800, 428));
             
             //实际速速
             dcGs.DrawString(
                 FloatList[UIObj.InFloatList[6]].ToString("0km/h"),
                 new Font("宋体", 13),
                 Brushes.Yellow,
                 new RectangleF(473, 69 + 119, 80, 20),
                 new StringFormat()
                 {
                     LineAlignment = StringAlignment.Center,
                     Alignment = StringAlignment.Center
                 }
                 );
             //设定速速
             dcGs.DrawString(
                 FloatList[UIObj.InFloatList[7]].ToString("0km/h"),
                 new Font("宋体", 13),
                 Brushes.Yellow,
                 new RectangleF(473, 135 + 119, 80, 20),
                 new StringFormat()
                 {
                     LineAlignment = StringAlignment.Center,
                     Alignment = StringAlignment.Center
                 }
                 );
             //司控器级位
             String level = FloatList[UIObj.InFloatList[8]].ToString("0.0");
             if (BoolList[UIObj.InBoolList[1]]) level = "*";
             else if (BoolList[UIObj.InBoolList[2]]) level = "-" + level;
             dcGs.DrawString(
                 level,
                 new Font("宋体", 13),
                 Brushes.Yellow,
                 new RectangleF(473, 204 + 119, 80, 20),
                 new StringFormat()
                 {
                     LineAlignment = StringAlignment.Center,
                     Alignment = StringAlignment.Center
                 }
                 );

             //A车
             Boolean isBreaking = BoolList[UIObj.InBoolList[0]];//是否为制动
             for (int i = 0; i < 2; i++)
             {
                 for (int j = 0; j < 2; j++)
                 {
                     if (!isBreaking) //牵引
                     {
                         //A车
                         dcGs.FillRectangle(
                             Brushes.White,
                             new RectangleF(64 + 87*i + 42*j,
                                 41 + (154 - 1.54F*FloatList[UIObj.InFloatList[0] + i*2 + j]) + 119, 7,
                                 1.54F*FloatList[UIObj.InFloatList[0] + i*2 + j])
                             );
                         dcGs.DrawString(
                            FloatList[UIObj.InFloatList[0] + i * 2 + j].ToString("0.0"),
                            new Font("宋体", 9),
                            Brushes.Yellow,
                            new RectangleF(65 + (i * 2 + j)*43, 384+119, 40, 13),
                            new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Center
                            }
                            );
                         dcGs.FillRectangle(
                             Brushes.Blue,
                             new RectangleF(73 + 87*i + 42*j,
                                 41 + (154 - 1.54F*FloatList[UIObj.InFloatList[1] + i*2 + j]) + 119, 30,
                                 1.54F*FloatList[UIObj.InFloatList[1] + i*2 + j])
                             );
                         dcGs.DrawString(
                            FloatList[UIObj.InFloatList[1] + i * 2 + j].ToString("0"),
                            new Font("宋体", 9),
                            Brushes.Yellow,
                            new RectangleF(65 + (i * 2 + j) * 43, 400 + 119, 40, 13),
                            new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Center
                            }
                            );

                         //B车
                         dcGs.FillRectangle(
                             Brushes.White,
                             new RectangleF(287 + 87*i + 42*j,
                                 41 + (154 - 1.54F*FloatList[UIObj.InFloatList[2] + i*2 + j]) + 119, 7,
                                 1.54F*FloatList[UIObj.InFloatList[2] + i*2 + j])
                             );
                         dcGs.DrawString(
                            FloatList[UIObj.InFloatList[2] + i * 2 + j].ToString("0.0"),
                            new Font("宋体", 9),
                            Brushes.Yellow,
                            new RectangleF(287 + (i * 2 + j) * 43, 384 + 119, 40, 13),
                            new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Center
                            }
                            );
                         dcGs.FillRectangle(
                             Brushes.Blue,
                             new RectangleF(295 + 87*i + 42*j,
                                 41 + (154 - 1.54F*FloatList[UIObj.InFloatList[3] + i*2 + j]) + 119, 30,
                                 1.54F*FloatList[UIObj.InFloatList[3] + i*2 + j])
                             );
                         dcGs.DrawString(
                            FloatList[UIObj.InFloatList[3] + i * 2 + j].ToString("0"),
                            new Font("宋体", 9),
                            Brushes.Yellow,
                            new RectangleF(287 + (i * 2 + j) * 43, 400 + 119, 40, 13),
                            new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Center
                            }
                            );

                         //第三个
                         dcGs.FillRectangle(
                             Brushes.White,
                             new RectangleF(606 + 87 * i + 42 * j,
                                 41 + (154 - 154 * FloatList[UIObj.InFloatList[4] + i * 2 + j]/600) + 119, 7,
                                 154  * FloatList[UIObj.InFloatList[4] + i * 2 + j]/ 600)
                             );
                         dcGs.DrawString(
                            FloatList[UIObj.InFloatList[4] + i * 2 + j].ToString("0.0"),
                            new Font("宋体", 9),
                            Brushes.Yellow,
                            new RectangleF(607 + (i * 2 + j) * 43, 384 + 119, 40, 13),
                            new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Center
                            }
                            );
                         dcGs.FillRectangle(
                             Brushes.Blue,
                             new RectangleF(615 + 87 * i + 42 * j,
                                 41 + (154 - 154 * FloatList[UIObj.InFloatList[5] + i * 2 + j] / 600) + 119, 30,
                                 154  * FloatList[UIObj.InFloatList[5] + i * 2 + j]/ 600)
                             );
                         dcGs.DrawString(
                            FloatList[UIObj.InFloatList[5] + i * 2 + j].ToString("0"),
                            new Font("宋体", 9),
                            Brushes.Yellow,
                            new RectangleF(607 + (i * 2 + j) * 43, 400 + 119, 40, 13),
                            new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Center
                            }
                            );
                     }
                     else
                     {
                         //A车
                         dcGs.FillRectangle(
                             Brushes.White,
                             new RectangleF(64 + 87*i + 42*j, 195 + 119, 7,
                                 1.55F*FloatList[UIObj.InFloatList[0] + i*2 + j])
                             );
                         String a = FloatList[UIObj.InFloatList[0] + i*2 + j].ToString("0.0");
                         if(a!="0.0") a="-"+a;
                         dcGs.DrawString(
                            a,
                            new Font("宋体", 9),
                            Brushes.Yellow,
                            new RectangleF(65 + (i * 2 + j) * 43, 384 + 119, 40, 13),
                            new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Center
                            }
                            );
                         dcGs.FillRectangle(
                             Brushes.Red,
                             new RectangleF(73 + 87*i + 42*j, 195 + 119, 30,
                                 1.55F*FloatList[UIObj.InFloatList[1] + i*2 + j])
                             );
                         a = FloatList[UIObj.InFloatList[1] + i * 2 + j].ToString("0");
                         if (a != "0") a = "-" + a;
                         dcGs.DrawString(
                            a,
                            new Font("宋体", 9),
                            Brushes.Yellow,
                            new RectangleF(65 + (i * 2 + j) * 43, 400 + 119, 40, 13),
                            new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Center
                            }
                            );

                         //B车
                         dcGs.FillRectangle(
                             Brushes.White,
                             new RectangleF(287 + 87 * i + 42 * j, 195 + 119, 7,
                                 1.55F*FloatList[UIObj.InFloatList[2] + i*2 + j])
                             );
                         a = FloatList[UIObj.InFloatList[2] + i * 2 + j].ToString("0.0");
                         if (a != "0.0") a = "-" + a;
                         dcGs.DrawString(
                            a,
                            new Font("宋体", 9),
                            Brushes.Yellow,
                            new RectangleF(287 + (i * 2 + j) * 43, 384 + 119, 40, 13),
                            new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Center
                            }
                            );
                         dcGs.FillRectangle(
                             Brushes.Red,
                             new RectangleF(295 + 87 * i + 42 * j, 195 + 119, 30,
                                 1.55F*FloatList[UIObj.InFloatList[3] + i*2 + j])
                             );
                         a = FloatList[UIObj.InFloatList[3] + i * 2 + j].ToString("0");
                         if (a != "0") a = "-" + a;
                         dcGs.DrawString(
                            a,
                            new Font("宋体", 9),
                            Brushes.Yellow,
                            new RectangleF(287 + (i * 2 + j) * 43, 400 + 119, 40, 13),
                            new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Center
                            }
                            );

                         dcGs.FillRectangle(
                             Brushes.White,
                             new RectangleF(606 + 87 * i + 42 * j, 195 + 119, 7,
                                 155  * FloatList[UIObj.InFloatList[4] + i * 2 + j]/ 600)
                             );
                         a = FloatList[UIObj.InFloatList[4] + i * 2 + j].ToString("0.0");
                         if (a != "0.0") a = "-" + a;
                         dcGs.DrawString(
                            a,
                            new Font("宋体", 9),
                            Brushes.Yellow,
                            new RectangleF(607 + (i * 2 + j) * 43, 384 + 119, 40, 13),
                            new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Center
                            }
                            );
                         dcGs.FillRectangle(
                             Brushes.Red,
                             new RectangleF(615 + 87 * i + 42 * j, 195 + 119, 30,
                                 155  * FloatList[UIObj.InFloatList[5] + i * 2 + j]/ 600)
                             );
                         a = FloatList[UIObj.InFloatList[5] + i * 2 + j].ToString("0");
                         if (a != "0") a = "-" + a;
                         dcGs.DrawString(
                            a,
                            new Font("宋体", 9),
                            Brushes.Yellow,
                            new RectangleF(607 + (i * 2 + j) * 43, 400 + 119, 40, 13),
                            new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Center
                            }
                            );
                     }
                 }
             }

             base.paint(dcGs);
         }
    }
}
