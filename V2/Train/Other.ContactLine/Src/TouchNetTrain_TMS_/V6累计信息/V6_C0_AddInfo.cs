using System;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace TouchNetTrain_TMS_.累计信息
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V6_C0_AddInfo : baseClass
    {
        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "累计信息-主界面";
        }

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }
        #endregion

        public override void paint(Graphics dcGs)
        {
            paint_Engine_1(dcGs);
            paint_Engine_2(dcGs);
            paint_Mileage(dcGs);
        }

        /// <summary>
        /// 柴油机1累计运行时间
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Engine_1(Graphics dcGs)
        {
            dcGs.DrawString(
                "柴油机Ⅰ累计运行时间",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(38, 93, 230, 46),
                Global.SF_CC
                );

            String[] strs = { "600以下", "600-800", "800-1000", "1000-1200", "1200-1500", "1500-1800", "1800以上", "总计时间" };
            for (int i = 0; i < 8; i++)
            {
                dcGs.DrawRectangle(
                    Global.Pen_White_2,
                    new Rectangle(38, 139 + 44 * i, 230, 40)
                    );
                dcGs.FillRectangle(Brushes.White, new Rectangle(38 + 3, 139 + 44 * i + 3, 130, 34));
                dcGs.DrawString(
                    strs[i],
                    Global.Font_Verdana_13,
                    Brushes.Black,
                    new RectangleF(38 + 3, 139 + 44 * i + 3, 130, 34),
                    Global.SF_CC
                    );
                dcGs.DrawString(
                    FloatList[UIObj.InFloatList[0] + i].ToString("0  h"),
                    Global.Font_Verdana_13,
                    Brushes.White,
                    new RectangleF(38, 139 + 44 * i, 220, 40),
                    Global.SF_FC
                    );
            }
        }

        /// <summary>
        /// 柴油机2累计运行时间
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Engine_2(Graphics dcGs)
        {
            dcGs.DrawString(
                "柴油机Ⅱ累计运行时间",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(290, 93, 230, 46),
                Global.SF_CC
                );
            String[] strs = {"600以下", "600-800", "800-1000", "1000-1200", "1200-1500", "1500-1800", "1800以上", "总计时间"};
            for (int i = 0; i < 8; i++)
            {
                dcGs.DrawRectangle(
                    Global.Pen_White_2,
                    new Rectangle(290, 139 + 44 * i, 230, 40)
                    );
                dcGs.FillRectangle(Brushes.White, new Rectangle(290 + 3, 139 + 44 * i+3, 130, 34));
                dcGs.DrawString(
                    strs[i],
                    Global.Font_Verdana_13,
                    Brushes.Black,
                    new RectangleF(290 + 3, 139 + 44 * i + 3, 130, 34),
                    Global.SF_CC
                    );
                dcGs.DrawString(
                    FloatList[UIObj.InFloatList[1]+i].ToString("0  h"),
                    Global.Font_Verdana_13,
                    Brushes.White,
                    new RectangleF(290, 139 + 44 * i, 220, 40),
                    Global.SF_FC
                    );
            }
        }

        /// <summary>
        /// 累计运行里程
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Mileage(Graphics dcGs)
        {
            dcGs.DrawString(
                "累计运行里程",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(580, 93, 140, 46),
                Global.SF_CC
                );
            dcGs.DrawRectangle(
                Global.Pen_White_2,
                new Rectangle(580, 139, 140, 40)
                );
            dcGs.FillRectangle(Brushes.White, new Rectangle(580 + 3, 139 + 3, 134, 34));
            dcGs.DrawString(
                FloatList[UIObj.InFloatList[2]].ToString("0.0  km"),
                Global.Font_Verdana_13,
                Brushes.Black,
                new RectangleF(580, 139, 140, 40),
                Global.SF_CC
                );
        }
    }
}
