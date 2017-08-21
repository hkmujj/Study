using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace HD_HXD2_TMS.V5过程数据
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V5_Data_AuxiliaryA : baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源
        private List<Rectangle> _rects1 = new List<Rectangle>();
        private List<Rectangle> _rects2 = new List<Rectangle>();
        private List<Rectangle> _rects3 = new List<Rectangle>();
        private List<Rectangle> _rects4 = new List<Rectangle>();

        public override string GetInfo()
        {
            return "过程数据界面-A辅助";
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

            _rects1 = new List<Rectangle>()
            {
                new Rectangle(213,74+119,15,7),
                new Rectangle(213,172+119,15,7),
                new Rectangle(213,277+119,15,7),
                new Rectangle(323,277+119,15,7),
                new Rectangle(323,306+119,15,7),
                new Rectangle(322,349+119,15,7),
                new Rectangle(322,378+119,15,7),
                new Rectangle(529,53+119,15,7),
                new Rectangle(571,53+119,15,7),
                new Rectangle(571,82+119,15,7),
                new Rectangle(546,310+119,15,7),
                new Rectangle(546,339+119,15,7)
            };
            _rects2 = new List<Rectangle>()
            {
                new Rectangle(127,169+119,32,14),
                new Rectangle(615,49+119,32,14),
                new Rectangle(547,115+119,32,14),
                new Rectangle(547,144+119,32,14),
                new Rectangle(547,173+119,32,14),
                new Rectangle(547,203+119,32,14),
                new Rectangle(547,232+119,32,14),
                new Rectangle(547,261+119,32,14)
            };
            _rects3 = new List<Rectangle>()
            {
                new Rectangle(259,221+119,8,16)
            };

            _rects4 = new List<Rectangle>() 
            {
                new Rectangle(37,79+119,40,20),
                new Rectangle(37,282+119,40,20),
                new Rectangle(107,48+119,20,20),
                new Rectangle(108,251+119,19,20)
            };

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            dcGs.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            dcGs.DrawImage(_images[0], new Rectangle(0,119,800,428));

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (BoolList[UIObj.InBoolList[0+ i]  + j])
                    {
                        dcGs.DrawImage(_images[1 + j], _rects1[i]);
                    }
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (BoolList[UIObj.InBoolList[12 + i] + j])
                    {
                        dcGs.DrawImage(_images[5 + j], _rects2[i]);
                    }
                }
            }

            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (BoolList[UIObj.InBoolList[20 + i ] + j])
                    {
                        dcGs.DrawImage(_images[3 + j], _rects3[i]);
                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                dcGs.DrawString(
                   FloatList[UIObj.InFloatList[i]].ToString("0"),
                   new Font("宋体", 11),
                   Brushes.Yellow,
                   _rects4[i],
                   new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Far }
                   );
            }

            base.paint(dcGs);
        }
    }
}
