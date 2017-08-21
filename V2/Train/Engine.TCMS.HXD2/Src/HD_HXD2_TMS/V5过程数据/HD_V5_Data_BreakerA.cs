using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace HD_HXD2_TMS.V5过程数据
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V5_Data_BreakerA : baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源

        public override string GetInfo()
        {
            return "过程数据界面-A断路器";
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
            String[] clomn1 =
            {
                "辅助（460V/230V）", "C1-CRP", "C2-CRP", "C3-CRP", "DJ-CRP", "Q-REC", "C-VT-BM1",
                "C-VT-BM2","C-VT-MT1","C-VT-MT2","C-VT-SM1","C-VT-SM2","DJ-PE1","DJ-PE2"
            };
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[0] + i*3 + j])
                    {
                        dcGs.DrawImage(_images[j], new Rectangle(212,20+119+24*i,158,24));
                        dcGs.DrawString(
                            clomn1[i],
                            new Font("宋体", 11, FontStyle.Bold),
                            Brushes.White,
                            new RectangleF(212, 20 + 119 + 24 * i, 158, 24),
                            new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Center
                            }
                            );
                    }
                }
            }

            String[] clomn2 =
            {
                "DJ-PE3", "DJ-PE4", "DJ-PH1", "DJ-PH2", "DJ-VT-SM1", "DJ-VT-SM2", "Q-TH-VT-BM1",
                "Q-TH-VT-BM2","Q-TH-VT-BM3","Q-TH-VT-BM4"
            };
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[1] + i * 3 + j])
                    {
                        dcGs.DrawImage(_images[j], new Rectangle(212+188, 20 + 119 + 24 * (i+1), 158, 24));
                        dcGs.DrawString(
                            clomn2[i],
                            new Font("宋体", 11, FontStyle.Bold),
                            Brushes.White,
                            new RectangleF(212+188, 20 + 119 + 24 * (i+1), 158, 24),
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
