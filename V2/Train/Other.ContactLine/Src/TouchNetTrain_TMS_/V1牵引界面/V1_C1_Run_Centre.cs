using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace TouchNetTrain_TMS_
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1_C1_Run_Centre : baseClass
    {
        private List<Image> _resource_Image = new List<Image>();    //图片资源

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "运行信息-中间信息";
        }

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            return true;
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            paint_Train(dcGs);
            paint_NiuJu(dcGs);
            paint_Information(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制车
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Train(Graphics dcGs)
        {
            dcGs.DrawImage(
                _resource_Image[0],
                new RectangleF(262, 93 + 5 + 3, 265, 84F)
                );
            dcGs.DrawLine(Global.Pen_White, new Point(265, 185), new Point(530, 185));

            if (BoolList[UIObj.InBoolList[0]])
            {
                dcGs.DrawImage(
                    _resource_Image[2],
                    new RectangleF(262+52, 93 + 5 + 3+16+5, 19, 37)
                    );
            }
            else if (BoolList[UIObj.InBoolList[1]])
            {
                dcGs.DrawImage(
                    _resource_Image[2],
                    new RectangleF(262 + 188, 93 + 5 + 3 + 16+5, 19, 37)
                    );
            }
        }

        private SolidBrush SB = new SolidBrush(Color.FromArgb(255, 185, 138));

        /// <summary>
        /// 绘制扭矩
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_NiuJu(Graphics dcGs)
        {
            dcGs.DrawImage(
                _resource_Image[1],
                new RectangleF(262, 187, 265, 205F)
                );

            float value1 = FloatList[UIObj.InFloatList[0]] * 100 * 1.94F;
            float value2 = FloatList[UIObj.InFloatList[1]] * 100 * 1.94F;

            dcGs.FillRectangle(
                SB,
                new RectangleF(262 + 84 + 2, 187 + 204 - 2 - value1, 24, value1)
                );
            dcGs.FillRectangle(
                SB,
                new RectangleF(262 + 152.1F + 2, 187 + 204 - 2 - value2, 24, value2)
                );
        }

        /// <summary>
        /// 绘制信息显示
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Information(Graphics dcGs)
        {
            //黄色框
            dcGs.DrawRectangle(
                Global.Pen_Yellow_2,
                new Rectangle(265, 394, 391, 141)
                );
        }
        #endregion
    }
}
