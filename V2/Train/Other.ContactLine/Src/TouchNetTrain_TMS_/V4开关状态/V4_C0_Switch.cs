using System;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace TouchNetTrain_TMS_.开关状态
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V4_C0_Switch : baseClass
    {
        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "开关状态-状态";
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
            paint_Frame(dcGs);

            base.paint(dcGs);
        }

        private void paint_Frame(Graphics dcGs)
        {
            String[] strs = { "DI1", "DI2", "DI5", "DI6", "DI7", "DI8", "DI9", "DI10",
                              "DI11", "DO1", "DO2", "DO3", "DO4", "DO5", "DO6", "DO7" };

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    dcGs.DrawRectangle(
                        Global.Pen_White_2,
                        new Rectangle(45 + 92 * j, 97 + 220 * i, 58, 214)
                        );
                    dcGs.DrawString(
                        strs[i * 8 + j],
                        new Font("Verdana", 11),
                        Brushes.White,
                        new RectangleF(45 + 92 * j, 97 + 220 * i, 58, 214),
                        Global.SF_CN
                        );
                    paint_State(dcGs, new Rectangle(45 + 92 * j, 97 + 220 * i, 58, 214), getValue(i * 8 + j));
                }
            }
        }

        private Boolean[] getValue(int id)
        {
            Boolean[] values = new Boolean[12];
            for (int i = 0; i < 12; i++)
            {
                values[i] = BoolList[UIObj.InBoolList[0] + id * 24 + i * 2];
            }

            return values;
        }

        private void paint_State(Graphics dcGs, Rectangle rect, Boolean[] values)
        {
            String[] strs = { "11", "21", "12", "22", "13", "23", "14", "24", "15", "25", "16", "26" };
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    dcGs.DrawString(
                        strs[i * 2 + j],
                        Global.Font_Verdana_10,
                        Brushes.White,
                        new RectangleF(rect.Left + 29 * j, rect.Top + 15 + 33 * i, 30, 14),
                        Global.SF_CN
                        );
                    if (values[i * 2 + j])
                    {
                        dcGs.FillEllipse(Brushes.Gray, rect.Left + 7 + 28 * j, rect.Top + 28 + 33 * i, 17, 17);
                    }
                    else
                        dcGs.FillEllipse(Brushes.Green, rect.Left + 7 + 28 * j, rect.Top + 28 + 33 * i, 17, 17);
                }
            }
        }
    }
}
