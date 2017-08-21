using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HD_HXD2_TMS.V1主界面
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V1_LeftModule : baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源
        public override string GetInfo()
        {
            return "公共试图-标题信息";
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
            if (HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentAView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentBView)
                return;

            HD_HXD2_TMS.VC公共组件.HD_VC_Title.TrainName = "A";

            //12个图片状态
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    dcGs.DrawRectangle(Pens.White, new Rectangle(30+50*i, 117+35+47*j, 42, 42));
                }
            }
            //机车方向
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    dcGs.DrawImage(_images[i], new Rectangle(30 + 50 * 0, 117 + 35 + 47 * 0, 42, 42));
                }
            }

            //空转、滑行
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    dcGs.DrawImage(_images[3+i], new Rectangle(30 + 50 * 1, 117 + 35 + 47 * 0, 42, 42));
                }
            }

            //受电弓
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    dcGs.DrawImage(_images[5+i], new Rectangle(30 + 50 * 0, 117 + 35 + 47 * 1, 42, 42));
                }
            }

            //定速
            if (BoolList[UIObj.InBoolList[3]])
            {
                dcGs.DrawImage(_images[9], new Rectangle(30 + 50 * 1, 117 + 35 + 47 * 1, 42, 42));
            }

            //主断
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[UIObj.InBoolList[4] + i])
                {
                    dcGs.DrawImage(_images[10 + i], new Rectangle(30 + 50 * 0, 117 + 35 + 47 * 2, 42, 42));
                }
            }

            //紧急制动、惩罚制动
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[5] + i])
                {
                    dcGs.DrawImage(_images[14 + i], new Rectangle(30 + 50 * 1, 117 + 35 + 47 * 2, 42, 42));
                }
            }

            //无电区
            if (BoolList[UIObj.InBoolList[6]])
            {
                dcGs.DrawImage(_images[16], new Rectangle(30 + 50 * 0, 117 + 35 + 47 * 3, 42, 42));
            }

            //停放制动
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[7] + i])
                {
                    dcGs.DrawImage(_images[17 + i], new Rectangle(30 + 50 * 0, 117 + 35 + 47 * 4, 42, 42));
                }
            }

            //撒沙
            if (BoolList[UIObj.InBoolList[8]])
            {
                dcGs.DrawImage(_images[20], new Rectangle(30 + 50 * 0, 117 + 35 + 47 * 5, 42, 42));
            }

            //压缩机
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[9] + i])
                {
                    dcGs.DrawImage(_images[21 + i], new Rectangle(30 + 50 * 1, 117 + 35 + 47 * 5, 42, 42));
                }
            }

            dcGs.DrawImage(_images[24], new Rectangle(5, 117 + 35 + 47 * 6+30+20, 70, 30));
            //单独A车
            if (BoolList[UIObj.InBoolList[10]]) //在A端
            {
                dcGs.DrawString(
                    "HXD2" + FloatList[UIObj.InFloatList[0]] + "A",
                    new Font("宋体", 9),
                    Brushes.White,
                    new RectangleF(5, 117 + 35 + 47*6 - 20 + 30+20, 70, 20),
                    new StringFormat() {LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center}
                    );
                if (!BoolList[UIObj.InBoolList[11]])
                {
                    dcGs.DrawImage(_images[25], new Rectangle(5 + 70 + 5, 117 + 35 + 47*6 + 30+20, 70, 30));
                    dcGs.DrawString(
                        "HXD2" + FloatList[UIObj.InFloatList[1]] + "B",
                        new Font("宋体", 9),
                        Brushes.White,
                        new RectangleF(5 + 70 + 5, 117 + 35 + 47*6 - 20 + 30+20, 70, 20),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                        );
                }
            }
            else if (BoolList[UIObj.InBoolList[10] + 1]) //在B端
            {
                dcGs.DrawString(
                    "HXD2" + FloatList[UIObj.InFloatList[1]] + "B",
                    new Font("宋体", 9),
                    Brushes.White,
                    new RectangleF(5, 117 + 35 + 47*6 - 20 + 30+20, 70, 20),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
                if (!BoolList[UIObj.InBoolList[11] + 1])
                {
                    dcGs.DrawImage(_images[25], new Rectangle(5 + 70 + 5, 117 + 35 + 47*6 + 30+20, 70, 30));
                    dcGs.DrawString(
                        "HXD2" + FloatList[UIObj.InFloatList[0]] + "A",
                        new Font("宋体", 9),
                        Brushes.White,
                        new RectangleF(5 + 70 + 5, 117 + 35 + 47*6 - 20 + 30+20, 70, 20),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                        );
                }
            }

            base.paint(dcGs);
        }
    }
}
