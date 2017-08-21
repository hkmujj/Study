#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-4
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图1-运行-No.5-模式和工况信息
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图1-运行-No.4-模式和工况信息
    /// 创建人：唐林
    /// 创建时间：2014-7-4
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V1_Run_C4_ModeAndGK : baseClass
    {
        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "运行试图-工况与模式";
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns>是否初始化成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            //this.paint_GK(dcGs);
            for (int i = 0; i < 3; i++)
            {
                dcGs.DrawRectangle(new Pen(new SolidBrush(Color.White)), new Rectangle(6+99*i, 510, 92, 33));
                dcGs.DrawLine(Pens.White, new Point(6 + 31 + 99 * i, 510), new Point(6 + 31 + 99 * i, 543));
                dcGs.DrawString(
                    _strs[i], 
                    new Font("宋体", 10),
                    Brushes.White,
                    new Rectangle(4+99*i,512,35,33), 
                    new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                    );
            }
            this.paint_Mode(dcGs);
            paint_CPU(dcGs);
            paint_TrainState(dcGs);

            base.paint(dcGs);
        }
        String[] _strs = { "模式\n手柄", "主控\n手柄", "车辆\n状态" };
        /// <summary>
        /// 绘制工况（B数据下标0）
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_GK(Graphics dcGs)
        {
            dcGs.DrawRectangle(new Pen(new SolidBrush(Color.White)), new Rectangle(129, 510, 143, 33));

            String[] strs = new String[] { "惰性", "牵引", "制动", "保持制动", "停放制动", "快速制动", "紧急制动", "无效" };
            Int32 index = 7;
            for (int i = 0; i < 7; i++)
            {
                if (BoolList[UIObj.InBoolList[0]+i])
                {
                    index = i;
                } 
            }
            dcGs.DrawString(strs[index], new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(129, 510, 143, 33), FontInfo.SF_CC);
        }

        private String[] _strs1 = { "惰性", "牵引", "制动", "未知" };
        private void paint_CPU(Graphics dcGs)
        {
            Int32 index = 3;
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    index = i;
                    break;
                }
            }
            String str = _strs1[index] + String.Format("-{0:F0}%", FloatList[UIObj.InFloatList[0]]);
            dcGs.DrawString(str, new Font("宋体", 10), new SolidBrush(Color.White), new RectangleF(3 + 31+99, 511, 67, 33), FontInfo.SF_CC);
        }

        /// <summary>
        /// 绘制模式（B数据下标1，F数据下标0）
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Mode(Graphics dcGs)
        {
            String[] strs = new String[] { "ATO", "ATP", "RMF","OFF", "RMR", "RM模式", "慢行模式", "退行模式", "救援模式", "紧急牵引", "未知" };
            Int32 index = 10;
            for (int i = 0; i < 10; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    index = i;
                    break;
                }
            }
            //String str = strs[index] + String.Format("-{0:F0}%", FloatList[UIObj.InFloatList[0]]);
            dcGs.DrawString(strs[index], new Font("宋体", 10), new SolidBrush(Color.White), new RectangleF(3+31, 511, 67, 33), FontInfo.SF_CC);
        }

        private String[] _strs2 = { "保持制动", "停放制动", "快速制动", "紧急制动", "惰性", "牵引", "制动", "未知" };
        private void paint_TrainState(Graphics dcGs)
        {
            Int32 index = 7;
            for (int i = 0; i < 7; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    index = i;
                    break;
                }
            }
            dcGs.DrawString(_strs2[index], new Font("宋体", 10), new SolidBrush(Color.White), new RectangleF(3 + 31+99*2, 511, 67, 33), FontInfo.SF_CC);
        }
        #endregion
    }
}
