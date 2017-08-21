using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace HD_HXD2_TMS.V5过程数据
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V5_Data_Conveter : baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源
        private List<Rectangle> _rects1 = new List<Rectangle>();
        private List<Rectangle> _rects2 = new List<Rectangle>();

        public override string GetInfo()
        {
            return "过程数据界面-主变流";
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
                new Rectangle(241,38+119,15,7),
                new Rectangle(241,68+119,15,7),
                new Rectangle(241,106+119,15,7),
                new Rectangle(241,136+119,15,7),
                new Rectangle(241,252+119,15,7),
                new Rectangle(241,283+119,15,7),
                new Rectangle(241,342+119,15,7),
                new Rectangle(241,372+119,15,7),
                new Rectangle(387,195+119,15,7),
                new Rectangle(387,226+119,15,7),
                new Rectangle(233,191+119,32,14)
            };

            _rects2 = new List<Rectangle>()
            {
                new Rectangle(469,68+119,42,13),
                new Rectangle(469,135+119,42,13),
                new Rectangle(469,281+119,42,13),
                new Rectangle(469,371+119,42,13)
            };

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            dcGs.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            dcGs.DrawImage(_images[0], new Rectangle(0,119,800,415));

            for (int i = 0; i < 10; i++)
            {
               if (BoolList[UIObj.InBoolList[0 + i]])
               {
                   dcGs.DrawImage(_images[1], _rects1[i]);
               }
            }

            if (BoolList[UIObj.InBoolList[10]])
            {
                dcGs.DrawImage(_images[2], _rects1[10]);
            }

            for (int i = 0; i < 4; i++)
            {
                dcGs.DrawString(
                    FloatList[UIObj.InFloatList[0]+i].ToString("0"),
                    new Font("宋体", 11),
                    Brushes.Yellow,
                    _rects2[i],
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Far }
                    );
            }

            base.paint(dcGs);
        }
    }
}
