using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace HD_HXD2_TMS.V5过程数据
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V5_Data_OnlineState : baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源
        private List<Rectangle> _rects1 = new List<Rectangle>();
        private List<String> _stateNames = new List<string>();

        public override string GetInfo()
        {
            return "过程数据界面-在线状态";
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
                new Rectangle(333,58+119,60,34),
                new Rectangle(393,58+119,60,34),
                new Rectangle(99,127+119,60,34),
                new Rectangle(339,279+119,60,34),
                new Rectangle(99,172+119,60,34),
                new Rectangle(213,127+119,60,34),
                new Rectangle(213,172+119,60,34),
                new Rectangle(213,277+119,60,34),
                new Rectangle(339,132+119,60,34),
                new Rectangle(339,172+119,60,34),
                new Rectangle(455,227+119,60,34),
                new Rectangle(528,350+119,60,34),
                new Rectangle(563,127+119,60,34),
                new Rectangle(563,172+119,60,34),
                new Rectangle(643,127+119,60,34),
                new Rectangle(643,172+119,60,34)
            };

            _stateNames = new List<string>()
            {
                "GW1",
                "GW2",
                "RIOM1",
                "RIOM2",
                "DDU1",
                "TCU1",
                "TCU2",
                "BCU",
                "MPU1",
                "MPU2",
                "GW3",
                "CMD",
                "TCU3",
                "TCU4",
                "ACU1",
                "ACU2"
            };

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            dcGs.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            dcGs.DrawImage(_images[0], new Rectangle(0,119,800,430));

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[0 + i] + j])
                    {
                        dcGs.DrawImage(_images[1 + j], _rects1[i]);
                        dcGs.DrawString(
                            _stateNames[i],
                            new Font("宋体", 11),
                            Brushes.Black,
                            _rects1[i],
                            new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                            );
                    }
                }
            }

            base.paint(dcGs);
        }
    }
}
